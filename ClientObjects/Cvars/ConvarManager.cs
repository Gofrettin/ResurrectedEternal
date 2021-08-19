using RedRain;
using ResurrectedEternal.Memory;
using ResurrectedEternal.Params.CSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects.Cvars
{

    //kNUM buckets?
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharCodes
    {
        //266 ?
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] tab;
    };

    

    public class ConvarManager
    {
        public static ConvarManager instance;
        public IntPtr pThis;
        public CharCodes codes;

        public ConvarManager()
        {
            codes = MemoryLoader.instance.Reader.Read<CharCodes>(MemoryLoader.instance.Modules["vstdlib.dll"] + (int)g_Globals.Offset.m_dwConvarTable);
            pThis = MemoryLoader.instance.Reader.Read<IntPtr>(MemoryLoader.instance.Modules["vstdlib.dll"] + (int)g_Globals.Offset.m_engineCvar);
            Walk();
            instance = this;
        }

        public string[] GetConvars()
        {
            return Convars.Values.Select(x => Generators.EndPadString(x.m_pszName, 45)
                    + " " + x.m_pszDefaultValue
                    + " >> n Value: " + x.m_nValue
                    + " >> f Value: " + x.m_flValue
                    + " >> s Value: " + x.m_pszValue).ToArray();
        }
        public string[] GetCmds()
        {
            return ClientCommands.Values.Select(x => Generators.EndPadString(x.m_pszName, 40) + " " + x.m_pszDefaultValue + " " + x.TypeOf.Name).ToArray();
        }
        public ConvarEntity GetConvar(string convar)
        {
            if (Convars.ContainsKey(convar))
                return Convars[convar];
            return null;
        }

        public string[] SearchConvarPattern(string pattern)
        {
            //List<string> _cons = ToList();
            //foreach (var item in Cons)
            //{
            //    if (item.Key.Contains(pattern))
            //        _cons.Add(item.Key);
            //}
            var _selection = Convars.Values.Where(x => x.m_pszName.Contains(pattern)).ToArray();
            List<string> _strings = new List<string>();
            foreach (var item in _selection)
            {
                _strings.Add(Generators.EndPadString(item.m_pszName, 40)
                    + " " + item.m_pszDefaultValue
                    + " >> n Value: " + item.m_nValue
                    + " >> f Value: " + item.m_flValue
                    + " >> s Value: " + item.m_pszValue);
            }
            return _strings.ToArray();
        }
        public string[] SearchCmdPattern(string pattern)
        {
            //List<string> _cons = ToList();
            //foreach (var item in Cons)
            //{
            //    if (item.Key.Contains(pattern))
            //        _cons.Add(item.Key);
            //}
            var _selection = ClientCommands.Values.Where(x => x.m_pszName.Contains(pattern)).ToArray();
            List<string> _strings = new List<string>();
            foreach (var item in _selection)
            {
                _strings.Add(Generators.EndPadString(item.m_pszName, 40) + " " + item.m_pszDefaultValue + " >>> " + item.TypeOf.Name);
            }
            return _strings.ToArray();
        }
        //public IntPtr Find(string a)
        //{
        //    //var shortCut = 
        //}

        private Dictionary<string, ConvarEntity> Convars = new Dictionary<string, ConvarEntity>();
        private Dictionary<string, ConvarEntity> ClientCommands = new Dictionary<string, ConvarEntity>();

        public void Walk()
        {
            ConsoleHelper.ShowAction("Walking convar table...", 33);
            IntPtr hashmapEntry = MemoryLoader.instance.Reader.Read<IntPtr>(MemoryLoader.instance.Reader.Read<IntPtr>(pThis + 0x34));

            var _convarList = new List<ConvarEntity>();

            while (hashmapEntry != IntPtr.Zero)
            {
                var pConvar = MemoryLoader.instance.Reader.Read<IntPtr>(hashmapEntry + 4); // Entry
                if (pConvar == IntPtr.Zero) break;

                ConvarEntity _convar = new ConvarEntity(pConvar);
                if (!_convarList.Contains(_convar))
                    _convarList.Add(_convar);
 //               if (!string.IsNullOrEmpty(_convar.m_pszDefaultValue))
//                    Console.WriteLine(Generators.EndPadString(_convar.m_pszName, 40) + " " + Generators.EndPadString(_convar.m_pszDefaultValue, 10) + " type of " + _convar.TypeOf.Name);
                hashmapEntry = MemoryLoader.instance.Reader.Read<IntPtr>(hashmapEntry + 4); // Next
            }
            _convarList = _convarList.OrderByDescending(x => x.m_pszName).ToList();
            foreach (var item in _convarList)
                if (string.IsNullOrEmpty(item.m_pszDefaultValue))
                    ClientCommands.Add(item.m_pszName.ToString().ToLower(), item);
                else
                    Convars.Add(item.m_pszName.ToString().ToLower(), item);
            ConsoleHelper.ConfirmAction(string.Format("OK! [{0} Convars | {1} Cmds]", Convars.Count, ClientCommands.Count));
            //ConsoleHelper.Write(string.Format("Loaded total of {0} convars and {1} client commands.\n", Convars.Count, ClientCommands.Count), 3);
        }

        public ConvarEntity GetConvarEntity(string name)
        {
            if (Convars.ContainsKey(name))
                return Convars[name];
            else return null;
        }

        public IntPtr GetConVarAddress(string name)
        {
            if (Convars.ContainsKey(name))
                return Convars[name].m_pThis;

            var hash = GetStringHash(name);

            IntPtr Pointer = MemoryLoader.instance.Reader.Read<IntPtr>(MemoryLoader.instance.Reader.Read<IntPtr>(pThis + 0x34) + ((byte)hash * 4));
            while (Pointer != IntPtr.Zero)
            {
                if (MemoryLoader.instance.Reader.Read<int>(Pointer) == hash)
                {
                    IntPtr ConVarPointer = MemoryLoader.instance.Reader.Read<IntPtr>(Pointer + 0x4);

                    if (MemoryLoader.instance.Reader.ReadString(MemoryLoader.instance.Reader.Read<IntPtr>(ConVarPointer + 0xC), Encoding.Default).Trim('\0') == name)
                        return ConVarPointer;
                }
                Pointer = MemoryLoader.instance.Reader.Read<IntPtr>(Pointer + 0xC);
            }
            return IntPtr.Zero;
        }

        public int GetStringHash(string name)
        {
            int v2 = 0;
            int v3 = 0;
            for (int i = 0; i < name.Length; i += 2)
            {
                v3 = codes.tab[v2 ^ char.ToUpper(name[i])];
                if (i + 1 == name.Length)
                    break;
                v2 = codes.tab[v3 ^ char.ToUpper(name[i + 1])];
            }
            return v2 | (v3 << 8);
        }

    }
}
