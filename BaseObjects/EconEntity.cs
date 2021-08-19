using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class EconEntity : BaseEntity
    {
        public bool m_bShouldGlow
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow, value); }
        }
        public EconEntity(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
