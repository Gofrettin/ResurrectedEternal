using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class BaseGrenade : BaseEntity
    {
        public BaseGrenade(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
        public float m_flDamage { 
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flDamage); } 
        }
        public float m_DmgRadius
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_DmgRadius); }
        }
        public bool m_bIsLive
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bIsLive); }
        }
        public int m_hThrower
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_hThrower) & 0xFFF; }
        }
    }
}
