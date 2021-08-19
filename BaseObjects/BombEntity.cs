using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class BombEntity : BaseEntity
    {
        public string Name = "Le Bomb";
        public BombEntity(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
        public bool m_bStartedArming
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bStartedArming); }
        }
        public bool m_bBombPlacedAnimation
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bBombPlacedAnimation); }
        }
        public float m_fArmedTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_bBombPlacedAnimation); }
        }
        public bool m_bShowC4LED
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bBombPlacedAnimation); }
        }
        public bool m_bIsPlantingViaUse
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bBombPlacedAnimation); }
        }

        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    base.Update(_localPlayer);
        //}
    }
}
