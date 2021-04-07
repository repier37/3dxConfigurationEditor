using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _3DxConfigurationEditor.SettingsParser
{
    /// <summary>
    /// Base class of settings parser
    /// </summary>
    public class SettingParser
    {


        public SettingParser(string inFilePath)
        {
            this.FilePath = inFilePath;
            this.XMLDoc = new XmlDocument();
        }

        #region properties
        /// <summary>
        /// The XML Document to modify
        /// </summary>
        public XmlDocument XMLDoc { get; set; }

        /// <summary>
        /// FilePath of the XML Document
        /// </summary>
        public string FilePath { get; set; }
        #endregion




        #region Methods
        /// <summary>
        /// Get the first child node of <paramref name="inNode"/> whose name is <paramref name="inNodeName"/>
        /// </summary>
        /// <param name="inNodeName"></param>
        /// <returns></returns>
        protected XmlNode GetNodeDeep(XmlNode inNode, string inNodeName)
        {
            if (inNode is null) return null;
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


        #endregion


    }
}
