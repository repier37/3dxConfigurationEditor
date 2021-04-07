using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace _3DxConfigurationEditor.SettingsParser
{
    public class TopSolidSettingsParser : SettingParser
    {

        #region Ctor
        public TopSolidSettingsParser(string inFilePath) : base(inFilePath)
        {
            CommandsWithShortcut = new List<TopSolidCommand>();
        }
        #endregion


        #region Properties
        public List<TopSolidCommand> CommandsWithShortcut { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// parse the document and fill this editor properties with the read values
        /// </summary>
        public void Parse()
        {
            if (this.XMLDoc is null)
                return;
            try
            {
                this.XMLDoc.Load(this.FilePath);
                //get all node with value
                XmlNodeList valuesNodes = this.XMLDoc.GetElementsByTagName("Value");
                //filter element  having an attribute "ShortCutKey"
                List<XmlNode> shortcutNodes = new List<XmlNode>();
                foreach (XmlNode node in valuesNodes)
                {
                    XmlNode nodeName = node.Attributes.GetNamedItem("name");
                    if (nodeName != null && nodeName.InnerText == "ShortcutKey")
                        shortcutNodes.Add(node);
                }

                foreach (XmlNode node in shortcutNodes)
                {
                    bool parsedKey = int.TryParse(node.InnerText, out int keyInt);
                    if (!parsedKey) continue;
                    Key key = this.GetKeyFromInt(keyInt);
                    if (key == Key.None) continue;
                    XmlNode commandNode = node.ParentNode;
                    string commandName = commandNode.Attributes?.GetNamedItem("name")?.InnerText;
                    if (string.IsNullOrWhiteSpace(commandName)) continue;
                    this.CommandsWithShortcut.Add(new TopSolidCommand(commandName, key));
                }
               

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
            }



            //

        }

        private Key GetKeyFromInt(int keyInt)
        {
            char c = (char)keyInt;

            Key result = KeyInterop.KeyFromVirtualKey(keyInt);
            return result;

        }


        #endregion
    }
}
