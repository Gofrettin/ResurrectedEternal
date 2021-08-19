using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Memory;
using ResurrectedEternal.MemoryManager.PatMod;
using ResurrectedEternal.Params.CSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.MemoryManager
{
    public class NetVarManager
    {
        public NetVarManager()
        {
            
            Init();
        }

        private void Init()
        {
            ScanOffsets();
            Walk();
            CreateDump();
        }
        private void ScanOffsets()
        {
            ConsoleHelper.ShowAction("Scanning Signatures...", 33);
            
            //var _getOffsetPatterns = RPC.Request(112);

            var _sigs = Serializer.LoadJson<ModulePattern[]>(g_Globals.Signatures);

            foreach (var item in _sigs)
            {
                if (MemoryLoader.instance.Reader.DumpModule(MemoryLoader.instance.ProcessModules[item.ModuleName]))
                {
                    foreach (var _off in item.Patterns)
                    {
                        ApplyOffset(_off.Name, Read(_off));
                    }
                }
            }
            ConsoleHelper.ConfirmAction("OK!");
        }

        private int Read(SerialPattern _pattern)
        {
            RedRain.ScanFlags _flags = RedRain.ScanFlags.NONE;

            if (_pattern.Relative)
                _flags = _flags | RedRain.ScanFlags.SUBSTRACT_BASE;
            if (!_pattern.SubtractOnly)
                _flags = _flags | RedRain.ScanFlags.READ;
                
            return MemoryLoader.instance.Reader.FindOffset(_pattern.Pattern, _flags, _pattern.Offset, _pattern.Extra);
        }

        private void ApplyOffset(string name, int offset)
        {
            var _ok = g_Globals.Offset.GetType();
            FieldInfo propertyInfo = g_Globals.Offset.GetType().GetField(name);
            propertyInfo.SetValue(g_Globals.Offset, offset);
            //ConsoleHelper.Write(name + ":: 0x" + new IntPtr(offset).ToString("x").ToUpper() + "\n", 33, ConsoleColor.Cyan);
        }

        private void ReadTableEx(RecvTable _table)
        {
            var _tblname = MemoryLoader.instance.Reader.ReadString(_table.m_pNetTableName, Encoding.UTF8);
            for (int i = 0; i < _table.m_nProps; i++)
            {
                var _prop = MemoryLoader.instance.Reader.Read<RecvProp>(new IntPtr((int)_table.m_pProps + (i * 0x3C)));
                var _name = MemoryLoader.instance.Reader.ReadString(new IntPtr(_prop.m_pVarName), Encoding.UTF8);
                if (_prop.m_RecvType == ePropType.DataTable)
                    ReadTableEx(MemoryLoader.instance.Reader.Read<RecvTable>((IntPtr)_prop.m_pDataTable));
                if (_prop.m_Offset == 0)
                    continue;
                if (!g_Globals.NetVars.ContainsKey(_tblname + "::" + _name))
                {
                    g_Globals.NetVars.Add(_tblname + "::" + _name, _prop.m_Offset);
                }
            }
        }
        private void Walk()
        {
            ConsoleHelper.ShowAction("Reading Netvars...", 33);
            //dont deref the pointer for all classes
            var _firstclass = MemoryLoader.instance.Modules["client.dll"] + g_Globals.Offset.m_dwGetAllClasses;
            do
            {
                var _n = MemoryLoader.instance.Reader.Read<ClientClass_t>(_firstclass);
                var _recvTable = MemoryLoader.instance.Reader.Read<RecvTable>(_n.m_pRecvTable);
                ReadTableEx(_recvTable);

                _firstclass = _n.m_pNext; // MemoryLoader.instance.Reader.Read<IntPtr>();
            } while (_firstclass != IntPtr.Zero);

            ConsoleHelper.ConfirmAction(string.Format("OK! [{0} Total]", g_Globals.NetVars.Count), ConsoleColor.Green);

        }

        private void CreateDump()
        {
            var s = new StringBuilder();
            foreach (var item in g_Globals.NetVars)
            {
                s.AppendLine(item.Key + " : " + "0x" + item.Value.ToString("x").ToUpper());
            }
            System.IO.File.WriteAllText("netvardump.txt", s.ToString());
        }

    }
}
