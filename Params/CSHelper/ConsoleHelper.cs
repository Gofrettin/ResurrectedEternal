using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Params.CSHelper
{
    public static class ConsoleHelper
    {
        private static ConsoleColor StandardColor = ConsoleColor.White;
        public static ConsoleColor ConfirmationColor = ConsoleColor.Green;
        public static ConsoleColor ErrorColor = ConsoleColor.Red;

        public static string CreatePad(string value, int min)
        {
            var _diff = value.Length - min;
            string _pad = "";
            for (int i = 0; i < _diff; i++)
            {
                _pad += " ";
            }
            return value + _pad;
        }

        private static string CreatePad(int num)
        {
            string _pad = "";
            for (int i = 0; i < num; i++)
                _pad += " ";
            return _pad;
        }

        public static void Write(string msg)
        {
            Console.Write(msg);
        }

        public static void Write(string msg, int pad)
        {
            var _padding = CreatePad(pad);
            Write(_padding + msg);
        }

        public static void Write(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Write(msg);
            Console.ForegroundColor = StandardColor;
        }

        public static void Write(string msg, int pad, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var _pad = CreatePad(pad);
            Write(_pad + msg);
            Console.ForegroundColor = StandardColor;
        }

        private static string _preString = "";

        public static void WriteAndRemove(string newString)
        {
            Clear(_preString.Length);
            _preString = newString;
            Write(newString);
        }
        public static void ShowAction(string input, int pad = 33)
        {
            var _diff = 0;
            if (input.Length < pad) _diff = pad - input.Length;
            for (int i = 0; i < _diff; i++)
                input += " ";
            Console.Write("   " + input);
        }
        public static void ShowAction(string input, int pad = 33, ConsoleColor _clr = ConsoleColor.White)
        {
            Console.ForegroundColor = _clr;
            ShowAction(input, pad);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConfirmAction(string input, ConsoleColor _clr = ConsoleColor.Green)
        {
            Console.ForegroundColor = _clr;
            Console.Write(input + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Clear(int num)
        {
            for (int i = 0; i < num; i++)
                Console.Write("\b \b");
        }

        public static string CaptureInput()
        {
            return Console.ReadLine();
        }

        public static string CaptureInput(string prepend)
        {
            Write(prepend + ": ");
            return CaptureInput();
        }
        public static string ColorCaptureInput(string prepend)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Write(prepend + ": ");
            Console.ForegroundColor = ConsoleColor.White;
            return CaptureInput();
            
        }

        public static string CaptureInputAndRemove(string prepend)
        {
            var _input = CaptureInput(prepend);
            Clear(_input.Length);
            return _input;
        }

        public static string CaptureInputAndRemove()
        {
            var _input = CaptureInput();
            Clear(_input.Length);
            return _input;
        }

        public static bool CaptureKey(ConsoleKey expected)
        {
            Console.Write(expected.ToString() + " to continue.\n");
            var _input = Console.ReadKey();
            Console.Write("\n");
            if (_input.Key == expected)
                return true;
            return false;
        }

        public static string GetValue(string input, char MarkUp)
        {
            var charArr = input.ToArray();
            int start = 0;
            for (int i = 0; i < charArr.Length; i++)
                if(charArr[i] == MarkUp)
                {
                    start = i + 1;
                    break;
                }
            var _next = input.Remove(0, start);
            charArr = _next.ToArray();

            int end = 0;
            for (int i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == MarkUp)
                {
                    end = i;
                    break;
                }
            }

            return _next.Remove(end, _next.Length - end);
        }

        public static string[] CreateParameters(char delimiter)
        {
            var _input = CaptureInput();
            return _input.Split(delimiter);
        }
        public static string[] CreateParameteres(string _input, char delimiter)
        {
            return _input.Split(delimiter);
        }

    }
}
