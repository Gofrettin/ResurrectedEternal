using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class World : BaseEntity
    {
//|__m_flWaveHeight____________________________________ -> 0x09D8 (float ) ???
//|__m_WorldMins_______________________________________ -> 0x09DC (Vec3 )
//|__m_WorldMaxs_______________________________________ -> 0x09E8 (Vec3 )
//|__m_bStartDark______________________________________ -> 0x09F4 (int )
//|__m_flMaxOccludeeArea_______________________________ -> 0x09F8 (float )
//|__m_flMinOccluderArea_______________________________ -> 0x09FC (float )
//|__m_flMaxPropScreenSpaceWidth_______________________ -> 0x0A04 (float )
//|__m_flMinPropScreenSpaceWidth_______________________ -> 0x0A00 (float )
//|__m_iszDetailSpriteMaterial_________________________ -> 0x0A10 (char[256] )
//|__m_bColdWorld______________________________________ -> 0x0A08 (int )
//|__m_iTimeOfDay______________________________________ -> 0x0A0C (int )
        public World(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
