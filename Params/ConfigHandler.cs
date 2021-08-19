using ResurrectedEternal.Params.CSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params
{
    class ConfigHandler
    {
        private Config Config => g_Globals.Config;

        public ConfigHandler()
        {

        }

        public void HandleConfig(string _input)
        {
            var param = ConsoleHelper.CreateParameteres(_input, ' ');

            /* 0 = "config" - input command
             * 1 = set / show
             * 2 = config
             * 3= entry
             * 4 = value
             * 5 = huh
             */

            if (param.Length < 2)
            {
                ConsoleHelper.Write("Error!\nParameters missing.\n type help for more information.\n", ConsoleColor.Red);
                return;
            }
            var _action = param[1];
            if (_action == "set")
            {
                if (param.Length < 5)
                {
                    ConsoleHelper.Write("Error!\nParameters missing.\n type help for more information.\n", ConsoleColor.Red);
                    return;
                }
                var config = param[2];
                var entry = param[3];
                var value = param[4];

                var _collect = GetEntry(config, entry);
                if (_collect == null)
                {
                    ConsoleHelper.Write(string.Format("Error!\n Config Tree Name {0} could not be found.\n", entry), ConsoleColor.Red);
                    //not found
                    return;
                }

                if (Apply(_collect, value))
                {
                    ConsoleHelper.Write(string.Format("Success!\nEntry {0} has been set to {1}\n", _collect.Name, _collect.Value.ToString()), ConsoleColor.Green);
                }
                else
                {
                    ConsoleHelper.Write(string.Format("Error!\nEntry {0} could not parse value {1}\n", _collect.Name, _collect.Value.ToString()), ConsoleColor.Red);
                }
            }
            else if (_action == "show")
            {
                //config show xyz
                if (param.Length == 3)
                {
                    //show config section
                    RenderConfigSection(param[2].ToLower());
                }
                //config show
                else if (param.Length == 2)
                {
                    RenderConfig();
                }
            }
            else
            {
                ConsoleHelper.Write(string.Format("Error!\nUnknown {0}\n", param[1]), ConsoleColor.Red);
                return;
            }






        }

        private ConfigValueEntry GetEntry(string _treeName, string entryName)
        {
            _treeName = _treeName.ToLower();
            entryName = entryName.ToLower();

            Type type = Config.GetType();
            var _fieldInfo = type.GetFields();
            //ITerate over Aimbot, Visual etc.
            foreach (var item in _fieldInfo)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(Config);
                if (_newitem == null) continue;
                //the correct tree entry
                if (_treeName == item.Name.ToLower())
                {
                    var _cfgvalEntryProps = _newitem.GetType().GetFields();
                    foreach (var _cfgvalEntry in _cfgvalEntryProps)
                    {
                        //Console.WriteLine(" >>>> Name: " + prop.Name + ", Value: " + prop.GetValue(_newitem));
                        if (_cfgvalEntry.FieldType == typeof(string)) continue;

                        var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                        if (cfgItem == null) continue;

                        if (_cfgvalEntry.Name.ToLower() == entryName)
                            return cfgItem;
                    }
                }

            }
            return null;
        }

        private void RenderConfigSection(string section)
        {
            int num = 0;
            Type type = Config.GetType();
            var _fieldInfo = type.GetFields();
            var _select = _fieldInfo.Where(x => x.Name.ToLower() == section.ToLower()).FirstOrDefault();

            if (_select == null)
            {
                ConsoleHelper.Write("Error!\nCould not find " + section + "\n", ConsoleColor.Red);
                return;
            }

            var _valItem = _select.GetValue(Config);
            if (_valItem == null)
            {
                ConsoleHelper.Write("Error!\n Unexpected Value Item found.\n" + section, ConsoleColor.Red);
                return;
            }

            var _fields = _valItem.GetType().GetFields();
            foreach (var item in _fields)
            {
                //var cfg = item.GetValue(_valItem);
                var _entryProps = item.GetValue(_valItem) as ConfigValueEntry;
                if (_entryProps == null || _entryProps.HiddenFromMenu) continue;
                ConsoleHelper.Write(Generators.EndPadString(item.Name.ToLower(), 50), 9, ConsoleColor.White);
                var _type = _entryProps.Value.GetType();
                if(_type == typeof(SharpDX.Color))
                {
                    var _clrName = g_Globals.ColorManager.GetNameByColor((SharpDX.Color)_entryProps.Value);
                    ConsoleHelper.Write(_clrName + "\n", ConsoleColor.Cyan);
                }
                else
                {
                    ConsoleHelper.Write(_entryProps.Value.ToString() + "\n", ConsoleColor.Cyan);
                }
                
                num++;
            }
            ConsoleHelper.Write(string.Format("Success!\nDisplaying {0} entries.\n", num), ConsoleColor.Green);
            //a unhandled exception if the keys dont match?
        }

        private void RenderConfig()
        {
            int numTotal = 0;
            Type type = Config.GetType();
            var _fieldInfo = type.GetFields();
            //ITerate over Aimbot, Visual etc.
            foreach (var item in _fieldInfo)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(Config);
                if (_newitem == null) continue;
                var _cfgvalEntryProps = _newitem.GetType().GetFields();
                string nicename = item.Name;
                ConsoleHelper.Write(string.Format("Config Tree {0}\n", nicename.ToLower()), 3, ConsoleColor.White);
                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;
                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;
                    if (cfgItem == null || cfgItem.HiddenFromMenu) continue;
                    ConsoleHelper.Write(Generators.EndPadString(_cfgvalEntry.Name.ToLower(), 50), 9, ConsoleColor.White);
                    var _type = cfgItem.Value.GetType();
                    if (_type == typeof(SharpDX.Color))
                    {
                        var _clrName = g_Globals.ColorManager.GetNameByColor((SharpDX.Color)cfgItem.Value);
                        ConsoleHelper.Write(_clrName + "\n", ConsoleColor.Cyan);
                    }
                    else
                    {
                        ConsoleHelper.Write(cfgItem.Value.ToString() + "\n", ConsoleColor.Cyan);
                    }
                    numTotal++;
                }

            }
            ConsoleHelper.Write(string.Format("Success!\nDisplaying {0} entries.\n", numTotal), ConsoleColor.Green);
        }

        private bool Apply(ConfigValueEntry entry, string newValue)
        {
            try
            {
                var _new = CastTo(newValue, entry.Value.GetType());
                entry.Value = _new;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private dynamic CastTo(string value, Type type)
        {
            if (type == typeof(float))
                return Convert.ToSingle(CorrectConvert(value, type));
            else if (type == typeof(int))
                return Convert.ToInt32(CorrectConvert(value, type));
            else if (type == typeof(string))
                return value.ToString();
            else
                return value;

        }
        private object CorrectConvert(string value, Type type)
        {
            return Convert.ChangeType(value, type);
        }
    }
}
