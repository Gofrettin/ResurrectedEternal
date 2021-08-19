using RRFull;
using ResurrectedEternal.Configs.ConfigSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Globals
{
    public class ManagedColor
    {
        public string Name { get; set; }
        public SharpDX.Color Color { get; set; }

        public ManagedColor(string n, SharpDX.Color c)
        {
            Color = c;
            Name = n;
        }
    }

    public class ColorManager
    {
        public int Count => ColorDictionary.Count - 1;
        private Dictionary<string, SharpDX.Color> StringDictionary = new Dictionary<string, SharpDX.Color>();
        private Dictionary<SharpDX.Color, string> ColorDictionary = new Dictionary<SharpDX.Color, string>();
        public ColorManager()
        {
            //var _getColors = Henker.RPC.Config(1, ConfigType.Colors, new byte[0]);

            if (!File.Exists(g_Globals.ColorConfig))
            {
                GenerateColors();
            }

            StringDictionary = Serializer.LoadJson<Dictionary<string, SharpDX.Color>>(g_Globals.ColorConfig);
            foreach (var item in StringDictionary)
                if (!ColorDictionary.ContainsKey(item.Value))
                    ColorDictionary.Add(item.Value, item.Key);


        }

        private void GenerateColors()
        {
            var _clrs = Generators.GetStaticPropertyBag(typeof(SharpDX.Color));
            foreach (var item in _clrs)
            {
                if (item.Key == "toStringFormat") continue;
                StringDictionary.Add(item.Key, (SharpDX.Color)item.Value);
                if (!ColorDictionary.ContainsKey((SharpDX.Color)item.Value))
                    ColorDictionary.Add((SharpDX.Color)item.Value, item.Key);
            }
            SaveColors();

        }

        private void SaveColors()
        {
            Serializer.SaveJson(StringDictionary, g_Globals.ColorConfig);
        }

        public bool DoesColorExist(string name)
        {
            return StringDictionary.ContainsKey(name);
        }

        public bool DoesColorExist(SharpDX.Color _clr)
        {
            return ColorDictionary.ContainsKey(_clr);
        }

        public void AddColor(SharpDX.Color _color, string name)
        {
            if (StringDictionary.ContainsKey(name))
            {
                StringDictionary[name] = _color;
                ColorDictionary[_color] = name;
                SaveColors();
                return;
            }
            else if (ColorDictionary.ContainsKey(_color))
            {
                var _name = ColorDictionary[_color];
                StringDictionary[_name] = _color;
                SaveColors();
                return;
            }
            Add(name, _color);
        }

        public SharpDX.Color GetColorByName(string name)
        {
            return StringDictionary[name];
        }

        public SharpDX.Color GetNextColor(SharpDX.Color color)
        {
            bool _return = false;
            foreach (var item in ColorDictionary)
            {
                if (_return)
                    return item.Key;
                if (item.Key == color)
                    _return = true;
            }

            return ColorDictionary.First().Key;
        }
        public SharpDX.Color GetPrevious(SharpDX.Color color)
        {
            var _list = ColorDictionary.Keys.ToArray();
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i] == color)
                {
                    var num = i - 1;
                    if (num < 0)
                        return _list[_list.Length - 1];
                    else
                        return _list[num];
                }
            }
            return ColorDictionary.First().Key;
        }
        public string GetNameByColor(SharpDX.Color _color)
        {


            if (!ColorDictionary.ContainsKey(_color))
            {
                var _str = Generators.GetRandomString(4);
                //ColorDictionary.Add(_color, "Color " + _str);
                //StringDictionary.Add(_str, _color);
                Add(_str, _color);
            }

            return ColorDictionary[_color];
        }

        private void Add(string str, SharpDX.Color _clr)
        {
            if (!ColorDictionary.ContainsKey(_clr))
                ColorDictionary.Add(_clr, str);
            if (!StringDictionary.ContainsKey(str))
                StringDictionary.Add(str, _clr);
            SaveColors();
        }

        public Dictionary<SharpDX.Color, string> ReadOnlyColor
        {
            get
            {
                return ColorDictionary;
            }
        }

    }
}
