using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _3DxConfigurationEditor.SettingsParser
{
    public class TopSolidCommand
    {
        public TopSolidCommand(string commandId, Key commandShortcut)
        {
            CommandId = commandId;
            CommandShortcut = commandShortcut;
        }

        public string CommandId { get; set; }

        public Key CommandShortcut { get; set; }

        public string ComputeCommandNiceName()
        {
            //TODO
            return CommandId;
        }

        public override string ToString()
        {
            return this.CommandId +": " + CommandShortcut.ToString();
        }
    }
}
