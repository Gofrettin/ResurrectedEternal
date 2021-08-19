using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    public class PredictedViewModel : BaseEntity
    {
        public IntPtr m_hWeapon
        {
            get { return MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + g_Globals.Offset.m_hWeapon); }
        }
        public int m_nSkin
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nSkin); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nSkin, value); }
        }
        public int m_nBody
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nBody); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nBody, value); }
        }
        public int m_nSequence
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nSequence); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nSequence, value); }
        }
        public uint m_nViewModelIndex
        {
            get { return MemoryLoader.instance.Reader.Read<uint>(BaseAddress + g_Globals.Offset.m_nViewModelIndex); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nViewModelIndex, value); }
        }
        public float m_flPlaybackRate
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flPlaybackRate); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flPlaybackRate, value); }
        }
        public int m_fEffects
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_fEffects); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_fEffects, value); }
        }
        public int m_nAnimationParity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nAnimationParity); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nAnimationParity, value); }
        }
        public IntPtr m_hOwner
        {
            get { return MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + g_Globals.Offset.m_hOwner); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_hOwner, value); }
        }
        public int m_nNewSequenceParity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nNewSequenceParity); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nNewSequenceParity, value); }
        }
        public int m_nResetEventsParity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nResetEventsParity); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nResetEventsParity, value); }
        }
        public int m_nMuzzleFlashParity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nMuzzleFlashParity); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nMuzzleFlashParity, value); }
        }
        public bool m_bShouldIgnoreOffsetAndAccuracy
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bShouldIgnoreOffsetAndAccuracy); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bShouldIgnoreOffsetAndAccuracy, value); }
        }
        public PredictedViewModel(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
