using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class ShadowControl : BaseEntity
    {
        public SharpDX.Vector3 m_shadowDirection
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_shadowDirection); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_shadowDirection, value); }
        }
        public SharpDX.Color m_shadowColor
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_shadowColor); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_shadowColor, value); }
        }
        public float m_flShadowMaxDist
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flShadowMaxDist); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flShadowMaxDist, value); }
        }
        public bool m_bDisableShadows
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bDisableShadows); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bDisableShadows, value); }
        }
        public bool m_bEnableLocalLightShadows
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bEnableLocalLightShadows); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bEnableLocalLightShadows, value); }
        }
        public ShadowControl(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
