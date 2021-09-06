using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.ClientObjects.Other;
using ResurrectedEternal.Memory;
using RRFull;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{

    //public class WeightedBone
    //{
    //    public float

    //    public Vector3 

    //    public WeightedBone(IntPtr _address)
    //    {

    //    }
    //}

    public class BasePlayer : BaseEntity
    {
        private uint V = 0xFFFFFFFF;

        public BasePlayer(IntPtr addr, ClientClass cid) : base(addr, cid)
        {

            //BaseAddress = addr;
            //CachedId = Id;
        }

        public BaseCombatWeapon[] m_hMyWeapons
        {
            get
            {
                var _r = MemoryLoader.instance.Reader.Read<mweapon_indexes_t>(BaseAddress + g_Globals.Offset.m_hMyWeapons)._pointers;
                for (int i = 0; i < _r.Length; i++)
                    _r[i] = _r[i] & 0xFFF;
                return Henker.Singleton.Client.GetWeaponById(_r.Where(x => (x & 0xFFF) < 4095).ToArray());
            }
        }
        public void Update()
        {

        }

        //}
        public new bool m_bIsActive
        {
            get
            {
                if (IsNullPtr)
                    return false;
                if (m_bDormant)
                    return false;
                if (!IsValid)
                    return false;
                if (m_iHealth <= 0)
                    return false;
                if (!m_bIsAlive)
                    return false;
                if (!ValidBoneMatrix)
                    return false;
                return true;
            }
        }
        public bool IsFriendly(PlayerTeam _localPlayerTeam)
        {
            return _localPlayerTeam == Team;
        }

        private PlayerInfo m_playerInfo;
        public string m_szPlayerName
        {
            get
            {
                if (m_playerInfo == null)
                {
                    if (ReadNamePlayerInfo(m_iIndex, out var _info))
                    {
                        m_playerInfo = new PlayerInfo(
                            Generators.ParseChars(_info.m_szPlayerName),
                             Generators.ParseChars(_info.m_szFriendsName),
                              Generators.ParseChars(_info.m_szSteamID));
                        return m_playerInfo.PlayerName;
                    }
                    else
                        return "INVALIDHANDLE";
                }
                else
                {
                    return m_playerInfo.PlayerName;
                }
            }
        }

        public string m_szPlayerSteamId
        {
            get
            {
                if (m_playerInfo == null)
                {
                    if (ReadNamePlayerInfo(m_iIndex, out var _info))
                    {
                        m_playerInfo = new PlayerInfo(
                            Generators.ParseChars(_info.m_szPlayerName),
                             Generators.ParseChars(_info.m_szFriendsName),
                              Generators.ParseChars(_info.m_szSteamID));
                        return m_playerInfo.SteamID;
                    }
                    else
                        return "INVALIDHANDLE";
                }
                else
                {
                    return m_playerInfo.SteamID;
                }
            }
        }

        public bool m_bHasDefuser
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bHasDefuser); }
        }
        public bool m_bIsDefusing
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bIsDefusing); }
        }
        public bool m_bIsSneaking
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bIsWalking); }
        }
        public int m_iArmor
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_ArmorValue); }
        }

        //public BaseCombatWeapon WeaponDefinitionIndex
        //{
        //    get { return GetWeaponItemDefinition(); }
        //}

        public bool m_bIsScoped
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bIsScoped); }
        }
        public int m_iObserverTarget
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_hObserverTarget) & 0xFFF; }
        }

        public ObserverMode m_iObserverMode
        {
            get { return (ObserverMode)MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_iObserverMode); }
        }

        public bool m_bGunGameImmunity
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bGunGameImmunity); }
        }
        //public int m_iGlowIndex
        //{
        //    get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_iGlowIndex); }
        //}
        public Vector3 m_vecViewOffset
        {
            get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress + (int)g_Globals.Offset.m_vecViewOffset); }
        }

        public bool m_bIsSpectator
        {
            get { return Team == PlayerTeam.Neutral; }
        }

        public Vector3 m_vecHead
        {
            get { return GetBoneVec(BONE.Head); }
        }

        public Vector3 m_vecBody
        {
            get { return GetBoneVec(BONE.Body); }
        }
        public Vector3 m_vecChest
        {
            get { return GetBoneVec(BONE.Chest); }
        }
        public Vector3 m_vecNeck
        {
            get { return GetBoneVec(BONE.Neck); }
        }
        public Vector3 m_vecLShoulder
        {
            get { return GetBoneVec(BONE.LShoulder); }
        }
        public Vector3 m_vecRShoulder
        {
            get { return GetBoneVec(Team == PlayerTeam.Terrorist ? BONE.TRShoulder : BONE.CTRShoulder); }
        }


        public IntPtr m_nStudioHdr
        {
            get { return MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + (int)g_Globals.Offset.m_pStudioHdr); }
        }
        //public ItemDefinitionIndex CachedWeaponDefintionIndex;

        public int m_iAccount
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iAccount); }
        }

        private string m_pszArmsModel;
        public string m_szArmsModel
        {
            get
            {
                if (string.IsNullOrEmpty(m_pszArmsModel))
                    m_pszArmsModel = MemoryLoader.instance.Reader.ReadString(BaseAddress + g_Globals.Offset.m_szArmsModel, Encoding.UTF8, 256);
                return m_pszArmsModel;
            }
            set
            {
                m_pszArmsModel = value;
                MemoryLoader.instance.Reader.WriteString(BaseAddress + g_Globals.Offset.m_szArmsModel, m_pszArmsModel, Encoding.UTF8);

            }
        }


        public Vector3[] GetUpperBody()
        {
            return new Vector3[] { m_vecHead + (SharpDX.Vector3.UnitZ * 6), m_vecLShoulder, m_vecRShoulder, m_vecBody, m_vecChest, m_vecNeck };
        }

        public Vector3[] m_v3aPseudoPredict
        {
            get
            {
                return new Vector3[] { 
                    m_vecHead + (SharpDX.Vector3.UnitZ * 6), 
                    m_vecLShoulder + (SharpDX.Vector3.Up * 14.777f), 
                    m_vecRShoulder + (SharpDX.Vector3.Down * 14.777f), 
                    m_vecLShoulder + (-SharpDX.Vector3.UnitX * 14.777f), 
                    m_vecRShoulder + (SharpDX.Vector3.UnitX * 14.777f),  
                    m_vecBody };
            }
        }

        public int m_nTickBase
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nTickBase); }
        }

        //IS valid check to dwEntityList and re read pointer?
        //Filter out null pointers on update?

        private DateTime m_pdtLastVisCheck;
        public DateTime m_dtLastVisCheck
        {
            get { return m_pdtLastVisCheck; }
            set { m_pdtLastVisCheck = value; }
        }


        //public void SetSpotted(bool value = true)
        //{
        //    MemoryLoader.instance.Reader.Write(BaseAddress + (int)g_Globals.Offset.m_bSpotted, value);
        //}

        int ECX_DISP = 0x40;
        int EDX_DISP = 0x0C;
        int INFO_OFFSET = 0x28;
        int ENTRY_SIZE = 0x34;

        //Cache this? or try another method
        public string ReadNamePlayerInfo(int id)
        {
            IntPtr usernfotable = MemoryLoader.instance.Reader.Read<IntPtr>(Henker.Singleton.Engine.Pointer + (int)g_Globals.Offset.dwPlayerInfo);
            if (usernfotable == IntPtr.Zero)
                return "UNNAMED";
            IntPtr ecx = MemoryLoader.instance.Reader.Read<IntPtr>(usernfotable + ECX_DISP);
            if (ecx == IntPtr.Zero)
                return "UNNAMED";
            IntPtr edx = MemoryLoader.instance.Reader.Read<IntPtr>(ecx + EDX_DISP);
            if (edx == IntPtr.Zero)
                return "UNNAMED";
            IntPtr eax = MemoryLoader.instance.Reader.Read<IntPtr>(edx + INFO_OFFSET + ENTRY_SIZE * (id - 1));
            if (eax == IntPtr.Zero || (uint)eax == V)
                return "UNNAMED";
            return new string(MemoryLoader.instance.Reader.Read<player_info_s>(eax).m_szPlayerName).Trim('\0');

        }

        public int m_iShotsFired
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iShotsFired); }
        }

        internal bool ValidBoneMatrix
        {
            get { return MemoryLoader.instance.Reader.Read<IntPtr>(this.BaseAddress + (int)g_Globals.Offset.m_dwBoneMatrix) != IntPtr.Zero; }
        }

        internal void SetRenderColor(SharpDX.Color _c)
        {
            SetRenderColor(_c.R, _c.G, _c.B, _c.A);
        }

        internal void SetRenderColor(byte r, byte g, byte b, byte a)
        {
            if (!IsValid)
                return;
            var _clr = m_clrRender;
            if (_clr.R == r && _clr.G == g && _clr.B == b && _clr.A == a)
                return;
            _clr.R = r;
            _clr.G = g;
            _clr.B = b;
            _clr.A = a;
            m_clrRender = _clr;
        }

        public matrix3x4 GetMatrix3x4(int iBone)
        {
            IntPtr pBase = MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + (int)g_Globals.Offset.m_dwBoneMatrix);
            return MemoryLoader.instance.Reader.Read<matrix3x4>(BaseAddress + (int)pBase + 0x30 * iBone);
        }

        /// <summary>
        /// probably just portal shit
        /// </summary>
        public int m_hViewEntity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_hViewEntity) & 0xFFF; }
        }

        public int m_iClass
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + 0xB374); }
        }


        public int m_iTotalHitsOnServer
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_totalHitsOnServer); }
        }

        public int m_iNumRoundKills
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iNumRoundKills); }
        }

        public int m_iNumRoundKillsHeadshots
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iNumRoundKillsHeadshots); }
        }

        public bool m_bKilledByTaser
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bKilledByTaser); }
        }

        //public byte m_nLastConcurrentKilled
        //{
        //    get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nLastConcurrentKilled); }
        //}
        //public byte m_nLastKillerIndex
        //{
        //    get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nLastKillerIndex); }
        //}
        public SharpDX.Vector3 GetBoneVec(BONE bone)
        {

            var _boneBase = MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + (int)g_Globals.Offset.m_dwBoneMatrix);
            if (_boneBase == IntPtr.Zero)
                return Vector3.Zero;
            BoneVec bVec = MemoryLoader.instance.Reader.Read<BoneVec>((IntPtr)(_boneBase + 0x30 * (int)bone));
            return new Vector3(bVec.X, bVec.Y, bVec.Z);
        }

        public BaseCombatWeapon m_hActiveWeapon
        {
            get { return Henker.Singleton.Client.GetWeaponById(MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_hActiveWeapon) & 0xFFF); }
        }

        private player_info_s _playerInfo = new player_info_s();
        private bool ReadNamePlayerInfo(int id, out player_info_s _info)
        {
            _info = new player_info_s();
            IntPtr usernfotable = MemoryLoader.instance.Reader.Read<IntPtr>(Henker.Singleton.Engine.Pointer + (int)g_Globals.Offset.dwPlayerInfo);
            if (usernfotable == IntPtr.Zero)
                return false;
            IntPtr ecx = MemoryLoader.instance.Reader.Read<IntPtr>(usernfotable + ECX_DISP);
            if (ecx == IntPtr.Zero)
                return false;
            IntPtr edx = MemoryLoader.instance.Reader.Read<IntPtr>(ecx + EDX_DISP);
            if (edx == IntPtr.Zero)
                return false;
            IntPtr eax = MemoryLoader.instance.Reader.Read<IntPtr>(edx + INFO_OFFSET + ENTRY_SIZE * (id - 1));
            if (eax == IntPtr.Zero || (uint)eax == V)
                return false;
            _info = MemoryLoader.instance.Reader.Read<player_info_s>(eax);
            _playerInfo = _info;
            return true;
        }
        public bool IsVisible = false;


    }
}
