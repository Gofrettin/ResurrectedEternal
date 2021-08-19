using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Configs;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.GenericObjects;
using ResurrectedEternal.Params;
using ResurrectedEternal.Params.CSHelper;
using ResurrectedEternal.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal
{



    public class ParamManager
    {
        public static ParamManager instance;

        public ParamManager()
        {
            instance = this;
        }

        private ConvarHandler ConvarHandler = new ConvarHandler();
        private ConfigHandler ConfigHandler = new ConfigHandler();
        private ColorHandler ColorHandler = new ColorHandler();
        private ModeHandler ModeHandler = new ModeHandler();
        private Dictionary<string, Dictionary<string, ConfigValueEntry>> _params = new Dictionary<string, Dictionary<string, ConfigValueEntry>>();

        private Dictionary<string, InputAction> _Actions = new Dictionary<string, InputAction>();
        private string OneTab = "   ";
        private string TwoTab = "       ";

        public void ParseParams()
        {
            ConfigFactory.OnConfigReload += ConfigFactory_OnConfigReload;
            Parse();
            ConsoleHelper.ShowAction("Hooking console...", 33);
        }

        /*
         * Add Color -> eg. color add 255 255 255 255 name
         */

        private void ConfigFactory_OnConfigReload()
        {
            Parse();
            //throw new NotImplementedException();
        }

        public void Hook()
        {
            HookConsole();
        }

        //This breaks when a config gets reloaded so we have to repopulate shit if we load another config.

        private void Parse()
        {
            _params.Clear();
            Type type = g_Globals.Config.GetType();
            var _fieldInfo = type.GetFields();
            //ITerate over Aimbot, Visual etc.
            foreach (var item in _fieldInfo)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(g_Globals.Config);
                if (_newitem == null) continue;
                var _cfgvalEntryProps = _newitem.GetType().GetFields();
                string nicename = item.Name;
                var _dict = new Dictionary<string, ConfigValueEntry>();
                //entry to Aimbot and its entries
                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    //Console.WriteLine(" >>>> Name: " + prop.Name + ", Value: " + prop.GetValue(_newitem));
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;

                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                    if (cfgItem == null) continue;

                    _dict.Add(_cfgvalEntry.Name.ToLower().Replace(" ", ""), cfgItem);
                }
                if (_dict.Count > 0)
                    _params.Add(nicename.ToLower(), _dict);

            }
        }

        private string _inputPrepend => g_Globals.Nickname;



        private void HookConsole()
        {
            LoadCommands();
            //Clear(null);
            ConsoleHelper.Write("[Bootup Sequence Completed]\n", ConsoleColor.Green);
            ConsoleHelper.Write("[MODE is set to " + StateMachine.ClientModus.ToString() + "]\n", ConsoleColor.Magenta);
            ConsoleHelper.Write($"Welcome {g_Globals.Nickname}. Enter !help to display commands\n");

            while (true)
            {
                ExecuteCommand(ConsoleHelper.ColorCaptureInput(_inputPrepend));
            }
        }

        private void ExecuteCommand(string input)
        {
            string command = "";
            if (input.Contains(' '))
                command = input.Remove(input.IndexOf(' '), input.Length - input.IndexOf(' '));
            else
                command = input;

            if (_Actions.ContainsKey(command))
            {
                _Actions[command].Perform(input);
            }
            else
            {
                ConsoleHelper.Write(string.Format("Error!\n Command {0} not found.\n", command), ConsoleColor.Red);
            }
        }

        private void LoadCommands()
        {
            _Actions.Add("help", new InputAction("help", new InputDescriptor[]
            {
                new InputDescriptor("help", "displays all available commands")
            }, RenderHelp));
            _Actions.Add("sub", new InputAction("sub", new InputDescriptor[]
            {
                new InputDescriptor("sub", "displays your current subscription")
            }, RenderSub));
            string _niceShowConfig = "";
            foreach (var item in _params.Keys)
            {
                _niceShowConfig += item + " ";
            }
            _Actions.Add("config", new InputAction("config", new InputDescriptor[]
            {
                new InputDescriptor("config show", "displays your current config set-up."),
                 new InputDescriptor("config show {config}", "displays the set-up for config tree entry {name} - config show aimbot"),
                 new InputDescriptor("config set {config} {name} {value}", "set's a value for a config tree entry - config set aimbot angle 180"),
                 new InputDescriptor("", _niceShowConfig)

            }, ConfigHandler.HandleConfig));

            _Actions.Add("convar", new InputAction("convar", new InputDescriptor[]
            {
                 new InputDescriptor("convar","displays all currently available console commands."),
                 new InputDescriptor("convar search {pattern}","find a convar by a pattern."),
                 new InputDescriptor("convar add {name} \"{value}\"","adds or updates a convar."),
                 new InputDescriptor("convar remove {name}","removes a convar and reverts to the default value."),
 //                new InputDescriptor("convar list", "shows all currently active convars.")
            }, ConvarHandler.HandleConvar));

            _Actions.Add("color", new InputAction("color", new InputDescriptor[]
            {
                new InputDescriptor("color show","show's all colors"),
                new InputDescriptor("color add {R} {G} {B} {A} {name}","adds or updates a color"),
                //new InputDescriptor("color remove {name}","removes a color from the color manager"),

            }, ColorHandler.HandleColor));
            _Actions.Add("mode", new InputAction("mode", new InputDescriptor[]
            {
                new InputDescriptor("mode","shows the current mode"),
                new InputDescriptor("mode novisuals","disables all drawing features"),
                new InputDescriptor("mode leaguemode","disables all features except aimbot and triggerbot"),
                new InputDescriptor("mode streammodefull","disables drawing, glow, effects, sounds"),
                new InputDescriptor("mode streammode","disables glow, effects, sounds"),
                new InputDescriptor("mode full","all features enabled"),
            //new InputDescriptor("color remove {name}","removes a color from the color manager"),

            }, ModeHandler.HandleMode));
            _Actions.Add("clear", new InputAction("clear", new InputDescriptor[]
            {
                new InputDescriptor("clear", "clears the console window")
            }, Clear));
            ConsoleHelper.ConfirmAction("OK!");

        }

        internal void Clear(string str)
        {
            Console.Clear();
        }

        private void RenderSub(string s)
        {
            //var _data = Henker.RPC.Request(99);
            //if (_data.Length == 0)
            //{
            //    ConsoleHelper.Write("Error!\nCouldn't resolve subscription length.\n", ConsoleColor.Red);
            //    return;
            //}
            //var _long = BitConverter.ToInt64(_data, 0);

            var _timespan = TimeSpan.FromTicks(long.MaxValue);

            ConsoleHelper.Write("Subscription expires in " + PrintTimeSpan(_timespan) + "\n", 5, ConsoleColor.Cyan);
        }

        private string PrintTimeSpan(TimeSpan t)
        {
            string answer = "";
            if (t.Days > 0)
            {
                answer += t.Days.ToString() + " days ";
            }
            if (t.Hours > 0)
            {
                answer += t.Hours.ToString() + " hours ";
            }
            if (t.Minutes > 0)
            {
                if (string.IsNullOrEmpty(answer))
                    answer += t.Minutes.ToString() + " minutes";
                else
                    answer += "and " + t.Minutes.ToString() + " minutes";
            }

            return answer;
        }

        private void RenderHelp(string s)
        {
            foreach (var item in _Actions)
            {
                //main commaind -> descriptions[] 

                ConsoleHelper.Write("[" + item.Key + "]\n", ConsoleColor.Red);
                foreach (var desc in item.Value.Descriptions)
                {
                    ConsoleHelper.Write(Generators.EndPadString(desc.CommandExtension, 36), 3, ConsoleColor.Cyan);
                    ConsoleHelper.Write(desc.Description + "\n", ConsoleColor.White);
                }
                ConsoleHelper.Write("\n");
            }

        }
    }
}
