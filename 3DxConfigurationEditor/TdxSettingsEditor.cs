using _3DxConfigurationEditor.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace _3DxConfigurationEditor
{
    /// <summary>
    /// Class to open and edit a 3dx configuration file
    /// </summary>
    public class TdxSettingsEditor : SettingsParser.SettingParser
    {



        public TdxSettingsEditor(string inFilePath): base(inFilePath)
        {

            this.Macros = new List<MacroEntry>();
            this.ButtonActions = new List<ButtonAction>();
            this.HasLoadedFile = false;
        }

        #region Properties

        /// <summary>
        /// Macros ids in the xmlDoc
        /// </summary>
        public List<MacroEntry> Macros { get; private set; }

        public List<ButtonAction> ButtonActions { get; private set; }

        /// <summary>
        /// Wether or not the editor has a loaded file
        /// </summary>
        public bool HasLoadedFile
        {
            get;
            private set;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Load the xml document
        /// </summary>
        /// <returns></returns>
        public bool Load(out string outError)
        {
            outError = string.Empty;
            if (!System.IO.File.Exists(this.FilePath))
                return false;

            try
            {
                this.XMLDoc.Load(this.FilePath);
                this.HasLoadedFile = true;
                if (!this.LoadMacros(out outError))
                    return false;

                if (!this.LoadButtonActions(out outError))
                    return false;
            }
            catch (Exception e)
            {
                outError = e.Message;
                this.HasLoadedFile = false;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Load the button actions
        /// </summary>
        /// <param name="outError"></param>
        /// <returns></returns>
        private bool LoadButtonActions(out string outError)
        {
            this.ButtonActions.Clear();
            outError = string.Empty;
            //get the button actions entry
            XmlNode ButtonActions = this.GetNodeDeep(this.XMLDoc, "ButtonActions");
            if (ButtonActions is null)
            {
                outError = "No button actions found";
                return false;
            }

            foreach (XmlNode node in ButtonActions.ChildNodes)
            {
                if (node.Name != "ButtonAction")
                    continue;

                ButtonAction button = new ButtonAction();
                //get its ID
                XmlNode ID = this.GetNodeDeep(node, "ID");
                if (ID is null || string.IsNullOrEmpty(ID.InnerText)) //skip node without id
                    continue;

                button.ID = ID.InnerText;

                //Name
                XmlNode Name = this.GetNodeDeep(node, "Name");
                if (!(Name is null) && !string.IsNullOrEmpty(Name.InnerText))
                {
                    button.Name = Name.InnerText;
                }
                //image source
                XmlNode Source = this.GetNodeDeep(node, "Source");
                if (!(Source is null) && !string.IsNullOrEmpty(Source.InnerText))
                {
                    button.ImageSource = Source.InnerText;
                }

                this.ButtonActions.Add(button);
            }

            return true;
        }

        /// <summary>
        /// Load the macros contains in the current xmlDoc
        /// </summary>
        /// <param name="outError"></param>
        /// <returns>true if loading succesfull</returns>
        private bool LoadMacros(out string outError)
        {
            this.Macros.Clear();
            outError = string.Empty;
            //from cfgNode get the macrotable
            XmlNode MacroTableNode = this.GetNodeDeep(this.XMLDoc, "MacroTable");

            if (MacroTableNode is null)
            {
                outError = "No macro table found";
                return false;
            }

            foreach (XmlNode node in MacroTableNode.ChildNodes)
            {
                if (node.Name != "MacroEntry")
                    continue;

                //found macro entry get its ID
                XmlNode ID = this.GetNodeDeep(node, "ID");
                if (ID != null)
                    this.Macros.Add(new MacroEntry(ID.InnerText));
            }

            return true;
        }

        /// <summary>
        /// Get the buttonAction corresponding to a given macro
        /// </summary>
        /// <param name="inMacro"></param>
        /// <returns></returns>
        public ButtonAction GetButtonActionFromMacro(MacroEntry inMacro)
        {
            if (inMacro is null) return null;
            foreach (ButtonAction button in this.ButtonActions)
            {
                if (button.ID == inMacro.ID)
                    return button;
            }
            return null;
        }

        /// <summary>
        /// Add a new macro entry to the current xml file
        /// </summary>
        /// <param name="inId"></param>
        /// <param name="inKeys"></param>
        /// <returns></returns>
        public bool AddMacroEntry(string inId, List<KeyWithAction> inKeys)
        {
            if (!System.IO.File.Exists(this.FilePath))
                return false;

            this.XMLDoc.Load(this.FilePath);

            //from cfgNode get the macrotable
            XmlNode MacroTableNode = this.GetNodeDeep(this.XMLDoc, "MacroTable");

            if (MacroTableNode is null)
                return false;


            XmlElement newMacroEntry = this.XMLDoc.CreateElement("MacroEntry");
            XmlElement Id = this.XMLDoc.CreateElement("ID");
            Id.InnerText = inId;
            newMacroEntry.AppendChild(Id);

            if (inKeys.Count == 2) //only one key pressed (and released) create keystroke
            {
                XmlElement keyStroke = this.XMLDoc.CreateElement("KeyStroke");
                XmlElement Key = this.XMLDoc.CreateElement("Key");
                Key.InnerText = inKeys.First().GetHIDValue();
                keyStroke.AppendChild(Key);
                newMacroEntry.AppendChild(keyStroke);
            }
            else //create a sequence
            {
                XmlElement sequence = this.XMLDoc.CreateElement("Sequence");
                foreach (KeyWithAction key in inKeys)
                {
                    string elementName = key.GetXMLElementName();
                    if (string.IsNullOrEmpty(elementName)) continue; //skip invalid key

                    string HIDValue = key.GetHIDValue();
                    if (string.IsNullOrEmpty(HIDValue)) continue; // we don't have mapping do nothing

                    //create the element
                    XmlElement keyElement = this.XMLDoc.CreateElement(elementName);
                    keyElement.InnerText = HIDValue;

                    //Append it to the sequence
                    sequence.AppendChild(keyElement);
                }
                newMacroEntry.AppendChild(sequence);
            }

            MacroTableNode.AppendChild(newMacroEntry);
            this.XMLDoc.Save(this.FilePath);

            return true;
        }

        /// <summary>
        /// Add a new button action to the current xml file
        /// </summary>
        /// <param name="inId"></param>
        /// <param name="inImageFilePath"></param>
        /// <returns></returns>
        public bool AddButtonAction(MacroEntry inMacro, string inImageFilePath)
        {
            if (inMacro is null || string.IsNullOrEmpty(inImageFilePath))
                return false;

            //get the button actions entry
            XmlNode ButtonActions = this.GetNodeDeep(this.XMLDoc, "ButtonActions");
            if (ButtonActions is null)
                return false;

            //find if we update or create
            ButtonAction existingAction = this.GetButtonActionFromMacro(inMacro);
            if (existingAction != null)
            { //update
                foreach (XmlNode node in ButtonActions)
                {
                    if (node.Name != "ButtonAction")
                        continue;

                    //get Id
                    XmlNode ID = this.GetNodeDeep(node, "ID");
                    if (ID != null & ID.InnerText == existingAction.ID)
                    {//found node to update, do it
                        //get source node
                        XmlNode sourceNode = this.GetNodeDeep(node, "Source");
                        if (sourceNode != null)
                        {
                            sourceNode.InnerText = inImageFilePath;
                            this.XMLDoc.Save(this.FilePath);
                            return true;
                        }

                    }

                }

            }
            else
            {
                //now create the new node
                XmlElement newButtonAction = this.XMLDoc.CreateElement("ButtonAction");

                newButtonAction.SetAttribute("Type", "App");
                XmlElement Id = this.XMLDoc.CreateElement("ID");
                Id.InnerText = inMacro.ID;
                XmlElement Name = this.XMLDoc.CreateElement("Name");
                Name.InnerText = inMacro.ID;

                XmlElement Image = this.XMLDoc.CreateElement("Image");
                XmlElement Source = this.XMLDoc.CreateElement("Source");
                Source.InnerText = inImageFilePath;

                Image.AppendChild(Source);

                newButtonAction.AppendChild(Id);
                newButtonAction.AppendChild(Name);
                newButtonAction.AppendChild(Image);

                ButtonActions.AppendChild(newButtonAction);
            }
            return true;

        }


        #endregion





    }
}
