using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class GroundWeapon : BaseEntity
    {
        //public int m_iGlowIndex 
        //{
        //    get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_iGlowIndex); }
        //}

//        DT_BaseWeaponWorldModel
//|__m_nModelIndex_____________________________________ -> 0x0258 (int )
//|__m_nBody___________________________________________ -> 0x0A20 (int )
//|__m_fEffects________________________________________ -> 0x00F0 (int )
//|__moveparent________________________________________ -> 0x0148 (int )
//|__m_hCombatWeaponParent_____________________________ -> 0x29F0 (int )
        public GroundWeapon(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }

        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    //base.Update(_localPlayer);
        //    //if (!IsValid)
        //    //    return;
        //    //GlowIndex = MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_iGlowIndex);
        //}
    }
}
