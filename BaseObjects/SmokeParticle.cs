using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class SmokeParticle : BaseEntity
    {
        //        |__m_flSpawnTime_____________________________________ -> 0x0AD4 (float )
        //|__m_FadeStartTime___________________________________ -> 0x0AD8 (float )
        //|__m_FadeEndTime_____________________________________ -> 0x0ADC (float )
        //|__m_MinColor________________________________________ -> 0x0AE4 (Vec3 )
        //|__m_MaxColor________________________________________ -> 0x0AF0 (Vec3 )
        //|__m_CurrentStage____________________________________ -> 0x0AC4 (int )
        public SmokeParticle(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
