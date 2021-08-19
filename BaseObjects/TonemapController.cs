using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{

    //|__m_bUseCustomAutoExposureMin_______________________ -> 0x09D8 (int )
    //|__m_bUseCustomAutoExposureMax_______________________ -> 0x09D9 (int )
    //|__m_bUseCustomBloomScale____________________________ -> 0x09DA (int )
    //|__m_flCustomAutoExposureMin_________________________ -> 0x09DC (float )
    //|__m_flCustomAutoExposureMax_________________________ -> 0x09E0 (float )
    //|__m_flCustomBloomScale______________________________ -> 0x09E4 (float )
    //|__m_flCustomBloomScaleMinimum_______________________ -> 0x09E8 (float )
    //|__m_flBloomExponent_________________________________ -> 0x09EC (float )
    //|__m_flBloomSaturation_______________________________ -> 0x09F0 (float )
    //|__m_flTonemapPercentTarget__________________________ -> 0x09F4 (float )
    //|__m_flTonemapPercentBrightPixels____________________ -> 0x09F8 (float )
    //|__m_flTonemapMinAvgLum______________________________ -> 0x09FC (float )
    //|__m_flTonemapRate___________________________________ -> 0x0A00 (float )

    class TonemapController : BaseEntity
    {
        public bool m_bUseCustomAutoExposureMin
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomAutoExposureMin); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomAutoExposureMin, value); }
        }
        public bool m_bUseCustomAutoExposureMax
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomAutoExposureMax); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomAutoExposureMax, value); }
        }
        public bool m_bUseCustomBloomScale
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomBloomScale); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + (int)g_Globals.Offset.m_bUseCustomBloomScale, value); }
        }
        public float m_flCustomAutoExposureMin
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomAutoExposureMin); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomAutoExposureMin, value); }
        }
        public float m_flCustomAutoExposureMax
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomAutoExposureMax); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomAutoExposureMax, value); }
        }
        public float m_flCustomBloomScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomBloomScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomBloomScale, value); }
        }
        public float m_flCustomBloomScaleMinimum
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomBloomScaleMinimum); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flCustomBloomScaleMinimum, value); }
        }
        public float m_flBloomExponent
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flBloomExponent); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flBloomExponent, value); }
        }
        public float m_flBloomSaturation
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flBloomSaturation); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flBloomSaturation, value); }
        }
        public float m_flTonemapPercentTarget
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapPercentTarget); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapPercentTarget, value); }
        }
        public float m_flTonemapPercentBrightPixels
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapPercentBrightPixels); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapPercentBrightPixels, value); }
        }
        public float m_flTonemapMinAvgLum
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapMinAvgLum); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapMinAvgLum, value); }
        }
        public float m_flTonemapRate
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapRate); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flTonemapRate, value); }
        }

        public TonemapController(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
            //defaultValues = MemoryLoader.instance.Reader.Read<CENVTONEMAPPER>(BaseAddress);
        }
        //public CENVTONEMAPPER defaultValues
        //{
        //    private set; get;
        //}



        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    base.Update(_localPlayer);

        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09D8, true);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09D9, true);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09DA, true);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09DC, (float)g_Globals.Config.MovieConfig.CustomAutoExposureMin.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09E0, (float)g_Globals.Config.MovieConfig.CustomAutoExposureMax.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09E4, (float)g_Globals.Config.MovieConfig.CustomBloomScale.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09E8, (float)g_Globals.Config.MovieConfig.CustomBloomScaleMin.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09EC, (float)g_Globals.Config.MovieConfig.BloomExponent.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09F0, (float)g_Globals.Config.MovieConfig.BloomSaturation.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09F4, (float)g_Globals.Config.MovieConfig.TonemapPercentTarget.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09F8, (float)g_Globals.Config.MovieConfig.TonemapPercentBrightPixels.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x09FC, (float)g_Globals.Config.MovieConfig.TonemapMinAvgLum.Value);
        //    MemoryLoader.instance.Reader.Write(BaseAddress + 0x0A00, (float)g_Globals.Config.MovieConfig.TonemapRate.Value);

        //}

        private void WriteCustomBloomScale()
        {
            //[FieldOffset(0x09E4)]
            //public float m_flCustomBloomScale;
           
        }

        private void WriteCustomBloomExponent()
        {
            //[FieldOffset(0x09EC)]
            //public float m_flBloomExponent;
            //[FieldOffset(0x09F0)]
            //public float m_flBloomSaturation;

        }

        //       g_bUseCustomAutoExposureMax = false;
        //g_bUseCustomBloomScale = false;

        //g_flBloomExponent = 2.5f;
        //g_flBloomSaturation = 1.0f;
        //g_flTonemapPercentTarget = 65.0f;
        //g_flTonemapPercentBrightPixels = 2.0f;
        //g_flTonemapMinAvgLum = 3.0f;
        //g_flTonemapRate = 1.0f;
    }
}
