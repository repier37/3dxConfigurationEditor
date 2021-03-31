using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace _3DxConfigurationEditor
{
    public class XMLEditor
    {
        public XmlDocument XMLDoc{ get; set; }
        public string FilePath{ get; set; }

        public ObservableCollection<string> Macros { get; set; }


        public XMLEditor(string inFilePath)
        {
            this.FilePath = inFilePath;
            XMLDoc = new XmlDocument();

        }

        public bool AddMacroEntry(string inId, List<KeyWithAction> inKeys)
        {
            if (!System.IO.File.Exists(this.FilePath))
                return false;

            this.XMLDoc.Load(this.FilePath);

            //from cfgNode get the macrotable
            XmlNode MacroTableNode = this.GetNodeDeep(this.XMLDoc,"MacroTable");

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

        public bool AddButtonAction(string inId, string inImageFilePath)
        {
            if (!System.IO.File.Exists(this.FilePath))
                return false;

            this.XMLDoc.Load(this.FilePath);

            //get the button actions entry
            XmlNode ButtonActions = this.GetNodeDeep(this.XMLDoc, "ButtonActions");
            if (ButtonActions is null)
                return false;

            //now create the new node
            XmlElement newButtonAction = this.XMLDoc.CreateElement("ButtonAction");

            newButtonAction.SetAttribute("Type", "App");
            XmlElement Id = this.XMLDoc.CreateElement("ID");
            Id.InnerText = inId;
            XmlElement Name = this.XMLDoc.CreateElement("Name");
            Name.InnerText = inId;

            XmlElement Image = this.XMLDoc.CreateElement("Image");
            XmlElement Source = this.XMLDoc.CreateElement("Source");
            Source.InnerText = inImageFilePath;

            Image.AppendChild(Source);

            newButtonAction.AppendChild(Id);
            newButtonAction.AppendChild(Name);
            newButtonAction.AppendChild(Image);

            ButtonActions.AppendChild(newButtonAction);

            this.XMLDoc.Save(this.FilePath);

            return true;

        }

        internal List<string> GetExistingMacros()
        {
            List<string> result = new List<string>();
            if (!System.IO.File.Exists(this.FilePath))
                return result;

            this.XMLDoc.Load(this.FilePath);

            //from cfgNode get the macrotable
            XmlNode MacroTableNode = this.GetNodeDeep(this.XMLDoc, "MacroTable");

            if (MacroTableNode is null)
                return result;

            foreach (XmlNode node in MacroTableNode.ChildNodes)
            {
                if (node.Name != "MacroEntry")
                    continue;

                //found macro entry get its ID
                XmlNode ID = this.GetNodeDeep(node, "ID");
                if (ID != null)
                    result.Add(ID.InnerText);
            }

            return result;

        }

        /// <summary>
        /// Get the first node whose name is <paramref name="inNodeName"/>
        /// </summary>
        /// <param name="inNodeName"></param>
        /// <returns></returns>
        private XmlNode GetNodeDeep(XmlNode inNode, string inNodeName)
        {
            foreach (XmlNode node in inNode) //get child node
            {
                if (node.Name == inNodeName) //look for a node named as parameter
                    return node;
                else if (node.HasChildNodes) //if not found look in its childs
                {
                    XmlNode result = this.GetNodeDeep(node, inNodeName);
                    if (result != null) return result;
                }
            }

            return null;
        }



        
    }
}
