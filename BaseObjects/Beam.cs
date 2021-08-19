using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    public class Beam : BaseEntity
    {
        public BeamType m_nBeamType
        {
            get { return (BeamType)MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nBeamType); }
            set { MemoryLoader.instance.Reader.Write<byte>(BaseAddress + g_Globals.Offset.m_nBeamFlags, Convert.ToByte(value)); }
        }
        public Beam_Flags_h m_nBeamFlags
        {
            get { return (Beam_Flags_h)MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nBeamFlags); }
            set { MemoryLoader.instance.Reader.Write<byte>(BaseAddress + g_Globals.Offset.m_nBeamFlags, Convert.ToByte(value)); }
        }
        public byte m_nNumBeamEnts
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nNumBeamEnts); }
            set { MemoryLoader.instance.Reader.Write<byte>(BaseAddress + g_Globals.Offset.m_nNumBeamEnts, value); }
        }
        public int m_hAttachEntity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_hAttachEntity) & 0xFFF; }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_hAttachEntity, value); }
        }
        public int m_nAttachIndex
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nAttachIndex); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_nAttachIndex, value); }
        }
        public byte m_nHaloIndex
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nHaloIndex); }
            set { MemoryLoader.instance.Reader.Write<byte>(BaseAddress + g_Globals.Offset.m_nHaloIndex, value); }
        }
        public float m_fHaloScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fHaloScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fHaloScale, value); }
        }
        public float m_fWidth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fWidth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fWidth, value); }
        }
        public float m_fEndWidth
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fEndWidth); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fEndWidth, value); }
        }
        public float m_fFadeLength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fFadeLength); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fFadeLength, value); }
        }
        public float m_fAmplitude
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fAmplitude); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fAmplitude, value); }
        }
        public float m_fStartFrame
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fStartFrame); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fStartFrame, value); }
        }
        public float m_fSpeed
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fSpeed); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_fSpeed, value); }
        }
        public float m_flFrameRate
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFrameRate); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flFrameRate, value); }
        }
        public float m_flBeamHDRColorScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flBeamHDRColorScale); }
            set { MemoryLoader.instance.Reader.Write<float>(BaseAddress + g_Globals.Offset.m_flBeamHDRColorScale, value); }
        }
        public Beam(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
