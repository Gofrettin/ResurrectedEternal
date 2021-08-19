using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class SpatialEntity : BaseEntity
    {
        public SharpDX.Vector3 m_SpatialEntityVecOrigin
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_SpatialEntityVecOrigin); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_SpatialEntityVecOrigin, value); }
        }
        public float m_minFalloff
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_minFalloff); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_minFalloff, value); }
        }
        public float m_maxFalloff
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_maxFalloff); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_maxFalloff, value); }
        }
        public float m_flCurWeight
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flCurWeight); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flCurWeight, value); }
        }
        public bool m_bEnabled
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bEnabled); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bEnabled, value); }
        }
        public SpatialEntity(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
