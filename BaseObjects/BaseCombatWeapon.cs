using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    public class BaseCombatWeapon : BaseEntity
    {
        public BaseCombatWeapon(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
        public short m_sItemDefinitionIndex
        {
            get { return MemoryLoader.instance.Reader.Read<short>(BaseAddress + (short)g_Globals.Offset.m_iItemDefinitionIndex); }
            set { MemoryLoader.instance.Reader.Write<short>(BaseAddress + (int)g_Globals.Offset.m_iItemDefinitionIndex, (short)value); }
        }
        public ItemDefinitionIndex m_iItemDefinitionIndex
        {
            get { return (ItemDefinitionIndex)MemoryLoader.instance.Reader.Read<short>(BaseAddress + (short)g_Globals.Offset.m_iItemDefinitionIndex); }
            set { MemoryLoader.instance.Reader.Write<short>(BaseAddress + (int)g_Globals.Offset.m_iItemDefinitionIndex, (short)value); }
        }
        public int m_iEntityLevel
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iEntityLevel); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + m_iEntityLevel, value); }
        }

        public int m_iItemIdHigh
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iItemIDHigh); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iItemIDHigh, value); }
        }
        public int m_iItemIdLow
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iItemIDLow); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iItemIDLow, value); }
        }

        public int m_iAccountId
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iAccountId); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iAccountId, value); }
        }

        public int m_iEntityQuality
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iEntityQuality); }
            set { MemoryLoader.instance.Reader.Write<int>(BaseAddress + g_Globals.Offset.m_iEntityQuality, value); }
        }


        public bool m_bInitialized
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bInitialized); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bInitialized, value); }
        }

        public string m_szCustomString
        {
            get { return MemoryLoader.instance.Reader.ReadString(BaseAddress + g_Globals.Offset.m_szCustomName, Encoding.Default, 161); }
            set { MemoryLoader.instance.Reader.WriteString(BaseAddress + g_Globals.Offset.m_szCustomName, value, Encoding.Default); }
        }

        public int m_OriginalOwnerXuidLow
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_OriginalOwnerXuidLow); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_bInitialized, value); }
        }
        public int m_OriginalOwnerXuidHigh
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_OriginalOwnerXuidHigh); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_OriginalOwnerXuidHigh, value); }
        }

        public uint m_nFallbackPaintKit
        {
            get { return MemoryLoader.instance.Reader.Read<uint>(BaseAddress + g_Globals.Offset.m_nFallbackPaintKit); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nFallbackPaintKit, value); }
        }
        public int m_nFallbackSeed
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nFallbackSeed); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nFallbackSeed, value); }
        }
        public float m_flFallbackWear
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFallbackWear); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flFallbackWear, value); }
        }
        public int m_nFallbackStatTrak
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nFallbackStatTrak); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_nFallbackStatTrak, value); }
        }

        //most likely a pointer
        public int m_iWeaponOrigin
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iWeaponOrigin); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iWeaponOrigin, value); }
        }

        public uint m_nViewModelIndex
        {
            get { return MemoryLoader.instance.Reader.Read<uint>(BaseAddress + g_Globals.Offset.m_iViewModelIndex); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_iViewModelIndex, value); }
        }

        public bool m_bInReload
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bInReload); }
        }

        public int m_iClip
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iClip); }
        }

        public int m_iClip2
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + 0x326C); }
        }

        //__m_iClip2 = 0x3268 ( int )
        public float m_flNextPrimaryAttack
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNextPrimaryAttack); }
        }
        public float m_flNextSecondaryAttack
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNextSecondaryAttack); }
        }

        public float m_fAccuracyPenalty
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fAccuracyPenalty); }
        }

        public WeaponModelClassification_t m_iState
        {
            get { return (WeaponModelClassification_t)MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iState); }
        }
        // |__m_OriginalOwnerXuidLow___________________________ -> 0x31C0 (int )
        // |__m_OriginalOwnerXuidHigh__________________________ -> 0x31C4 (int )
        // |__m_nFallbackPaintKit______________________________ -> 0x31C8 (int )
        // |__m_nFallbackSeed__________________________________ -> 0x31CC (int )
        // |__m_flFallbackWear_________________________________ -> 0x31D0 (float )
        // |__m_nFallbackStatTrak______________________________ -> 0x31D4 (int )
        //|__m_AttributeManager________________________________ -> 0x2D80 (void* )
        // |__m_hOuter_________________________________________ -> 0x2D9C (int )
        // |__m_ProviderType___________________________________ -> 0x2DA4 (int )
        // |__m_iReapplyProvisionParity________________________ -> 0x2D98 (int )
        // |__m_Item___________________________________________ -> 0x2DC0 (void* )
        //  |__m_iItemDefinitionIndex__________________________ -> 0x2FAA (int )
        //  |__m_iEntityLevel__________________________________ -> 0x2FB0 (int )
        //  |__m_iItemIDHigh___________________________________ -> 0x2FC0 (int )
        //  |__m_iItemIDLow____________________________________ -> 0x2FC4 (int )
        //  |__m_iAccountID____________________________________ -> 0x2FC8 (int )
        //  |__m_iEntityQuality________________________________ -> 0x2FAC (int )
        //  |__m_bInitialized__________________________________ -> 0x2FD4 (int )
        //  |__m_szCustomName__________________________________ -> 0x303C (char[161] )
        //  |__m_NetworkedDynamicAttributesForDemos____________ -> 0x3020 (void* )
        //   |__m_Attributes___________________________________ -> 0x3020 (void* )
        //    |__lengthproxy___________________________________ -> 0x3020 (void* )
        //     |__lengthprop32_________________________________ -> 0x3020 (int )
    }
}
