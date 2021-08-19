using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class EnvDOFController : BaseEntity
    {
        public bool m_bDOFEnabled
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bDOFEnabled); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bDOFEnabled, value); }
        }
        public float m_flNearBlurDepth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNearBlurDepth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flNearBlurDepth, value); }
        }
        public float m_flNearFocusDepth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNearFocusDepth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flNearFocusDepth, value); }
        }
        public float m_flFarFocusDepth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFarFocusDepth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFarFocusDepth, value); }
        }
        public float m_flFarBlurDepth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFarBlurDepth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFarBlurDepth, value); }
        }
        public float m_flNearBlurRadius
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNearBlurRadius); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flNearBlurRadius, value); }
        }
        public float m_flFarBlurRadius
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFarBlurRadius); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFarBlurRadius, value); }
        }
        public EnvDOFController(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
