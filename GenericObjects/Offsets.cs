using Newtonsoft.Json;
using ResurrectedEternal.Configs.ConfigSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.GenericObjects
{
    public class Offsets
    {


        #region "signatures"
        public int m_dwGetAllClasses; // = 0xDB101C;
        public int dwViewMatrix;// 0x4D93824;
        public int dwEntityList;// 0x4DA1F24;
        public int dwRadarBase;// 0x51D6C9C;//0x518810C;
        public int dwGlowObjectManager;// 0x52EA520;//0x529A1D0;
        public int dwPlayerResource;// 0x31D17E0;
        public int dwForceJump;// 0x524BE84;
        public int dwForceAttack;// 0x31D3460;
        public int dwForceLeft;
        public int dwForceRight;
        public int dwGameRulesProxy;// 0x52BF16C;
        public int dwGlobalVars;// 0x58ECE8;  
        public int dwClientState;// 0x58EFE4;
        //force_spectator_glow
        public int dwForceGlow;// 0x3AD962;
        public int dwPlayerInfo;// 0x52C0;
        public int dwModelPrecacheTable;// 0x52A4;
        public int m_dwConvarTable;// 0x2F0F8;
        public int m_engineCvar;// 0x3E9EC;
        #endregion



        public int dwClientState_Map = 0x28C;
        public int dwClientState_MapDirectory = 0x188;
        public int dwClientState_MaxPlayer = 0x388;
        public int dwClientState_ViewAngles = 0x4D90;

        public int m_dwLocalPlayerIndex = 0x180;
        public int m_dwIndex = 0x64;
        public int m_bDormant = 0xED;
        public int GameState = 0x108;// 0x108;
        public int EntitySize = 0x10;


        public int m_aimPunchAngle => m_Local + g_Globals.NetVars["DT_Local::m_aimPunchAngle"]; // 0x302C;
        public int m_aimPunchAngleVel => m_Local + g_Globals.NetVars["DT_Local::m_aimPunchAngleVel"]; // 0x3038;

        //public int m_iDefaultFOV;// 0x3208;
        //public int m_flFOVRate;// 0x3000;
        //public int m_model_ambient_min;// 0x59205C;
        public int m_pStudioHdr;// 0x294C;



        //0x1F0
        

        #region "FOG"
        public int m_bFogEnable => g_Globals.NetVars["DT_FogController::m_fog.enable"]; //0x0A1C;// (int )
        public int m_iFogblend => g_Globals.NetVars["DT_FogController::m_fog.blend"]; // 0x0A1D;// (int )
        public int m_vFogdirPrimary => g_Globals.NetVars["DT_FogController::m_fog.dirPrimary"]; // 0x09DC;//(Vec3 )
        public int m_cFogcolorPrimary => g_Globals.NetVars["DT_FogController::m_fog.colorPrimary"]; // 0x09E8;//(int )
        public int m_cFogcolorSecondary => g_Globals.NetVars["DT_FogController::m_fog.colorSecondary"]; // 0x09EC;// (int )
        public int m_fFogstart => g_Globals.NetVars["DT_FogController::m_fog.start"]; // 0x09F8;// (float )
        public int m_fFogend => g_Globals.NetVars["DT_FogController::m_fog.end"]; // 0x09FC;// (float )
        //this doenst do shit except break your whole game
        public int m_fFogfarz => g_Globals.NetVars["DT_FogController::m_fog.farz"]; // 0x0A00;// (float )
        public int m_fFogmaxdensity => g_Globals.NetVars["DT_FogController::m_fog.maxdensity"]; // 0x0A04;// (float )
        public int m_fFogcolorPrimaryLerpTo => g_Globals.NetVars["DT_FogController::m_fog.colorPrimaryLerpTo"]; // 0x09F0;//(int )
        public int m_fFogcolorSecondaryLerpTo => g_Globals.NetVars["DT_FogController::m_fog.colorSecondaryLerpTo"]; // 0x09F4;//(int )
        public int m_fFogstartLerpTo => g_Globals.NetVars["DT_FogController::m_fog.startLerpTo"]; // 0x0A08;//(float )
        public int m_fFogendLerpTo => g_Globals.NetVars["DT_FogController::m_fog.endLerpTo"]; // 0x0A0C;//(float )
        public int m_fFogmaxdensityLerpTo => g_Globals.NetVars["DT_FogController::m_fog.maxdensityLerpTo"]; // 0x0A10;// (float )
        public int m_fFoglerptime => g_Globals.NetVars["DT_FogController::m_fog.lerptime"]; // 0x0A14;// (float )
        public int m_fFogduration => g_Globals.NetVars["DT_FogController::m_fog.duration"]; // 0x0A18;// (float )
        public int m_fFogHDRColorScale => g_Globals.NetVars["DT_FogController::m_fog.HDRColorScale"]; // 0x0A24;// (float )
        public int m_fFogZoomFogScale => g_Globals.NetVars["DT_FogController::m_fog.ZoomFogScale"]; // 0x0A20;// (float )
        #endregion

        #region "sun"

        //attach ReClass to csgo, jump to sun address, add B54, add a couple of thousand bytes, reverse the offsets yourself, its easy
        public int m_dwPrimaryGlowOverlay = 0x9D8;
        public int m_dwSecondaryGlowOverlay = 0xA88;
        public int m_vVecDir = 0x14;
        public int m_nSpriteCount = 0x90;
        public int m_glowOverlayOffset = 0x30;
        public int m_glowOverlaySize = 0x18;
        public int m_overlayVecColor = 0x0;
        public int m_flHorizontalScale = 0x0C;
        public int m_flVerticalScale = 0x10;
        public int m_dwMaterial = 0x14;

        #endregion
        #region "color correction"

        public int m_bccEnabled => g_Globals.NetVars["DT_ColorCorrection::m_bEnabled"]; // 0x0B00; //(int )

        #endregion

        #region "wind"
        public int m_iMinWind => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iMinWind"]; //0x09E4; //(int )
        public int m_iMaxWind => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iMaxWind"]; // 0x09E8; //(int )
        public int m_iMinGust => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iMinGust"]; // 0x09F0; //(int )
        public int m_iMaxGust => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iMaxGust"]; // 0x09F4; //(int )
        public int m_flMinGustDelay => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_flMinGustDelay"]; // 0x09F8; // (float )
        public int m_flMaxGustDelay => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_flMaxGustDelay"]; // 0x09FC; //(float )
        public int m_iGustDirChange => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iGustDirChange"]; // 0x0A04; //(int )
        public int m_iWindSeed => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iWindSeed"]; // 0x09E0; //(int )
        public int m_iInitialWindDir => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_iInitialWindDir"]; // 0x0A44; // (int )
        public int m_flInitialWindSpeed => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_flInitialWindSpeed"]; // 0x0A48; //(float )
                                                                                    // public int m_flStartTime = 0x09DC; //(float )
        public int m_flGustDuration => m_EnvWindShared + g_Globals.NetVars["DT_EnvWindShared::m_flGustDuration"]; // 0x0A00; //(float )

        #endregion

        #region "gamerulesproxy"
        public int m_bFreezePeriod => g_Globals.NetVars["DT_CSGameRules::m_bFreezePeriod"];// 0x0020; //(int )
        public int m_bMatchWaitingForResume => g_Globals.NetVars["DT_CSGameRules::m_bMatchWaitingForResume"];// = 0x0040; //(int )
        public int m_bWarmupPeriod => g_Globals.NetVars["DT_CSGameRules::m_bWarmupPeriod"];// = 0x0021; //(int )
        public int m_fWarmupPeriodEnd => g_Globals.NetVars["DT_CSGameRules::m_fWarmupPeriodEnd"];// = 0x0024; //(float )
        public int m_fWarmupPeriodStart => g_Globals.NetVars["DT_CSGameRules::m_fWarmupPeriodStart"];// = 0x0028; //(float )
        public int m_bTerroristTimeOutActive => g_Globals.NetVars["DT_CSGameRules::m_bTerroristTimeOutActive"];// = 0x002C; // (int )
        public int m_bCTTimeOutActive => g_Globals.NetVars["DT_CSGameRules::m_bCTTimeOutActive"];// = 0x002D; // (int )
        public int m_flTerroristTimeOutRemaining => g_Globals.NetVars["DT_CSGameRules::m_flTerroristTimeOutRemaining"];// = 0x0030; // (float )
        public int m_flCTTimeOutRemaining => g_Globals.NetVars["DT_CSGameRules::m_flCTTimeOutRemaining"];//= 0x0034; // (float )
        public int m_nTerroristTimeOuts => g_Globals.NetVars["DT_CSGameRules::m_nTerroristTimeOuts"];// = 0x0038; //(int )
        public int m_nCTTimeOuts => g_Globals.NetVars["DT_CSGameRules::m_nCTTimeOuts"];// = 0x003C; //(int )
        public int m_iRoundTime => g_Globals.NetVars["DT_CSGameRules::m_iRoundTime"];// = 0x0044; // (int )
        public int m_gamePhase => g_Globals.NetVars["DT_CSGameRules::m_gamePhase"];// = 0x0060; //(int )
        public int m_totalRoundsPlayed => g_Globals.NetVars["DT_CSGameRules::m_totalRoundsPlayed"];// = 0x0064; // (int )
        public int m_nOvertimePlaying => g_Globals.NetVars["DT_CSGameRules::m_nOvertimePlaying"];// = 0x0068; //(int )
        public int m_timeUntilNextPhaseStarts => g_Globals.NetVars["DT_CSGameRules::m_timeUntilNextPhaseStarts"];// = 0x005C; //(float )
        public int m_flCMMItemDropRevealStartTime => g_Globals.NetVars["DT_CSGameRules::m_flCMMItemDropRevealStartTime"];// = 0x0878; //(float )
        public int m_flCMMItemDropRevealEndTime => g_Globals.NetVars["DT_CSGameRules::m_flCMMItemDropRevealEndTime"];// = 0x087C; //(float )
        public int m_fRoundStartTime => g_Globals.NetVars["DT_CSGameRules::m_fRoundStartTime"];// = 0x004C; // (float )
        public int m_bGameRestart => g_Globals.NetVars["DT_CSGameRules::m_bGameRestart"];// = 0x0054; //(int )
        public int m_flRestartRoundTime => g_Globals.NetVars["DT_CSGameRules::m_flRestartRoundTime"];// = 0x0050; //(float )
        public int m_flGameStartTime => g_Globals.NetVars["DT_CSGameRules::m_flGameStartTime"];// = 0x0058; //(float )
        public int m_iHostagesRemaining => g_Globals.NetVars["DT_CSGameRules::m_iHostagesRemaining"];// = 0x006C; //(int )
        public int m_bAnyHostageReached => g_Globals.NetVars["DT_CSGameRules::m_bAnyHostageReached"];// = 0x0070; //(int )
        public int m_bMapHasBombTarget => g_Globals.NetVars["DT_CSGameRules::m_bMapHasBombTarget"];// = 0x0071; //(int )
        public int m_bMapHasRescueZone => g_Globals.NetVars["DT_CSGameRules::m_bMapHasRescueZone"];// = 0x0072; //(int )
        public int m_bMapHasBuyZone => g_Globals.NetVars["DT_CSGameRules::m_bMapHasBuyZone"];// = 0x0073; //(int )
        public int m_bIsQueuedMatchmaking => g_Globals.NetVars["DT_CSGameRules::m_bIsQueuedMatchmaking"];// = 0x0074; //(int )
        public int m_nQueuedMatchmakingMode => g_Globals.NetVars["DT_CSGameRules::m_nQueuedMatchmakingMode"];// = 0x0078; //(int )
        public int m_bIsValveDS => g_Globals.NetVars["DT_CSGameRules::m_bIsValveDS"];// = 0x007C; //(int )
        public int m_bIsQuestEligible => g_Globals.NetVars["DT_CSGameRules::m_bIsQuestEligible"];// = 0x0881; //(int )
        public int m_bLogoMap => g_Globals.NetVars["DT_CSGameRules::m_bLogoMap"];// = 0x007D; //(int )
        public int m_bPlayAllStepSoundsOnServer => g_Globals.NetVars["DT_CSGameRules::m_bPlayAllStepSoundsOnServer"];// = 0x007E; //(int )
        public int m_iNumGunGameProgressiveWeaponsCT => g_Globals.NetVars["DT_CSGameRules::m_iNumGunGameProgressiveWeaponsCT"];// = 0x0080; //(int )
        public int m_iNumGunGameProgressiveWeaponsT => g_Globals.NetVars["DT_CSGameRules::m_iNumGunGameProgressiveWeaponsT"];// = 0x0084; //(int )
        public int m_iSpectatorSlotCount => g_Globals.NetVars["DT_CSGameRules::m_iSpectatorSlotCount"];// = 0x0088; // (int )
        public int m_bBombDropped => g_Globals.NetVars["DT_CSGameRules::m_bBombDropped"];// = 0x09A4; //(int )
        public int m_bBombPlanted => g_Globals.NetVars["DT_CSGameRules::m_bBombPlanted"];// = 0x09A5; //(int )
        public int m_iRoundWinStatus => g_Globals.NetVars["DT_CSGameRules::m_iRoundWinStatus"];// = 0x09A8; //(int )
        public int m_eRoundWinReason => g_Globals.NetVars["DT_CSGameRules::m_eRoundWinReason"];// = 0x09AC; //(int )
        public int m_flDMBonusStartTime => g_Globals.NetVars["DT_CSGameRules::m_flDMBonusStartTime "];// = 0x0454; //(float )
        public int m_flDMBonusTimeLength => g_Globals.NetVars["DT_CSGameRules::m_flDMBonusTimeLength"];// = 0x0458; //(float )
        public int m_unDMBonusWeaponLoadoutSlot => g_Globals.NetVars["DT_CSGameRules::m_unDMBonusWeaponLoadoutSlot"];//= 0x045C; //(int )
        public int m_bDMBonusActive => g_Globals.NetVars["DT_CSGameRules::m_bDMBonusActive"];//= 0x045E; //(int )
        public int m_bTCantBuy => g_Globals.NetVars["DT_CSGameRules::m_bTCantBuy"];//= 0x09B0; //(int )
        public int m_bCTCantBuy => g_Globals.NetVars["DT_CSGameRules::m_bCTCantBuy"];//= 0x09B1; // (int )
        public int m_flGuardianBuyUntilTime => g_Globals.NetVars["DT_CSGameRules::m_flGuardianBuyUntilTime"];//= 0x09B4; //(float )
        public int m_iMatchStats_RoundResults => g_Globals.NetVars["DT_CSGameRules::m_iMatchStats_RoundResults"];//= 0x09B8; //(void* )
        public int m_iMatchStats_PlayersAlive_T => g_Globals.NetVars["DT_CSGameRules::m_iMatchStats_PlayersAlive_T"];//= 0x0AA8; //(void* )
        public int m_iMatchStats_PlayersAlive_CT => g_Globals.NetVars["DT_CSGameRules::m_iMatchStats_PlayersAlive_CT"];//= 0x0A30; //; //void* )
        public int m_GGProgressiveWeaponOrderCT => g_Globals.NetVars["DT_CSGameRules::m_GGProgressiveWeaponOrderCT"];//= 0x008C; //void* )
        public int m_GGProgressiveWeaponOrderT => g_Globals.NetVars["DT_CSGameRules::m_GGProgressiveWeaponOrderT"];//= 0x017C; //void* )
        public int m_GGProgressiveWeaponKillUpgradeOrderCT => g_Globals.NetVars["DT_CSGameRules::m_GGProgressiveWeaponKillUpgradeOrderCT"];//= 0x026C; //void* )
        public int m_GGProgressiveWeaponKillUpgradeOrderT => g_Globals.NetVars["DT_CSGameRules::m_GGProgressiveWeaponKillUpgradeOrderT"];//= 0x035C; //void* )
        public int m_MatchDevice => g_Globals.NetVars["DT_CSGameRules::m_MatchDevice"];//= 0x044C; //int )
        public int m_bHasMatchStarted => g_Globals.NetVars["DT_CSGameRules::m_bHasMatchStarted"];//= 0x0450; //int )
        public int m_TeamRespawnWaveTimes => g_Globals.NetVars["DT_CSGameRules::m_TeamRespawnWaveTimes"];// = 0x0B20; //void* )
        public int m_flNextRespawnWave => g_Globals.NetVars["DT_CSGameRules::m_flNextRespawnWave"];// = 0x0BA0; //void* )
        public int m_nNextMapInMapgroup => g_Globals.NetVars["DT_CSGameRules::m_nNextMapInMapgroup"];// = 0x0460; //int )
        public int m_nEndMatchMapGroupVoteTypes => g_Globals.NetVars["DT_CSGameRules::m_nEndMatchMapGroupVoteTypes"];// = 0x0C28; //void* )
        public int m_nEndMatchMapGroupVoteOptions => g_Globals.NetVars["DT_CSGameRules::m_nEndMatchMapGroupVoteOptions"];// = 0x0C50; //void* )
        public int m_nEndMatchMapVoteWinner => g_Globals.NetVars["DT_CSGameRules::m_nEndMatchMapVoteWinner"];// = 0x0C78; //int )
        public int m_bIsDroppingItems => g_Globals.NetVars["DT_CSGameRules::m_bIsDroppingItems"];// = 0x0880; //int )
        public int m_iActiveAssassinationTargetMissionID => g_Globals.NetVars["DT_CSGameRules::m_iActiveAssassinationTargetMissionID"];// = 0x0C20; //int )
        public int m_fMatchStartTime => g_Globals.NetVars["DT_CSGameRules::m_fMatchStartTime"];// = 0x0048; //float )
        public int m_szTournamentEventName => g_Globals.NetVars["DT_CSGameRules::m_szTournamentEventName"];// = 0x0464; //char[260] )
        public int m_szTournamentEventStage => g_Globals.NetVars["DT_CSGameRules::m_szTournamentEventStage"];// = 0x0568; //char[260] )
        public int m_szTournamentPredictionsTxt => g_Globals.NetVars["DT_CSGameRules::m_szTournamentPredictionsTxt"];// = 0x0770; //char[260] )
        public int m_nTournamentPredictionsPct => g_Globals.NetVars["DT_CSGameRules::m_nTournamentPredictionsPct"];// = 0x0874; //int )
        public int m_szMatchStatTxt => g_Globals.NetVars["DT_CSGameRules::m_szMatchStatTxt"];// = 0x066C; //char[260] )
        public int m_nGuardianModeWaveNumber => g_Globals.NetVars["DT_CSGameRules::m_nGuardianModeWaveNumber"];// = 0x0884; //int )
        public int m_nGuardianModeSpecialKillsRemaining => g_Globals.NetVars["DT_CSGameRules::m_nGuardianModeSpecialKillsRemaining"];// = 0x0888; //int )
        public int m_nGuardianModeSpecialWeaponNeeded => g_Globals.NetVars["DT_CSGameRules::m_nGuardianModeSpecialWeaponNeeded"];// = 0x088C; //int )
        public int m_nHalloweenMaskListSeed => g_Globals.NetVars["DT_CSGameRules::m_nHalloweenMaskListSeed"];// = 0x09A0; //int )
        public int m_numGlobalGiftsGiven => g_Globals.NetVars["DT_CSGameRules::m_numGlobalGiftsGiven"];// = 0x0898; //int )
        public int m_numGlobalGifters => g_Globals.NetVars["DT_CSGameRules::m_numGlobalGifters"];// = 0x089C; //int )
        public int m_numGlobalGiftsPeriodSeconds => g_Globals.NetVars["DT_CSGameRules::m_numGlobalGiftsPeriodSeconds"];// = 0x08A0; //int )
        public int m_arrFeaturedGiftersAccounts => g_Globals.NetVars["DT_CSGameRules::m_arrFeaturedGiftersAccounts"];// = 0x08A4; //void* )
        public int m_arrFeaturedGiftersGifts => g_Globals.NetVars["DT_CSGameRules::m_arrFeaturedGiftersGifts"];// = 0x08B4; //void* )
        public int m_arrProhibitedItemIndices => g_Globals.NetVars["DT_CSGameRules::m_arrProhibitedItemIndices"];// = 0x08C4; //void* )
        public int m_numBestOfMaps => g_Globals.NetVars["DT_CSGameRules::m_numBestOfMaps"];// = 0x099C; //int )
        public int m_arrTournamentActiveCasterAccounts => g_Globals.NetVars["DT_CSGameRules::m_arrTournamentActiveCasterAccounts"];// = 0x098C; //void* )
        public int m_iNumConsecutiveCTLoses => g_Globals.NetVars["DT_CSGameRules::m_iNumConsecutiveCTLoses"];// = 0x0C7C; //int )
        public int m_iNumConsecutiveTerroristLoses => g_Globals.NetVars["DT_CSGameRules::m_iNumConsecutiveTerroristLoses"];// = 0x0C80; //int )
        //no idea what happened here
        public int m_SurvivalRules => g_Globals.NetVars["DT_CSGameRules::m_SurvivalRules"];// = 0x0D00; //void* )
        public int m_vecPlayAreaMins => g_Globals.NetVars["DT_SurvivalGameRules::m_vecPlayAreaMins"];// = 0x0D00; //Vec3 )
        public int m_vecPlayAreaMaxs => g_Globals.NetVars["DT_SurvivalGameRules::m_vecPlayAreaMaxs"];// = 0x0D0C; //Vec3 )
        public int m_iPlayerSpawnHexIndices => g_Globals.NetVars["DT_SurvivalGameRules::m_iPlayerSpawnHexIndices"];// = 0x0D18; //void* )
        public int m_SpawnTileState => g_Globals.NetVars["DT_SurvivalGameRules::m_SpawnTileState"];// = 0x0E18; //void* )
        public int m_flSpawnSelectionTimeStart => g_Globals.NetVars["DT_SurvivalGameRules::m_flSpawnSelectionTimeStart"];// = 0x0EF8; //float )
        public int m_flSpawnSelectionTimeEnd => g_Globals.NetVars["DT_SurvivalGameRules::m_flSpawnSelectionTimeEnd"];// = 0x0EFC; //float )
        public int m_flSpawnSelectionTimeLoadout => g_Globals.NetVars["DT_SurvivalGameRules::m_flSpawnSelectionTimeLoadout"];// = 0x0F00; //float )
        public int m_spawnStage => g_Globals.NetVars["DT_SurvivalGameRules::m_spawnStage"];// = 0x0F04; //int )
        public int m_flTabletHexOriginX => g_Globals.NetVars["DT_SurvivalGameRules::m_flTabletHexOriginX"];// = 0x0F08; //float )
        public int m_flTabletHexOriginY => g_Globals.NetVars["DT_SurvivalGameRules::m_flTabletHexOriginY"];// = 0x0F0C; //float )
        public int m_flTabletHexSize => g_Globals.NetVars["DT_SurvivalGameRules::m_flTabletHexSize"];// = 0x0F10; //float )
        public int m_roundData_playerXuids => g_Globals.NetVars["DT_SurvivalGameRules::m_roundData_playerXuids"];// = 0x0F18; //void* )
        public int m_roundData_playerPositions => g_Globals.NetVars["DT_SurvivalGameRules::m_roundData_playerPositions"];// = 0x1120; //void* )
        public int m_roundData_playerTeams => g_Globals.NetVars["DT_SurvivalGameRules::m_roundData_playerTeams"];// = 0x1224; //void* )
        public int m_SurvivalGameRuleDecisionTypes => g_Globals.NetVars["DT_SurvivalGameRules::m_SurvivalGameRuleDecisionTypes"];// = 0x1328; //void* )
        public int m_SurvivalGameRuleDecisionValues => g_Globals.NetVars["DT_SurvivalGameRules::m_SurvivalGameRuleDecisionValues"];// = 0x1368; //void* )
        public int m_flSurvivalStartTime => g_Globals.NetVars["DT_SurvivalGameRules::m_flSurvivalStartTime"];// = 0x13A8; //float )
        public int m_RetakeRules => g_Globals.NetVars["DT_CSGameRules::m_RetakeRules"];// = 0x13C0; //void* )
        public int m_bBlockersPresent => g_Globals.NetVars["DT_RetakeGameRules::m_bBlockersPresent"];// = 0x14C0; //int )
        public int m_bRoundInProgress => g_Globals.NetVars["DT_RetakeGameRules::m_bRoundInProgress"];// = 0x14C1; //int )
        public int m_iFirstSecondHalfRound => g_Globals.NetVars["DT_RetakeGameRules::m_iFirstSecondHalfRound"];// = 0x14C4; //int )
        public int m_iBombSite => g_Globals.NetVars["DT_RetakeGameRules::m_iBombSite"];// = 0x14C8; //int )
        #endregion


        #region "postprocessing"

        //this shit is a simple float array, most of them are overridden by the convars.
        public int m_flPostProcessParameters => g_Globals.NetVars["DT_PostProcessController::m_flPostProcessParameters"];
        public int m_flVignetteBlurStrength => m_flPostProcessParameters + g_Globals.NetVars["m_flPostProcessParameters::005"]; //0x9EC; //0x0014
        public int m_flFadetoblackStrength => m_flPostProcessParameters + g_Globals.NetVars["m_flPostProcessParameters::006"]; //0x9F0‬ //0x0018
        #endregion

        public int m_clrRender => g_Globals.NetVars["DT_TestTraceline::m_clrRender"];
        public int m_vecOrigin => g_Globals.NetVars["DT_TestTraceline::m_vecOrigin"];
        public int m_flSpawnTime => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_flSpawnTime"];
        public int m_FadeStartTime => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_FadeStartTime"];
        public int m_FadeEndTime => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_FadeEndTime"];
        public int m_MinColor => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_MinColor"];
        public int m_MaxColor => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_MaxColor"];
        public int m_CurrentStage => g_Globals.NetVars["DT_ParticleSmokeGrenade::m_CurrentStage"];
        public int m_EnvWindShared => g_Globals.NetVars["DT_EnvWind::m_EnvWindShared"];
        public int m_OriginalOwnerXuidLow => g_Globals.NetVars["DT_BaseAttributableItem::m_OriginalOwnerXuidLow"];
        public int m_OriginalOwnerXuidHigh => g_Globals.NetVars["DT_BaseAttributableItem::m_OriginalOwnerXuidHigh"];
        public int m_nFallbackPaintKit => g_Globals.NetVars["DT_BaseAttributableItem::m_nFallbackPaintKit"];
        public int m_nFallbackSeed => g_Globals.NetVars["DT_BaseAttributableItem::m_nFallbackSeed"];
        public int m_flFallbackWear => g_Globals.NetVars["DT_BaseAttributableItem::m_flFallbackWear"];
        public int m_nFallbackStatTrak => g_Globals.NetVars["DT_BaseAttributableItem::m_nFallbackStatTrak"];
        public int m_fAccuracyPenalty => g_Globals.NetVars["DT_WeaponCSBase::m_fAccuracyPenalty"];
        public int m_fLastShotTime => g_Globals.NetVars["DT_WeaponCSBase::m_fLastShotTime"];
        public int m_bStartedArming => g_Globals.NetVars["DT_WeaponC4::m_bStartedArming"];
        public int m_bBombPlacedAnimation => g_Globals.NetVars["DT_WeaponC4::m_bBombPlacedAnimation"];
        public int m_fArmedTime => g_Globals.NetVars["DT_WeaponC4::m_fArmedTime"];
        public int m_bShowC4LED => g_Globals.NetVars["DT_WeaponC4::m_bShowC4LED"];
        public int m_bIsPlantingViaUse => g_Globals.NetVars["DT_WeaponC4::m_bIsPlantingViaUse"];
        public int m_vecDirection => g_Globals.NetVars["DT_EnvGasCanister::m_vecDirection"];
        public int m_bBombTicking => g_Globals.NetVars["DT_PlantedC4::m_bBombTicking"];
        public int m_nBombSite => g_Globals.NetVars["DT_PlantedC4::m_nBombSite"];
        public int m_flC4Blow => g_Globals.NetVars["DT_PlantedC4::m_flC4Blow"];
        public int m_flTimerLength => g_Globals.NetVars["DT_PlantedC4::m_flTimerLength"];
        public int m_flDefuseLength => g_Globals.NetVars["DT_PlantedC4::m_flDefuseLength"];
        public int m_flDefuseCountDown => g_Globals.NetVars["DT_PlantedC4::m_flDefuseCountDown"];
        public int m_bBombDefused => g_Globals.NetVars["DT_PlantedC4::m_bBombDefused"];
        public int m_hBombDefuser => g_Globals.NetVars["DT_PlantedC4::m_hBombDefuser"];
        public int m_iScore => g_Globals.NetVars["DT_CSPlayerResource::m_iScore"];
        public int m_iCompetitiveRanking => g_Globals.NetVars["DT_CSPlayerResource::m_iCompetitiveRanking"];
        public int m_iCompetitiveWins => g_Globals.NetVars["DT_CSPlayerResource::m_iCompetitiveWins"];
        public int m_szClan => g_Globals.NetVars["DT_CSPlayerResource::m_szClan"];
        public int m_bKilledByTaser => g_Globals.NetVars["DT_CSPlayer::m_bKilledByTaser"];
        public int m_bGunGameImmunity => g_Globals.NetVars["DT_CSPlayer::m_bGunGameImmunity"];
        public int m_szArmsModel => g_Globals.NetVars["DT_CSPlayer::m_szArmsModel"];
        public int m_nModelIndex => g_Globals.NetVars["DT_CSRagdoll::m_nModelIndex"];
        public int m_iTeamNum => g_Globals.NetVars["DT_CSRagdoll::m_iTeamNum"];
        public int m_fFlags => g_Globals.NetVars["DT_CHostage::m_fFlags"];
        public int m_hAttachedToEntity => g_Globals.NetVars["DT_Sprite::m_hAttachedToEntity"];
        public int m_nAttachment => g_Globals.NetVars["DT_Sprite::m_nAttachment"];
        public int m_flScaleTime => g_Globals.NetVars["DT_Sprite::m_flScaleTime"];
        public int m_flSpriteScale => g_Globals.NetVars["DT_Sprite::m_flSpriteScale"];
        public int m_flSpriteFramerate => g_Globals.NetVars["DT_Sprite::m_flSpriteFramerate"];
        public int m_flGlowProxySize => g_Globals.NetVars["DT_Sprite::m_flGlowProxySize"];
        public int m_flHDRColorScale => g_Globals.NetVars["DT_Sprite::m_flHDRColorScale"];
        public int m_flFrame => g_Globals.NetVars["DT_Sprite::m_flFrame"];
        public int m_flBrightnessTime => g_Globals.NetVars["DT_Sprite::m_flBrightnessTime"];
        public int m_nBrightness => g_Globals.NetVars["DT_Sprite::m_nBrightness"];
        public int m_bWorldSpaceScale => g_Globals.NetVars["DT_Sprite::m_bWorldSpaceScale"];
        public int m_shadowDirection => g_Globals.NetVars["DT_SunlightShadowControl::m_shadowDirection"];
        public int m_clrOverlay => g_Globals.NetVars["DT_Sun::m_clrOverlay"];
        public int m_vDirection => g_Globals.NetVars["DT_Sun::m_vDirection"];
        public int m_bOn => g_Globals.NetVars["DT_Sun::m_bOn"];
        public int m_nSize => g_Globals.NetVars["DT_Sun::m_nSize"];
        public int m_nOverlaySize => g_Globals.NetVars["DT_Sun::m_nOverlaySize"];
        public int m_nMaterial => g_Globals.NetVars["DT_Sun::m_nMaterial"];
        public int m_nOverlayMaterial => g_Globals.NetVars["DT_Sun::m_nOverlayMaterial"];
        public int m_flLightScale => g_Globals.NetVars["DT_SpotlightEnd::m_flLightScale"];
        public int m_Radius => g_Globals.NetVars["DT_SpotlightEnd::m_Radius"];
        public int m_minFalloff => g_Globals.NetVars["DT_SpatialEntity::m_minFalloff"];
        public int m_maxFalloff => g_Globals.NetVars["DT_SpatialEntity::m_maxFalloff"];
        public int m_flCurWeight => g_Globals.NetVars["DT_SpatialEntity::m_flCurWeight"];
        public int m_shadowColor => g_Globals.NetVars["DT_ShadowControl::m_shadowColor"];
        public int m_flShadowMaxDist => g_Globals.NetVars["DT_ShadowControl::m_flShadowMaxDist"];
        public int m_bDisableShadows => g_Globals.NetVars["DT_ShadowControl::m_bDisableShadows"];
        public int m_bEnableLocalLightShadows => g_Globals.NetVars["DT_ShadowControl::m_bEnableLocalLightShadows"];
        public int m_iParentAttachment => g_Globals.NetVars["DT_RopeKeyframe::m_iParentAttachment"];
        public int m_iPing => g_Globals.NetVars["DT_PlayerResource::m_iPing"];
        public int m_iKills => g_Globals.NetVars["DT_PlayerResource::m_iKills"];
        public int m_iAssists => g_Globals.NetVars["DT_PlayerResource::m_iAssists"];
        public int m_iDeaths => g_Globals.NetVars["DT_PlayerResource::m_iDeaths"];
        public int m_fEffects => g_Globals.NetVars["DT_ParticleSystem::m_fEffects"];
        public int m_angRotation => g_Globals.NetVars["DT_ParticleSystem::m_angRotation"];
        public int m_bShouldGlow => g_Globals.NetVars["DT_Item::m_bShouldGlow"];
        public int m_vecVelocity => g_Globals.NetVars["DT_FuncMoveLinear::m_vecVelocity"];
        public int m_bUseCustomAutoExposureMin => g_Globals.NetVars["DT_EnvTonemapController::m_bUseCustomAutoExposureMin"];
        public int m_bUseCustomAutoExposureMax => g_Globals.NetVars["DT_EnvTonemapController::m_bUseCustomAutoExposureMax"];
        public int m_bUseCustomBloomScale => g_Globals.NetVars["DT_EnvTonemapController::m_bUseCustomBloomScale"];
        public int m_flCustomAutoExposureMin => g_Globals.NetVars["DT_EnvTonemapController::m_flCustomAutoExposureMin"];
        public int m_flCustomAutoExposureMax => g_Globals.NetVars["DT_EnvTonemapController::m_flCustomAutoExposureMax"];
        public int m_flCustomBloomScale => g_Globals.NetVars["DT_EnvTonemapController::m_flCustomBloomScale"];
        public int m_flCustomBloomScaleMinimum => g_Globals.NetVars["DT_EnvTonemapController::m_flCustomBloomScaleMinimum"];
        public int m_flBloomExponent => g_Globals.NetVars["DT_EnvTonemapController::m_flBloomExponent"];
        public int m_flBloomSaturation => g_Globals.NetVars["DT_EnvTonemapController::m_flBloomSaturation"];
        public int m_flTonemapPercentTarget => g_Globals.NetVars["DT_EnvTonemapController::m_flTonemapPercentTarget"];
        public int m_flTonemapPercentBrightPixels => g_Globals.NetVars["DT_EnvTonemapController::m_flTonemapPercentBrightPixels"];
        public int m_flTonemapMinAvgLum => g_Globals.NetVars["DT_EnvTonemapController::m_flTonemapMinAvgLum"];
        public int m_flTonemapRate => g_Globals.NetVars["DT_EnvTonemapController::m_flTonemapRate"];
        public int m_bDOFEnabled => g_Globals.NetVars["DT_EnvDOFController::m_bDOFEnabled"];
        public int m_flNearBlurDepth => g_Globals.NetVars["DT_EnvDOFController::m_flNearBlurDepth"];
        public int m_flNearFocusDepth => g_Globals.NetVars["DT_EnvDOFController::m_flNearFocusDepth"];
        public int m_flFarFocusDepth => g_Globals.NetVars["DT_EnvDOFController::m_flFarFocusDepth"];
        public int m_flFarBlurDepth => g_Globals.NetVars["DT_EnvDOFController::m_flFarBlurDepth"];
        public int m_flNearBlurRadius => g_Globals.NetVars["DT_EnvDOFController::m_flNearBlurRadius"];
        public int m_flFarBlurRadius => g_Globals.NetVars["DT_EnvDOFController::m_flFarBlurRadius"];
        public int m_envLightShadowDirection => g_Globals.NetVars["DT_CascadeLight::m_envLightShadowDirection"];
        public int m_bEnabled => g_Globals.NetVars["DT_CascadeLight::m_bEnabled"];
        public int m_bUseLightEnvAngles => g_Globals.NetVars["DT_CascadeLight::m_bUseLightEnvAngles"];
        public int m_LightColor => g_Globals.NetVars["DT_CascadeLight::m_LightColor"];
        public int m_LightColorScale => g_Globals.NetVars["DT_CascadeLight::m_LightColorScale"];
        public int m_flMaxShadowDist => g_Globals.NetVars["DT_CascadeLight::m_flMaxShadowDist"];
        public int m_vecColor => g_Globals.NetVars["DT_EnvAmbientLight::m_vecColor"];
        public int m_flStartTime => g_Globals.NetVars["DT_EntityDissolve::m_flStartTime"];
        public int m_flMaxWeight => g_Globals.NetVars["DT_ColorCorrection::m_flMaxWeight"];
        public int m_flFadeInDuration => g_Globals.NetVars["DT_ColorCorrection::m_flFadeInDuration"];
        public int m_flFadeOutDuration => g_Globals.NetVars["DT_ColorCorrection::m_flFadeOutDuration"];
        public int m_netLookupFilename => g_Globals.NetVars["DT_ColorCorrection::m_netLookupFilename"];
        public int m_bMaster => g_Globals.NetVars["DT_ColorCorrection::m_bMaster"];
        public int m_bClientSide => g_Globals.NetVars["DT_ColorCorrection::m_bClientSide"];
        public int m_bExclusive => g_Globals.NetVars["DT_ColorCorrection::m_bExclusive"];
        public int m_iFOV => g_Globals.NetVars["DT_BasePlayer::m_iFOV"];
        public int m_iFOVStart => g_Globals.NetVars["DT_BasePlayer::m_iFOVStart"];
        public int m_nRenderMode => g_Globals.NetVars["DT_BaseEntity::m_nRenderMode"];
        public int m_nRenderFX => g_Globals.NetVars["DT_BaseEntity::m_nRenderFX"];
        public int m_iName => g_Globals.NetVars["DT_BaseEntity::m_iName"];
        public int m_nSequence => g_Globals.NetVars["DT_BaseAnimating::m_nSequence"];
        public int m_nSkin => g_Globals.NetVars["DT_BaseAnimating::m_nSkin"];
        public int m_nBody => g_Globals.NetVars["DT_BaseAnimating::m_nBody"];
        public int m_flPlaybackRate => g_Globals.NetVars["DT_BaseAnimating::m_flPlaybackRate"];
        public int m_nNewSequenceParity => g_Globals.NetVars["DT_BaseAnimating::m_nNewSequenceParity"];
        public int m_nResetEventsParity => g_Globals.NetVars["DT_BaseAnimating::m_nResetEventsParity"];
        public int m_nMuzzleFlashParity => g_Globals.NetVars["DT_BaseAnimating::m_nMuzzleFlashParity"];
        public int m_ScaleType => g_Globals.NetVars["DT_BaseAnimating::m_ScaleType"];
        public int m_nBeamType => g_Globals.NetVars["DT_Beam::m_nBeamType"];
        public int m_nBeamFlags => g_Globals.NetVars["DT_Beam::m_nBeamFlags"];
        public int m_nNumBeamEnts => g_Globals.NetVars["DT_Beam::m_nNumBeamEnts"];
        public int m_hAttachEntity => g_Globals.NetVars["DT_Beam::m_hAttachEntity"];
        public int m_nAttachIndex => g_Globals.NetVars["DT_Beam::m_nAttachIndex"];
        public int m_nHaloIndex => g_Globals.NetVars["DT_Beam::m_nHaloIndex"];
        public int m_fHaloScale => g_Globals.NetVars["DT_Beam::m_fHaloScale"];
        public int m_fWidth => g_Globals.NetVars["DT_Beam::m_fWidth"];
        public int m_fEndWidth => g_Globals.NetVars["DT_Beam::m_fEndWidth"];
        public int m_fFadeLength => g_Globals.NetVars["DT_Beam::m_fFadeLength"];
        public int m_fAmplitude => g_Globals.NetVars["DT_Beam::m_fAmplitude"];
        public int m_fStartFrame => g_Globals.NetVars["DT_Beam::m_fStartFrame"];
        public int m_fSpeed => g_Globals.NetVars["DT_Beam::m_fSpeed"];
        public int m_flFrameRate => g_Globals.NetVars["DT_Beam::m_flFrameRate"];
        public int m_hWeapon => g_Globals.NetVars["DT_BaseViewModel::m_hWeapon"];
        public int m_nViewModelIndex => g_Globals.NetVars["DT_BaseViewModel::m_nViewModelIndex"];
        public int m_nAnimationParity => g_Globals.NetVars["DT_BaseViewModel::m_nAnimationParity"];
        public int m_hOwner => g_Globals.NetVars["DT_BaseViewModel::m_hOwner"];
        public int m_bShouldIgnoreOffsetAndAccuracy => g_Globals.NetVars["DT_BaseViewModel::m_bShouldIgnoreOffsetAndAccuracy"];
        public int m_flDamage => g_Globals.NetVars["DT_BaseGrenade::m_flDamage"];
        public int m_DmgRadius => g_Globals.NetVars["DT_BaseGrenade::m_DmgRadius"];
        public int m_bIsLive => g_Globals.NetVars["DT_BaseGrenade::m_bIsLive"];
        public int m_hThrower => g_Globals.NetVars["DT_BaseGrenade::m_hThrower"];
        public int m_iViewModelIndex => g_Globals.NetVars["DT_BaseCombatWeapon::m_iViewModelIndex"];
        public int m_iState => g_Globals.NetVars["DT_BaseCombatWeapon::m_iState"];

        public int m_vecViewOffset => g_Globals.NetVars["DT_LocalPlayerExclusive::m_vecViewOffset[0]"];// 0x108;
        //public int m_viewPunchAngle;// 0x3020;

        public int m_lifeState => g_Globals.NetVars["DT_BasePlayer::m_lifeState"];// 0x25F;
        public int m_Local => g_Globals.NetVars["DT_LocalPlayerExclusive::m_Local"];// 0x2FBC;
        public int m_bIsScoped => g_Globals.NetVars["DT_CSPlayer::m_bIsScoped"];// 0x3928;
        public int m_iHealth => g_Globals.NetVars["DT_BasePlayer::m_iHealth"];// 0x100;
        public int m_ArmorValue => g_Globals.NetVars["DT_CSPlayer::m_ArmorValue"];// 0xB378;
        public int m_iShotsFired => g_Globals.NetVars["DT_CSLocalPlayerExclusive::m_iShotsFired"];// 0xA390;


        public int m_bHasDefuser => g_Globals.NetVars["DT_CSPlayer::m_bHasDefuser"];// 0xB388;
        public int m_bHasHelmet => g_Globals.NetVars["DT_CSPlayer::m_bHasHelmet"];// 0xB36C;
        public int m_bInReload => m_flNextPrimaryAttack + 109;// 0x32A5;
        public int m_bIsDefusing => g_Globals.NetVars["DT_CSPlayer::m_bIsDefusing"];// 0x3930;
        public int m_bIsWalking => g_Globals.NetVars["DT_CSPlayer::m_bIsWalking"];// 0x3929;
        public int m_dwBoneMatrix=> g_Globals.NetVars["DT_BaseAnimating::m_nForceBone"] + 28;// 0x26A8;
        public int m_bSpotted => g_Globals.NetVars["DT_BaseEntity::m_bSpotted"];// 0x93D;
        public int m_bSpottedByMask => g_Globals.NetVars["DT_BaseEntity::m_bSpottedByMask"];// 0x980;
        public int m_hActiveWeapon => g_Globals.NetVars["DT_BaseCombatCharacter::m_hActiveWeapon"];// 0x2EF8;
        public int m_hMyWeapons => g_Globals.NetVars["DT_BaseCombatCharacter::m_hMyWeapons"];// 0x2DF8;
        public int m_hViewModel => g_Globals.NetVars["DT_BasePlayer::m_hViewModel[0]"];// 0x32F8;

        public int m_flFlashDuration => g_Globals.NetVars["DT_CSPlayer::m_flFlashDuration"];// 0xA420;
        public int m_flFlashMaxAlpha => g_Globals.NetVars["DT_CSPlayer::m_flFlashMaxAlpha"];// 0xA41C;

        public int m_flModelScale => g_Globals.NetVars["DT_BaseAnimating::m_flModelScale"];// 0x2748;

        public int m_nTickBase => g_Globals.NetVars["DT_LocalPlayerExclusive::m_nTickBase"];// 0x3430;


        //this is quite retarded

        private int m_Item => g_Globals.NetVars["DT_AttributeContainer::m_Item"] + g_Globals.NetVars["DT_BaseAttributableItem::m_AttributeManager"];



        public int m_iItemDefinitionIndex => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iItemDefinitionIndex"];// 0x2FAA;
        public int m_iEntityLevel => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iEntityLevel"];// 0x2FB0;
        public int m_iItemIDHigh => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iItemIDHigh"];// 0x2FC0;
        public int m_iItemIDLow => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iItemIDLow"];// 0x2FC4;
        public int m_iAccountId => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iAccountID"];// 0x2FC8;
        public int m_iEntityQuality => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_iEntityQuality"];// 0x2FAC;
        public int m_bInitialized;// 0x2FD4;
        public int m_szCustomName => m_Item + g_Globals.NetVars["DT_ScriptCreatedItem::m_szCustomName"];// 0x303C;

        public int m_flNextPrimaryAttack => g_Globals.NetVars["DT_LocalActiveWeaponData::m_flNextPrimaryAttack"];// 0x3238;
        public int m_flNextSecondaryAttack => g_Globals.NetVars["DT_LocalActiveWeaponData::m_flNextSecondaryAttack"];// 0x323C;
        public int m_flTimeWeaponIdle => g_Globals.NetVars["DT_LocalActiveWeaponData::m_flTimeWeaponIdle"];// 0x3274;

        public int m_iClip => g_Globals.NetVars["DT_BaseCombatWeapon::m_iClip1"];// 0x3264;
        public int m_iClip2 => g_Globals.NetVars["DT_BaseCombatWeapon::m_iClip2"];// 0x3268;
                                                                                  //public int m_fAccuracyPenalty;// 0x3330;// (float )
                                                                                  //public int m_fLastShotTime;// 0x33A8;// (float )
        public int m_iWeaponOrigin => g_Globals.NetVars["DT_BaseCombatWeapon::m_iWeaponOrigin"];// 0x32C8;


        public int m_hObserverTarget => g_Globals.NetVars["DT_BasePlayer::m_hObserverTarget"];// 0x338C;
        public int m_iObserverMode => g_Globals.NetVars["DT_BasePlayer::m_iObserverMode"];// 0x3378;
        public int m_iAccount => g_Globals.NetVars["DT_CSPlayer::m_iAccount"];// 0xB364;
        public int m_totalHitsOnServer => g_Globals.NetVars[
            "DT_CSPlayer::m_totalHitsOnServer"];// 0xA3A8;
        public int m_iNumRoundKills => g_Globals.NetVars["DT_CSPlayer::m_iNumRoundKills"];// 0x3954;
        public int m_iNumRoundKillsHeadshots => g_Globals.NetVars["DT_CSPlayer::m_iNumRoundKillsHeadshots"];// 0x3958;


        public int m_nLastConcurrentKilled;// 0xB3B0;
        public int m_hViewEntity => g_Globals.NetVars["DT_BasePlayer::m_hViewEntity"];// 0x333C;
        public int m_nLastKillerIndex;// 0xB3AC;



        public int m_flBeamHDRColorScale => g_Globals.NetVars["DT_Beam::m_flHDRColorScale"];// 0x09DC;//(float )



        public int m_SpatialEntityVecOrigin => g_Globals.NetVars["DT_SpatialEntity::m_vecOrigin"];// 0x09D8;



        public static Offsets Load()
        {

            //if (!System.IO.File.Exists(g_Globals.Offsets))
            //    Serializer.SaveJson(new Offsets(), g_Globals.Offsets);

            if (!System.IO.File.Exists(g_Globals.Signatures))
                Serializer.SaveJson(Generators.CreateModulePattern(), g_Globals.Signatures);

            return new Offsets();

        }

    }
}
