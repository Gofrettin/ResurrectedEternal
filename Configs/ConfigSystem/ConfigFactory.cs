using Newtonsoft.Json;
using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Configs.GamePlayConfig;
using ResurrectedEternal.Configs.SubConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs
{

    public static class ConfigFactory
    {
        public static event Action OnConfigReload;
        public static void SaveConfig()
        {
            
            Serializer.SaveJson(g_Globals.Config, g_Globals.ConfigConfig);

            //if (_reuslt.Length == 0)
                new Events.EventArgs.PushClipEventArgs("Config saved!", 7.77f, SharpDX.Color.Green);          
            ////   new Events.EventArgs.PushClipEventArgs("Could not save config!", 7.77f, SharpDX.Color.Red);

            //new GameplayWrapper
            //{
            //    AimbotConfig = g_Globals.Config.AimbotConfig,
            //    VisualConfig = g_Globals.Config.VisualConfig,
            //    NeonConfig = g_Globals.Config.NeonConfig,
            //    ExtraConfig = g_Globals.Config.ExtraConfig,
            //    ConvarConfig = g_Globals.Config.ConvarConfig,
            //    ColorConfig = g_Globals.Config.ColorConfig
            //}
        }

        public static void TryLoadConfig()
        {
            if (!System.IO.File.Exists(g_Globals.ConfigConfig))
                SaveConfig();
            //var _data = Henker.RPC.Config(1, ConfigType.Generic, new byte[0]);

            //should we request the base template config from the server?

            //if (_data == null || _data.Length == 0)
            //{
            //    //no config here.
            //    SaveConfig();
            //    return;
            //}
            g_Globals.Config = Serializer.LoadJson<Config>(g_Globals.ConfigConfig);
            //well try to load the config on start, we wont be generating new cfgvalues so we dont have to destroy the menu.

            //OnConfigReload?.Invoke();
        }



        public static void CreateNewConfig()
        {



            Type type = g_Globals.Config.GetType();
            var ConfigFields = type.GetFields();
            foreach (var item in ConfigFields)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(g_Globals.Config);
                if (_newitem == null || _newitem.GetType() == typeof(string)) continue;
                var _cfgvalEntryProps = _newitem.GetType().GetFields();

                if (_cfgvalEntryProps.Length == 0) continue;
                List<ConfigValueEntry> _vals = new List<ConfigValueEntry>();
                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;

                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                    if (cfgItem == null) continue;

                    if (string.IsNullOrEmpty(cfgItem.AccessorName))
                        cfgItem.AccessorName = _cfgvalEntry.Name;

                    _vals.Add(cfgItem);
                }

                string _menuName = _valtype.Name.Replace("Config", "").ToUpper();
                System.IO.File.WriteAllText(_menuName + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(_vals, Newtonsoft.Json.Formatting.Indented));

                //Root.Add(_menuName, _cfgVals);

            }
            Load();
        }

        static void Load()
        {
            List<List<ConfigValueEntry>> _shits = new List<List<ConfigValueEntry>>();
            Type type = g_Globals.Config.GetType();
            var ConfigFields = type.GetFields();
            foreach (var item in ConfigFields)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(g_Globals.Config);
                if (_newitem == null || _newitem.GetType() == typeof(string)) continue;
                var _cfgvalEntryProps = _newitem.GetType().GetFields();

                if (_cfgvalEntryProps.Length == 0) continue;
                string _menuName = _valtype.Name.Replace("Config", "").ToUpper();
                List<ConfigValueEntry> _vals = new List<ConfigValueEntry>();
                if (System.IO.File.Exists(_menuName + ".json"))
                {

                    _vals = JsonConvert.DeserializeObject<List<ConfigValueEntry>>(System.IO.File.ReadAllText(_menuName + ".json"));
                    DaConfigs.Add(_menuName, new Dictionary<string, ConfigValueEntry>());
                    foreach (var dd in _vals)
                    {
                        if (dd.Value is double)
                            dd.Value = Convert.ToSingle(dd.Value);
                        else if (dd.Value is long)
                            dd.Value = Convert.ToInt32(dd.Value);
                        else if (dd.Value is SharpDX.Color)
                            dd.MaxValue = g_Globals.ColorManager.Count;

                        DaConfigs[_menuName].Add(dd.AccessorName, dd);
                    }
                    _shits.Add(_vals);
                }




            }

            GenerateMappingClass();
            Console.WriteLine("BREAK");
        }

        private static void GenerateMappingClass()
        {
            if (!System.IO.Directory.Exists("mapping"))
                System.IO.Directory.CreateDirectory("mapping");
            foreach (var item in DaConfigs)
            {
                string[] mappings = new string[item.Value.Count + 2];
                mappings[0] = "public static class " + item.Key + "Map \n {";

               
                int idx = 1;
                foreach (var x in item.Value)
                {
                    var _type = GetTypeName(x.Value.Value);
                    string _the = string.Format("public {0} m_dw{2} => ({0})g_Globals.Config[\"{1}\"][\"{2}\"].Value;", _type, item.Key, x.Value.AccessorName);
                    mappings[idx] = _the;


                    idx++;
                }
                mappings[item.Value.Count+1] = "}";
                System.IO.File.WriteAllLines("mapping\\mapping_" + item.Key, mappings);
            }

        }

        public static string GetTypeName(object t)
        {
            var daType = t.GetType();
            if (daType == typeof(int))
                return "int";
            else if (daType == typeof(float))
                return "float";
            else if (daType == typeof(bool))
                return "bool";
            else if (daType == typeof(Newtonsoft.Json.Linq.JObject))
                return "SharpDX.Color";
            else
                return "string";
        }

        public static Dictionary<string, Dictionary<string, ConfigValueEntry>> DaConfigs = new Dictionary<string, Dictionary<string, ConfigValueEntry>>();

        //public static void LoadEnvironmentConfig()
        //{
        //    var _data = Henker.RPC.Request(7, 2, g_Globals.MapName);

        //    //we dont have a config yet.
        //    if (_data.Length == null)
        //    {
        //        SaveEnvironmentConfig();
        //        LoadEnvironmentConfig();
        //        return;
        //    }


        //    var _resultSet = Serializer.DecryptedJson<EnvironmentWrapper>(_data);

        //    if (_resultSet == null)
        //        return;

        //    g_Globals.Config.MovieConfig = _resultSet.MovieConfig;
        //    g_Globals.Config.SunConfig = _resultSet.SunConfig;
        //    g_Globals.Config.TIDAL = _resultSet.TIDAL;
        //    g_Globals.Config.SpriteConfig = _resultSet.SpriteConfig;
        //    OnConfigReload?.Invoke();

        //}
        //[System.Serializable]
        //public class GameplayWrapper
        //{
        //    public AimbotConfig AimbotConfig;
        //    public VisualConfig VisualConfig;
        //    public NeonConfig NeonConfig;
        //    public ExtraConfig ExtraConfig;
        //    public ConvarConfig ConvarConfig;
        //    public ColorConfig ColorConfig;
        //}
        //[System.Serializable]
        //public class EnvironmentWrapper
        //{
        //    public AmbientConfig MovieConfig;
        //    public EnvironmentConfig TIDAL;
        //    public SunConfig SunConfig;
        //    public SPRITES SpriteConfig;
        //}
    }
}
