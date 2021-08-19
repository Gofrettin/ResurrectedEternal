using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class BeamEnd : BaseEntity
    {
        //        |__m_hOwnerEntity___________________________________ -> 0x014C (int )
        //|__m_hEffectEntity__________________________________ -> 0x0998 (int )
        public int m_hOwnerEntity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + 0x014C) & 0xFFF; }
        }
        public int m_hEffectEntity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + 0x0998) & 0xFFF; }
        }
        public int m_iParentAttachment
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iParentAttachment); }
        }
        public float m_flLightScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flLightScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flLightScale, value); }
        }
        public float m_Radius
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_Radius); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_Radius, value); }
        }
        public BeamEnd(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
