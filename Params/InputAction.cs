using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params
{

    public class InputDescriptor
    {
        public string CommandExtension;
        public string Description;

        public InputDescriptor(string cmdExt, string desc)
        {
            CommandExtension = cmdExt;
            Description = desc;
        }
    }
    public class InputAction
    {
        public string Command;
        public InputDescriptor[] Descriptions;
        public Action<string> Perform;
        public InputAction(string cmd, InputDescriptor[] desc, Action<string> act)
        {
            Command = cmd;
            Descriptions = desc;
            Perform = act;
        }

    }
}
