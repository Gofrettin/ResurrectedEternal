using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class ColorCorrection : SpatialEntity
    {
        public float m_flMaxWeight
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flMaxWeight); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flMaxWeight, value); }
        }
        public float m_flFadeInDuration
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFadeInDuration); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFadeInDuration, value); }
        }
        public float m_flFadeOutDuration
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFadeOutDuration); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFadeOutDuration, value); }
        }
        public string m_netLookupFilename
        {
            get { return MemoryLoader.instance.Reader.ReadString(BaseAddress + g_Globals.Offset.m_netLookupFilename, Encoding.UTF8, 260); }
            set { MemoryLoader.instance.Reader.WriteString(BaseAddress + g_Globals.Offset.m_netLookupFilename, value, Encoding.UTF8); }
        }
        public bool m_bccEnabled
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bccEnabled); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bccEnabled, value); }
        }
        public bool m_bMaster
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bMaster); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bMaster, value); }
        }
        public bool m_bClientSide
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bClientSide); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bClientSide, value); }
        }
        public bool m_bExclusive
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bExclusive); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bExclusive, value); }
        }
        public ColorCorrection(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
