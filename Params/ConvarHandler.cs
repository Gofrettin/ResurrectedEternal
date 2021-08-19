using ResurrectedEternal.ClientObjects.Cvars;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Params.CSHelper;
using RRFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params
{
    class ConvarHandler
    {


        private ConvarManager ConvarManager => Henker.Singleton.ConvarManager;

        public ConvarHandler()
        {

        }

        public void ShowConvars()
        {

        }

        public void HandleConvar(string input)
        {
            // space -> split -> "VALUE"

            var _params = ConsoleHelper.CreateParameteres(input, ' ');

            /*
             * params[0] = convar
             * params[1] = action
             * params[2] = convar name or pattern
             * 
             * 
             */


            if (_params.Length < 2)
            {
                foreach (var item in ConvarManager.GetConvars())
                {
                    ConsoleHelper.Write(item + "\n");
                }

                //ConsoleHelper.Write("Error!\n Parameter missing!\n", ConsoleColor.Red);
                return;
            }



            var _action = _params[1];
            if (_action == "list")
            {
                ListConvars();
                return;
            }
            var _cvarName = _params[2];

            if (_action == "search")
            {
                //lets crawl our database
                var c = ConvarManager.SearchConvarPattern(_cvarName);
                for (int i = 0; i < c.Length; i++)
                {
                    ConsoleHelper.Write(c[i] + "\n", 2);
                }
                ConsoleHelper.Write(string.Format("Crawl result for {1}: {0} possible entries found\n", c.Length, _cvarName), ConsoleColor.Green);
                return;
            }

            var _convar = ConvarManager.GetConvar(_cvarName);
            if (_convar == null)
            {
                ConsoleHelper.Write(string.Format("Error!\n Convar {0} does not exist!\n", _params[2]), ConsoleColor.Red);
                return;
            }

            if (_action == "remove")
            {
                RemoveConvar(_convar);
                ConsoleHelper.Write(string.Format("Success!\nConvar {0} has been sucessfully removed\n", _convar.m_pszName), ConsoleColor.Green);
            }
            else if (_action == "add")
            {
                var _value = ConsoleHelper.GetValue(input, '"');
                if (string.IsNullOrEmpty(_value))
                {
                    ConsoleHelper.Write(string.Format("Error!\n Missing parameter 'value' for {0}!\n e.g: sv_cheats \"1\"\n", _params[2]), ConsoleColor.Red);
                    return;
                }
                AddConvar(_convar, _value);
                ConsoleHelper.Write(string.Format("Success!\nConvar {0} has been sucessfully added or updated.\nNew Value {1}\n", _convar.m_pszName, _value), ConsoleColor.Green);
            }
            else
            {
                ConsoleHelper.Write(string.Format("Error!\n Unknown command {0}\n", _action), ConsoleColor.Red);
                return;
            }








        }

        private void ListConvars()
        {
            //now the question is how do we get to the convars lol
            //throw new NotImplementedException();
            EventManager.ShowConvars();
        }

        private void AddConvar(ConvarEntity _cvar, string expectedValue)
        {
            //first we must check if the convar is already added to the active convars.
            //if it is we'll update the expected value for it
            //if not we'll just add it to the convars.
            //doesnt matter, alread did LOL
            new ConvarEntityEventArgs(_cvar, expectedValue);



        }

        private void RemoveConvar(ConvarEntity _cvar)
        {
            //huh
            new RemoveConvarEventArgs(_cvar.m_pszName);
            //EventManager.NotifyRemoveConvar(_cvar.m_pszName);
        }


    }
}
