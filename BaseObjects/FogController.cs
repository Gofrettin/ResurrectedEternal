using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class FogController : BaseEntity
    {
        public bool m_bFogEnable
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bFogEnable); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bFogEnable, value); }
        }
        public bool m_bFogblend
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_iFogblend); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_iFogblend, value); }
        }

        public SharpDX.Vector3 m_vFogdirPrimary
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_vFogdirPrimary); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_vFogdirPrimary, value); }
        }
        public SharpDX.Color m_cFogcolorPrimary
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_cFogcolorPrimary); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_cFogcolorPrimary, value); }
        }
        public SharpDX.Color m_cFogcolorSecondary
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_cFogcolorSecondary); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Color>(BaseAddress + g_Globals.Offset.m_cFogcolorSecondary, value); }
        }
        public float m_fFogstart
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogstart); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogstart, value); }
        }
        public float m_fFogend
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogend); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogend, value); }
        }
        public float m_fFogfarz
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogfarz); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogfarz, value); }
        }
        public float m_fFogmaxdensity
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogmaxdensity); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogmaxdensity, value); }
        }
        public _CColor m_fFogcolorPrimaryLerpTo
        {
            get { return MemoryLoader.instance.Reader.Read<_CColor>(BaseAddress + g_Globals.Offset.m_fFogcolorPrimaryLerpTo); }
            set { MemoryLoader.instance.Reader.Write<_CColor>(BaseAddress + g_Globals.Offset.m_fFogcolorPrimaryLerpTo, value); }
        }
        public _CColor m_fFogcolorSecondaryLerpTo
        {
            get { return MemoryLoader.instance.Reader.Read<_CColor>(BaseAddress + g_Globals.Offset.m_fFogcolorSecondaryLerpTo); }
            set { MemoryLoader.instance.Reader.Write<_CColor>(BaseAddress + g_Globals.Offset.m_fFogcolorSecondaryLerpTo, value); }
        }
        public float m_fFogstartLerpTo
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogstartLerpTo); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogstartLerpTo, value); }
        } 
        public float m_fFogendLerpTo
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogendLerpTo); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogendLerpTo, value); }
        }
        public float m_fFogmaxdensityLerpTo
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogmaxdensityLerpTo); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogmaxdensityLerpTo, value); }
        }
        public float m_fFoglerptime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFoglerptime); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFoglerptime, value); }
        }
        public float m_fFogduration
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogduration); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogduration, value); }
        }
        public float m_fFogHDRColorScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogHDRColorScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogHDRColorScale, value); }
        }
        public float m_fFogZoomFogScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFogZoomFogScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFogZoomFogScale, value); }
        }
        public FogController(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
