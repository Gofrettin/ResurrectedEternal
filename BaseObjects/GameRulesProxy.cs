using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class GameRulesProxy : BaseEntity
    {
        public short m_totalRoundsPlayed
        {
            get { return MemoryLoader.instance.Reader.Read<short>(BaseAddress + g_Globals.Offset.m_totalRoundsPlayed); }
        }
        public bool m_bFreezePeriod
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bFreezePeriod); }
        }
        public bool m_bMatchWaitingForResume
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bMatchWaitingForResume); }
        }
        public bool m_bWarmupPeriod
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bWarmupPeriod); }
        }
        public float m_fWarmupPeriodEnd
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fWarmupPeriodEnd); }
        }
        public float m_fWarmupPeriodStart
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fWarmupPeriodStart); }
        }
        public bool m_bTerroristTimeOutActive
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bTerroristTimeOutActive); }
        }
        public bool m_bCTTimeOutActive
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bCTTimeOutActive); }
        }
        public float m_flTerroristTimeOutRemaining
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flTerroristTimeOutRemaining); }
        }
        public float m_flCTTimeOutRemaining
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flCTTimeOutRemaining); }
        }
        public int m_nTerroristTimeOuts
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nTerroristTimeOuts); }
        }
        public int m_nCTTimeOuts
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_nTerroristTimeOuts); }
        }
        public int m_iRoundTime
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iRoundTime); }
        }
        public GamePhase m_gamePhase
        {
            get { return (GamePhase)MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_gamePhase); }
        }

        public short m_nOvertimePlaying
        {
            get { return MemoryLoader.instance.Reader.Read<short>(BaseAddress + g_Globals.Offset.m_nOvertimePlaying); }
        }
        public float m_flTimeUntilNextPhaseStarts
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_timeUntilNextPhaseStarts); }
        }
        public float m_flCMMItemDropRevealStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flCMMItemDropRevealStartTime); }
        }
        public float m_flCMMItemDropRevealEndTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flCMMItemDropRevealEndTime); }
        }
        public float m_fRoundStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_fRoundStartTime); }
        }
        public bool m_bGameRestart
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bGameRestart); }
        }
        public float m_flRestartRoundTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flRestartRoundTime); }
        }
        public float m_flGameStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flGameStartTime); }
        }
        public int m_iHostagesRemaining
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iHostagesRemaining); }
        }
        public bool m_bAnyHostageReached
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bAnyHostageReached); }
        }
        public bool m_bMapHasBombTarget
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bMapHasBombTarget); }
        }
        public bool m_bMapHasRescueZone
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bMapHasRescueZone); }
        }
        public bool m_bMapHasBuyZone
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bMapHasBuyZone); }
        }
        public bool m_bIsQueuedMatchmaking
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bIsQueuedMatchmaking); }
        }
        //public int m_nQueuedMatchmakingMode = 0x0078; //(int )
        public bool m_bIsValveDS
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bIsValveDS); }
        }
        public bool m_bIsQuestEligible
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bIsQuestEligible); }
        }
        public bool m_bLogoMap
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bLogoMap); }
        }
        public bool m_bPlayAllStepSoundsOnServer
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bPlayAllStepSoundsOnServer); }
        }
        public int m_iNumGunGameProgressiveWeaponsCT
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iNumGunGameProgressiveWeaponsCT); }
        }
        public int m_iNumGunGameProgressiveWeaponsT
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iNumGunGameProgressiveWeaponsT); }
        }
        public int m_iSpectatorSlotCount
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iSpectatorSlotCount); }
        }
        public bool m_bBombDropped
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bBombDropped); }
        }
        public bool m_bBombPlanted
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bBombPlanted); }
        }
        public int m_iRoundWinStatus
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iRoundWinStatus); }
        }
        public e_RoundEndReason m_eRoundWinReason
        {
            get { return (e_RoundEndReason)MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_eRoundWinReason); }
        }
        public float m_flDMBonusStartTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flDMBonusStartTime); }
        }
        public float m_flDMBonusTimeLength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flDMBonusTimeLength); }
        }
        public ushort m_unDMBonusWeaponLoadoutSlot
        {
            get { return MemoryLoader.instance.Reader.Read<ushort>(BaseAddress + g_Globals.Offset.m_unDMBonusWeaponLoadoutSlot); }
        }
        public bool m_bDMBonusActive
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bDMBonusActive); }
        }
        public bool m_bTCantBuy
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bTCantBuy); }
        }
        public bool m_bCTCantBuy
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bCTCantBuy); }
        }
        public float m_flGuardianBuyUntilTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flGuardianBuyUntilTime); }
        }
        public int m_iMatchStats_RoundResults
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMatchStats_RoundResults); }
        }
        public int m_iMatchStats_PlayersAlive_T
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMatchStats_PlayersAlive_T); }
        }
        public int m_iMatchStats_PlayersAlive_CT
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_iMatchStats_PlayersAlive_CT); }
        }
        //public int m_GGProgressiveWeaponOrderCT = 0x008C; //void* )
        //public int m_GGProgressiveWeaponOrderT = 0x017C; //void* )
        //public int m_GGProgressiveWeaponKillUpgradeOrderCT = 0x026C; //void* )
        //public int m_GGProgressiveWeaponKillUpgradeOrderT = 0x035C; //void* )
        //public int m_MatchDevice = 0x044C; //int )
        public bool m_bHasMatchStarted
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bHasMatchStarted); }
        }
        public float m_flTeamRespawnWaveTimes
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_TeamRespawnWaveTimes); }
        }
        public float m_flNextRespawnWave
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flNextRespawnWave); }
        }
        //public int m_nNextMapInMapgroup = 0x0460; //int )
        //public int m_nEndMatchMapGroupVoteTypes = 0x0C28; //void* )
        //public int m_nEndMatchMapGroupVoteOptions = 0x0C50; //void* )
        //public int m_nEndMatchMapVoteWinner = 0x0C78; //int )
        //public int m_bIsDroppingItems = 0x0880; //int )
        //public int m_iActiveAssassinationTargetMissionID = 0x0C20; //int )
        //public int m_fMatchStartTime = 0x0048; //float )
        //public int m_szTournamentEventName = 0x0464; //char[260] )
        //public int m_szTournamentEventStage = 0x0568; //char[260] )
        //public int m_szTournamentPredictionsTxt = 0x0770; //char[260] )
        //public int m_nTournamentPredictionsPct = 0x0874; //int )
        //public int m_szMatchStatTxt = 0x066C; //char[260] )
        //public int m_nGuardianModeWaveNumber = 0x0884; //int )
        //public int m_nGuardianModeSpecialKillsRemaining = 0x0888; //int )
        //public int m_nGuardianModeSpecialWeaponNeeded = 0x088C; //int )
        //public int m_nHalloweenMaskListSeed = 0x09A0; //int )
        //public int m_numGlobalGiftsGiven = 0x0898; //int )
        //public int m_numGlobalGifters = 0x089C; //int )
        //public int m_numGlobalGiftsPeriodSeconds = 0x08A0; //int )
        //public int m_arrFeaturedGiftersAccounts = 0x08A4; //void* )
        //public int m_arrFeaturedGiftersGifts = 0x08B4; //void* )
        //public int m_arrProhibitedItemIndices = 0x08C4; //void* )
        //public int m_numBestOfMaps = 0x099C; //int )
        //public int m_arrTournamentActiveCasterAccounts = 0x098C; //void* )
        //public int m_iNumConsecutiveCTLoses = 0x0C7C; //int )
        //public int m_iNumConsecutiveTerroristLoses = 0x0C80; //int )
        //public int m_SurvivalRules = 0x0D00; //void* )
        //public int m_vecPlayAreaMins = 0x0D00; //Vec3 )
        //public int m_vecPlayAreaMaxs = 0x0D0C; //Vec3 )
        //public int m_iPlayerSpawnHexIndices = 0x0D18; //void* )
        //public int m_SpawnTileState = 0x0E18; //void* )
        //public int m_flSpawnSelectionTimeStart = 0x0EF8; //float )
        //public int m_flSpawnSelectionTimeEnd = 0x0EFC; //float )
        //public int m_flSpawnSelectionTimeLoadout = 0x0F00; //float )
        //public int m_spawnStage = 0x0F04; //int )
        //public int m_flTabletHexOriginX = 0x0F08; //float )
        //public int m_flTabletHexOriginY = 0x0F0C; //float )
        //public int m_flTabletHexSize = 0x0F10; //float )
        //public int m_roundData_playerXuids = 0x0F18; //void* )
        //public int m_roundData_playerPositions = 0x1120; //void* )
        //public int m_roundData_playerTeams = 0x1224; //void* )
        //public int m_SurvivalGameRuleDecisionTypes = 0x1328; //void* )
        //public int m_SurvivalGameRuleDecisionValues = 0x1368; //void* )
        //public int m_flSurvivalStartTime = 0x13A8; //float )
        //public int m_RetakeRules = 0x13C0; //void* )
        //public int m_bBlockersPresent = 0x14C0; //int )
        //public int m_bRoundInProgress = 0x14C1; //int )
        //public int m_iFirstSecondHalfRound = 0x14C4; //int )
        //public int m_iBombSite = 0x14C8; //int )
        public GameRulesProxy(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
