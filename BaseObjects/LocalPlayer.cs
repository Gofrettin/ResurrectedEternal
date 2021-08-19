using ResurrectedEternal.Memory;
using ResurrectedEternal.Objects;
using RRFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    public class LocalPlayer : BasePlayer
    {


        public LocalPlayer(IntPtr addr, ClientClass vmtid) : base(addr, vmtid)
        {
        }

        private IntPtr Client_dll => MemoryLoader.instance.Modules["client.dll"];

        public void BunnyJump()
        {
            int nextNum = MemoryLoader.instance.Reader.Read<int>(Client_dll + (int)g_Globals.Offset.dwForceJump);
            if (((FL_TYPE)m_fFlags).HasFlag(FL_TYPE.FL_ONGROUND))
            {
                if (nextNum == 4)
                    MemoryLoader.instance.Reader.Write(Client_dll + (int)g_Globals.Offset.dwForceJump, 5);

            }
            else
            {
                if (nextNum == 5)
                    MemoryLoader.instance.Reader.Write(Client_dll + (int)g_Globals.Offset.dwForceJump, 4);
            }
        }

        private SharpDX.Vector3 m_vPrevViewAngles = SharpDX.Vector3.Zero;

        public void AutoStrafe()
        {
            var _pth = new IntPtr(g_Globals.Offset.dwForceLeft);
            var _atr = new IntPtr(g_Globals.Offset.dwForceRight);
            if (!((FL_TYPE)m_fFlags).HasFlag(FL_TYPE.FL_ONGROUND))
            {
                if (m_vViewAngles.Y > m_vPrevViewAngles.Y)
                    MemoryLoader.instance.Reader.Write(Client_dll + g_Globals.Offset.dwForceLeft, 6);
                else if (m_vViewAngles.Y < m_vPrevViewAngles.Y)
                    MemoryLoader.instance.Reader.Write(Client_dll + g_Globals.Offset.dwForceRight, 6);
                m_vPrevViewAngles = m_vViewAngles;
            }

        }

        public bool m_bIsShooting = false;

        public void ForceAttack()
        {
            MemoryLoader.instance.Reader.Write(Client_dll + (int)g_Globals.Offset.dwForceAttack, 6);
            m_bIsShooting = true;
        }


        public bool IsShooting()
        {
            return MemoryLoader.instance.Reader.Read<int>(Client_dll + (int)g_Globals.Offset.dwForceAttack) == 5;
        }

        public void ForceAttackDown()
        {
            MemoryLoader.instance.Reader.Write(Client_dll + (int)g_Globals.Offset.dwForceAttack, 5);
        }
        public void ForceAttackUp()
        {
            MemoryLoader.instance.Reader.Write(Client_dll + (int)g_Globals.Offset.dwForceAttack, 4);
        }

        public view_matrix_t view_matrix_t
        {
            get { return MemoryLoader.instance.Reader.Read<view_matrix_t>(Client_dll + (int)g_Globals.Offset.dwViewMatrix); }
        }

        public SharpDX.Vector3 m_vaimPunchAngle
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + (int)g_Globals.Offset.m_aimPunchAngle); }
        }

        public WeaponClass GetWeaponClass(ItemDefinitionIndex _idx)
        {
            return Generators.GetWeaponType(_idx);
        }

        public WeaponClass GetWeaponId
        {
            get { return Generators.GetWeaponType(m_hActiveWeapon.m_iItemDefinitionIndex); }
        }


        public ItemDefinitionIndex GetMyWeaponsId(IntPtr _add)
        {
            return (ItemDefinitionIndex)MemoryLoader.instance.Reader.Read<short>(_add + (int)g_Globals.Offset.m_iItemDefinitionIndex);
        }


        public PredictedViewModel m_hViewModelWeapon
        {
            get { return Henker.Singleton.Client.GetViewModel(MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_hViewModel) & 0xFFF); }
        }

        public float m_dwFlashAlpha
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFlashMaxAlpha); }
            set
            {
                MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFlashMaxAlpha, value);
            }
        }

        public bool m_bCanShoot
        {
            get
            {
                if (m_hActiveWeapon == null)
                    return false;

                float time = m_nTickBase * g_Globals.m_dwGlobalVars.m_flIntervalPerTick;

                if (m_hActiveWeapon.m_flNextPrimaryAttack <= time)
                    return true;
                return false;
            }
        }


        public SharpDX.Vector3 m_vEyePosition
        {
            get { return base.m_vecViewOffset + base.m_vecOrigin; }
        }

        public SharpDX.Vector3 m_vViewAngles
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(Henker.Singleton.Engine.Pointer + (int)g_Globals.Offset.dwClientState_ViewAngles); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Vector3>(Henker.Singleton.Engine.Pointer + g_Globals.Offset.dwClientState_ViewAngles, value); }
        }

    }
}
