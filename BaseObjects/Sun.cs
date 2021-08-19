using ResurrectedEternal.BaseObjects.SunMod;
using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class Sun : BaseEntity
    {
        //public C_SunGlowOverly m_cSunPrimaryGlowOverlay
        //{
        //    get { return MemoryLoader.instance.Reader.Read<C_SunGlowOverly>(BaseAddress + 0x9D8); }
        //    set { MemoryLoader.instance.Reader.Write(BaseAddress + 0x9D8, value); }
        //}
        //public C_SunGlowOverly m_cSunSecondaryGlowOverlay
        //{
        //    get { return MemoryLoader.instance.Reader.Read<C_SunGlowOverly>(BaseAddress + 0xA88); }
        //    set { MemoryLoader.instance.Reader.Write(BaseAddress + 0xA88, value); }
        //}

        public bool HasValidPointers
        {
            get
            {
                if (m_dwSunOverlayPrimary == null || m_dwSunOverlaySecondary == null)
                {
                    Create();
                    return false;
                }
                else if (!m_dwSunOverlayPrimary.IsValid || !m_dwSunOverlaySecondary.IsValid)
                {
                    Create();
                    return false;
                }

                return true;
            }
        }

        public SunOverlay m_dwSunOverlayPrimary;

        public SunOverlay m_dwSunOverlaySecondary;

        public SharpDX.Color m_clrOverlay
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_clrOverlay); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_clrOverlay, value); }
        }

        public SharpDX.Vector3 m_vDirection
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_vDirection); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_vDirection, value); }
        }
        public bool m_bOn
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bOn); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bOn, value); }
        }
        public byte m_nSize
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nSize); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nSize, value); }
        }
        public byte m_nOverlaySize
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nOverlaySize); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nOverlaySize, value); }
        }
        public int m_nMaterial
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nMaterial); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nMaterial, value); }
        }
        public int m_nOverlayMaterial
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nOverlayMaterial); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nOverlayMaterial, value); }
        }

        public void Create()
        {
            m_dwSunOverlayPrimary = new BaseObjects.SunMod.SunOverlay(BaseAddress + g_Globals.Offset.m_dwPrimaryGlowOverlay);
            m_dwSunOverlaySecondary = new BaseObjects.SunMod.SunOverlay(BaseAddress + g_Globals.Offset.m_dwSecondaryGlowOverlay);
        }

        public Sun(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {

        }
    }
}
