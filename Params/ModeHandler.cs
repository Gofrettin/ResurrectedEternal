using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Params.CSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params
{
    class ModeHandler
    {
        public void HandleMode(string input)
        {
            var _input = ConsoleHelper.CreateParameteres(input, ' ');

            /*
             * 0 = color
             * 1 = add / remove
             * 2 3 4 5 = R G B A
             * 6 = name
             * 
             */
            bool _generateName = true;
            //we dont have a name
            if (_input.Length < 2)
            {
                ConsoleHelper.Write(string.Format("The current Mode is {0}\n", Events.StateMachine.ClientModus), ConsoleColor.Magenta);
                return;
            }

            var _action = _input[1];
            if(Enum.TryParse<Events.Modus>(_action, out var _mode))
            {
                Events.StateMachine.ClientModus = _mode;
                SaveMode();
                //save mode
                ConsoleHelper.Write(string.Format("Success!\n New Mode set and has been saved. [{0}] \n", _action), ConsoleColor.Green);

            }
            else
            {
                ConsoleHelper.Write(string.Format("Error!\n Mode not found. [{0}]\n", _action), ConsoleColor.Red);
            }
        }

        private void SaveMode()
        {
            Serializer.SaveJson(Events.StateMachine.ClientModus, g_Globals.ModeConfig);
            //Henker.RPC.Config(0, ConfigType.Mode, new byte[1] { Convert.ToByte(
            //    Events.StateMachine.ClientModus) });
        }
    }
}
