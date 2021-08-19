using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects.Fire
{
    class Inferno : BaseEntity
    {

        //public Vector3 m_dwDeltaBounds
        //{
        //    get
        //    {
        //        return Vector3.
        //    }
        //}

        //       |__m_fireXDelta______________________________________ -> 0x09E4 (void* )
        //|__m_fireYDelta_____________________________________ -> 0x0B74 (void* ) //are thos mins max?
        // |__m_fireZDelta____________________________________ -> 0x0D04 (void* )
        //  |__m_bFireIsBurning_______________________________ -> 0x0E94 (void* )
        //   |__m_nFireEffectTickBegin________________________ -> 0x13B4 (int )
        //   |__m_fireCount___________________________________ -> 0x13A8 (int )
        public Inferno(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
