using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class PostProcessController : BaseEntity
    {
        //public float m_fLFadeTime
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fLFadeTime); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fLFadeTime, value); }
        //} 
        //public float m_flLocalContrast
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flLocalContrast); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flLocalContrast, value); }
        //}
        //public float m_flLocalContrastedStrength
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flLocalContrastedStrength); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flLocalContrastedStrength, value); }
        //}
        //public float m_flVignetteStart
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flVignetteStart); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flVignetteStart, value); }
        //}
        //public float m_flVignetteEnd
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flVignetteEnd); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flVignetteEnd, value); }
        //}
        public float m_flVignetteBlurStrength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flVignetteBlurStrength); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flVignetteBlurStrength, value); }
        }
        public float m_flFadetoblackStrength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFadetoblackStrength); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFadetoblackStrength, value); }
        }
        //public float m_flDepthblurFocalStrength
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flDepthblurFocalStrength); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flDepthblurFocalStrength, value); }
        //}
        //public float m_flDepthblurStrength
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flDepthblurStrength); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flDepthblurStrength, value); }
        //}
        //public float m_flScreenblurStrength
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flScreenblurStrength); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flScreenblurStrength, value); }
        //}
        //public float m_flFilmgrainStrength
        //{
        //    get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFilmgrainStrength); }
        //    set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFilmgrainStrength, value); }
        //}
        //public bool m_ppbMaster
        //{
        //    get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_ppbMaster); }
        //    set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_ppbMaster, value); }
        //}
        public PostProcessController(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
