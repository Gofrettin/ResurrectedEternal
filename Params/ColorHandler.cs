using ResurrectedEternal.Globals;
using ResurrectedEternal.Params.CSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params
{
    class ColorHandler
    {
        private ColorManager ColorManager => g_Globals.ColorManager;

        public ColorHandler()
        {

        }

        public void HandleColor(string input)
        {

            var _input = ConsoleHelper.CreateParameteres(input, ' ');

            /*
             * 0 = color
             * 1 = add / remove
             * 2 3 4 5 = R G B A
             * 6 = name
             * 
             */


            var _action = _input[1];

            if (_action == "add")
            {
                bool _generateName = true;
                //we dont have a name
                if (_input.Length < 6)
                {
                    ConsoleHelper.Write("Error!\nMissing Params -> e.g: color add 255 255 255 255 white\n", ConsoleColor.Red);
                    return;
                }
                if (_input.Length == 7)
                {
                    _generateName = false;
                }
                var _colorName = "";

                if (_generateName)
                    _colorName = Generators.GetRandomString(5);
                else
                    _colorName = _input[6];


                //var _colorArray = 
                if (GetColor(_input, out var _newColor))
                {
                    if (ColorManager.DoesColorExist(_colorName)
                        || ColorManager.DoesColorExist(_newColor))
                    {
                        Console.WriteLine("Color already exists. Overwrite color?");
                        if (!ConsoleHelper.CaptureKey(ConsoleKey.Y))
                            return;
                    }

                    ColorManager.AddColor(_newColor, _colorName);
                    ConsoleHelper.Write(string.Format("Success!\n Color {0} successfully added!", _colorName), ConsoleColor.Green);
                    return;
                }
                else
                    return;
            }
            else if(_action == "show")
            {
                foreach (var item in ColorManager.ReadOnlyColor)
                {
                    Console.WriteLine(item.Value + " " + item.Key.ToString());
                }
                return;
            }
            else
                ConsoleHelper.Write(string.Format("Error!\nUnknown Command {0}", _action), ConsoleColor.Red);

            /*
             * 0 = color
             * 1 = add or remove
             */




        }
        private bool GetColor(string[] strs, out SharpDX.Color _newColor)
        {

            _newColor = new SharpDX.Color();
            var _colorcode = new string[4];
            List<byte> _colorBytes = new List<byte>();
            Array.Copy(strs, 2, _colorcode, 0, 4);
            foreach (var item in _colorcode)
            {
                if (!byte.TryParse(item, out var clrcd))
                {
                    ConsoleHelper.Write("Error!\nCould not parse colorcode for " + item + "\n", ConsoleColor.Red);
                    return false;
                }
                _colorBytes.Add(clrcd);
            }

            if (_colorBytes.Count > 4)
            {
                ConsoleHelper.Write("Error!\nCount Mismatch! Expected 4 -> Result " + _colorBytes.Count, ConsoleColor.Red);
                return false;
            }

            _newColor = new SharpDX.Color(_colorBytes[0],
                _colorBytes[1],
                _colorBytes[2],
                _colorBytes[3]
                );

            return true;
        }


    }
}
