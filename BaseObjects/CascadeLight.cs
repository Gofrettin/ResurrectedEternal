using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class CascadeLight : BaseEntity
    {
        public SharpDX.Vector3 m_shadowDirection 
        { 
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_shadowDirection); } 
            set { MemoryLoader.instance.Reader.Write<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_shadowDirection, value); } 
        }
        public SharpDX.Vector3 m_envLightShadowDirection
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_envLightShadowDirection); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_envLightShadowDirection, value); }
        }
        public bool m_bEnabled
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bEnabled); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bEnabled, value); }
        }
        public bool m_bUseLightEnvAngles
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bUseLightEnvAngles); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bUseLightEnvAngles, value); }
        }
        public SharpDX.Color m_LightColor
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_LightColor); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_LightColor, value); }
        }
        public int m_LightColorScale
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_LightColorScale); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_LightColorScale, value); }
        }
        public float m_flMaxShadowDist
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flMaxShadowDist); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flMaxShadowDist, value); }
        }
        public CascadeLight(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
