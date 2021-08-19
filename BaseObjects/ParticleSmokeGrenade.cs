using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class ParticleSmokeGrenade : BaseEntity
    {
        //|__m_flSpawnTime_____________________________________ -> 0x0AD4 (float )
        //|__m_FadeStartTime___________________________________ -> 0x0AD8 (float )
        //|__m_FadeEndTime_____________________________________ -> 0x0ADC (float )
        //|__m_MinColor________________________________________ -> 0x0AE4 (Vec3 )
        //|__m_MaxColor________________________________________ -> 0x0AF0 (Vec3 )
        //|__m_CurrentStage____________________________________ -> 0x0AC4 (int )
        public float m_flSpawnTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flSpawnTime); }
        }
        public float m_FadeStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_FadeStartTime); }
        }
        public float m_FadeEndTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_FadeEndTime); }
        }
        public Vector3 m_MinColor
        {
            get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress + g_Globals.Offset.m_MinColor); }
        }
        public Vector3 m_MaxColor
        {
            get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress + g_Globals.Offset.m_MaxColor); }
        }
        public int m_CurrentStage
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_CurrentStage); }
        }
        public ParticleSmokeGrenade(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
