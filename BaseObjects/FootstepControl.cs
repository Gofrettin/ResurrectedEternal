using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class FootstepControl : BaseEntity
    {
//|__m_vecFinalDest____________________________________ -> 0x09EC (Vec3 )
//|__m_movementType____________________________________ -> 0x09F8 (int )
//|__m_flMoveTargetTime________________________________ -> 0x09FC (float )
//|__m_bClientSidePredicted____________________________ -> 0x0A08 (int )
//|__m_spawnflags______________________________________ -> 0x02C8 (int )
//|__m_source__________________________________________ -> 0x0A10 (char[16] )
//|__m_destination_____________________________________ -> 0x0A20 (char[16] )
        public FootstepControl(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
