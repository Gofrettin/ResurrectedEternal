using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class Wind : BaseEntity
    {
        public IntPtr m_EnvWindShared
        { 
            get { return MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + g_Globals.Offset.m_EnvWindShared); } 
        } 
        public int m_iMinWind
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMinWind); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iMinWind, value); }
        }
        public int m_iMaxWind
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMaxWind); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iMaxWind, value); }
        }
        public int m_iMinGust
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMinGust); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iMinGust, value); }
        }
        public int m_iMaxGust
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMaxGust); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iMaxGust, value); }
        }
        public float m_flMinGustDelay
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flMinGustDelay); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flMinGustDelay, value); }
        }
        public float m_flMaxGustDelay
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flMaxGustDelay); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flMaxGustDelay, value); }
        }
        public int m_iGustDirChange
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iGustDirChange); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iGustDirChange, value); }
        }
        public int m_iWindSeed
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iWindSeed); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iWindSeed, value); }
        }
        public int m_iInitialWindDir
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iInitialWindDir); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iInitialWindDir, value); }
        }
        public float m_flInitialWindSpeed
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flInitialWindSpeed); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flInitialWindSpeed, value); }
        }
        public float m_flStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flStartTime); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flStartTime, value); }
        }
        public float m_flGustDuration
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flGustDuration); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flGustDuration, value); }
        }
        public Wind(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
