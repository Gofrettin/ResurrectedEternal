using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.SoundEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.GamePlaySkillMods
{
    class SkillModSoundEffects : SkillMod
    {

        private void Reset()
        {
            //CurrentNumKills = 0;
            //CurrentNumHits = 0;
            //CurrentHeadshots = 0;
            //CurrentlyAlive = true;
            m_dwbIsLastManStanding = false;
            m_dwbIsLastManStandingEnemy = false;
        }

        private int CurrentNumKills = 0;
        private int CurrentNumHits = 0;
        private int CurrentHeadshots = 0;
        private bool CurrentlyAlive = true;
        private bool m_dwbIsLastManStanding = false;
        private bool m_dwbIsLastManStandingEnemy = false;

        public SkillModSoundEffects(Engine engine, Client client) : base(engine, client)
        {

            EventManager.OnPlayerChanged += EventManager_OnPlayerChanged;
            EventManager.OnGameSenseRoundPhaseChanged += EventManager_OnGameSenseRoundPhaseChanged;
            EventManager.OnGameSenseRoundChanged += EventManager_OnGameSenseRoundChanged;
            //EventManager.OnGameSenseChanged += EventManager_OnGameSenseChanged;
            EventManager.BombEntityChanged += EventManager_BombEntityChanged;
            EventManager.OnEngineStateChanged += EventManager_OnEngineStateChanged;
        }

        private void EventManager_OnGameSenseRoundChanged(GameSenseRoundChangedEventArgs obj)
        {
            OnRoundChanged(obj.RoundState);
        }

        private void EventManager_OnGameSenseRoundPhaseChanged(GameSenseGamePhaseChangedEventArgs obj)
        {
            OnGamePhaseChange(obj.GamePhase);
        }

        private void EventManager_OnEngineStateChanged(SignonState obj)
        {
            switch (obj)
            {
                case SignonState.None:
                    break;
                case SignonState.Challenge:
                    SoundCache.PlayRandom("onjoinserver");
                    break;
                case SignonState.Connected:
                    break;
                case SignonState.New:

                    break;
                case SignonState.PreSpawn:
                    break;
                case SignonState.Spawn:
                    break;
                case SignonState.Full:
                    break;
                case SignonState.ChangingLevel:
                    break;
                default:
                    break;
            }
        }

        private void EventManager_BombEntityChanged(BombEntityChangedEventArgs obj)
        {
            switch (obj.NewState)
            {
                case Events.EventArgs.BombEntityStateChange.Planted:
                    SoundCache.PlayRandom("onbombplanted");
                    break;
                case Events.EventArgs.BombEntityStateChange.Exploded:
                    SoundCache.PlayRandom("onbombexploded");
                    break;
                case Events.EventArgs.BombEntityStateChange.Dropped:
                    break;
                case Events.EventArgs.BombEntityStateChange.BeforeExplosion:
                    SoundCache.PlayRandom("onbombabouttoexplode");
                    break;
                case Events.EventArgs.BombEntityStateChange.Defused:
                    SoundCache.PlayRandom("onbombdefused");
                    break;
                default:
                    break;
            }
        }


        private void OnGamePhaseChange(GamePhase _gamePhase)
        {
            switch (_gamePhase)
            {
                case GamePhase.GAMEPHASE_WARMUP_ROUND:
                    break;
                case GamePhase.GAMEPHASE_PLAYING_STANDARD:
                    break;
                case GamePhase.GAMEPHASE_PLAYING_FIRST_HALF:
                    break;
                case GamePhase.GAMEPHASE_PLAYING_SECOND_HALF:
                    break;
                case GamePhase.GAMEPHASE_HALFTIME:
                    break;
                case GamePhase.GAMEPHASE_MATCH_ENDED:
                    SoundCache.PlayRandom("onmatchended");
                    break;
                case GamePhase.GAMEPHASE_MAX:
                    break;
                default:
                    break;
            }
        }

        private void OnRoundChanged(e_RoundEndReason _reason)
        {
            //Reset();
            switch (_reason)
            {
                case e_RoundEndReason.RoundEndReason_StillInProgress:
                    OnRoundStart();
                    break;
                default:
                    OnRoundEnd(Generators.GetWinningTeam(_reason));
                    break;
            }
        }

        private void EventManager_OnPlayerChanged(BasePlayerChangedEventArgs obj)
        {
            switch (obj.NewState)
            {
                case Events.EventArgs.BasePlayerStateChange.Connected:
                    OnPlayerConnected();
                    break;
                case Events.EventArgs.BasePlayerStateChange.Disconnected:
                default:
                    OnPlayerDisconnected();
                    break;
            }
        }

        public override void Before()
        {
            if (SoundCache.m_dwVolume != (float)g_Globals.Config.ExtraConfig.SoundVolume.Value)
                SoundCache.m_dwVolume = (float)g_Globals.Config.ExtraConfig.SoundVolume.Value;
            base.Before();
        }

        public override bool Update()
        {
            return base.Update();
        }
        public override void AfterUpdate()
        {
            base.AfterUpdate();


            if (!(bool)Config.ExtraConfig.Sound.Value || Client.LocalPlayer == null || !Client.LocalPlayer.IsValid)
                return;

            //if ()
            //{
            //    CurrentNumKills = 0;
            //    CurrentNumHits = 0;
            //    CurrentHeadshots = 0;
            //    return;
            //}

            //Are we currently playing deathmatch?

            if (CurrentlyAlive && !Client.LocalPlayer.m_bIsAlive)
            {
                if (Client.LocalPlayer.m_bKilledByTaser)
                    OnTaserKill();
                else
                    OnPlayerDeath();

            }
            //else if (!CurrentlyAlive && Client.LocalPlayer.m_bIsAlive)
            //{
            //    CurrentNumKills = Client.LocalPlayer.m_iNumRoundKills;
            //    CurrentNumHits = Client.LocalPlayer.m_iTotalHitsOnServer;
            //    CurrentHeadshots = Client.LocalPlayer.m_iNumRoundKillsHeadshots;
            //    CurrentlyAlive = true;
            //    //SanityChecks();
            //    return;
            //}
            else if (!CurrentlyAlive && !Client.LocalPlayer.m_bIsAlive)
                SoundCache.FadeOutAmbiente();



            if (CurrentlyAlive)
            {
                if (!m_bRoundEnded)
                {
                    if (!m_dwbIsLastManStanding && IsLastManStanding())
                    {
                        //we need da flag so we dont spam this motherfucker, but 
                        m_dwbIsLastManStanding = true;
                        SoundCache.PlayRandom("lastman");
                        SoundCache.PlayAmbiente("suspense");
                    }
                    if (!m_dwbIsLastManStandingEnemy && IsLastManStandingEnemy())
                    {
                        m_dwbIsLastManStandingEnemy = true;
                        SoundCache.PlayRandom("lastman_enemy");
                    }
                }

                if ((bool)g_Globals.Config.ExtraConfig.SoundHitmaker.Value)
                {
                    if (Client.LocalPlayer.m_iTotalHitsOnServer > CurrentNumHits)
                    {
                        //CurrentNumHits = Client.LocalPlayer.m_iTotalHitsOnServer;
                        SoundCache.PlayRandom("hitmarker");
                    }
                }

                if ((bool)g_Globals.Config.ExtraConfig.SoundQuake.Value)
                {
                    if (Client.LocalPlayer.m_iNumRoundKills > CurrentNumKills)
                    {
                        //CurrentNumKills = Client.LocalPlayer.m_iNumRoundKills;
                        SoundCache.PlaySound(Client.LocalPlayer.m_iNumRoundKills);
                    }

                    if (Client.LocalPlayer.m_iNumRoundKillsHeadshots > CurrentHeadshots)
                    {
                        //CurrentHeadshots = Client.LocalPlayer.m_iNumRoundKillsHeadshots;
                        SoundCache.PlayRandom("headshot");
                        SoundCache.PlayHeadshot(Client.LocalPlayer.m_iNumRoundKillsHeadshots);
                    }
                }
            }

            Set();
        }

        private void Set()
        {
            CurrentNumKills = Client.LocalPlayer.m_iNumRoundKills;
            CurrentNumHits = Client.LocalPlayer.m_iTotalHitsOnServer;
            CurrentHeadshots = Client.LocalPlayer.m_iNumRoundKillsHeadshots;
            CurrentlyAlive = Client.LocalPlayer.m_bIsAlive;
        }

        private void SanityChecks()
        {
            if (Client.LocalPlayer.m_iNumRoundKills < CurrentNumKills)
                CurrentNumKills = Client.LocalPlayer.m_iNumRoundKills;
            if (Client.LocalPlayer.m_iTotalHitsOnServer < CurrentNumHits)
                CurrentNumHits = Client.LocalPlayer.m_iTotalHitsOnServer;
            if (Client.LocalPlayer.m_iNumRoundKillsHeadshots < CurrentHeadshots)
                CurrentHeadshots = Client.LocalPlayer.m_iNumRoundKillsHeadshots;
            if (!CurrentlyAlive && Client.LocalPlayer.m_bIsAlive)
                CurrentlyAlive = true;

        }

        private bool IsLastManStandingEnemy()
        {
            return Filter.GetActivePlayers(TargetType.Enemy).Count == 1;
        }

        private bool IsLastManStanding()
        {
            //m_dwbIsLastManStanding = ;
            return Filter.GetActivePlayers(TargetType.Friendly).Count == 0;

            //foreach (var item in _filter)
            //{

            //    if (item.m_bIsAlive)
            //        return false;
            //}
            //return true;

        }

        public void OnPlayerDeath()
        {
            //CurrentNumKills = 0;
            //CurrentNumHits = 0;
            //CurrentHeadshots = 0;
            SoundCache.PlayRandom("onplayerdeath");
        }

        public void OnTaserKill()
        {
            SoundCache.PlayRandom("taserkill");
        }

        private void OnRoundStart()
        {
            Reset();
            m_bRoundEnded = false;
            SoundCache.PlayRandom("roundstart");
        }

        private bool m_bRoundEnded = false;

        private void OnRoundEnd(PlayerTeam _winningTeam)
        {
            m_bRoundEnded = true;
            SoundCache.FadeOutAmbiente();
            //we could check here to set some sort of flag so we dont play anymore sounds if the round has ended.
            if (_winningTeam == PlayerTeam.Neutral)
                return;
            if (Client.LocalPlayer == null)
                return;
            switch (_winningTeam)
            {
                case PlayerTeam.Neutral:
                    break;
                case PlayerTeam.Terrorist:
                    SoundCache.PlayRandom("roundend_twin");
                    break;
                case PlayerTeam.CounterTerrorist:
                    SoundCache.PlayRandom("roundend_ctwin");
                    break;
                default:
                    break;
            }
            //did we win?
            if (_winningTeam == Client.LocalPlayer.Team)
                SoundCache.PlayRandom("roundend_win");
            else
                SoundCache.PlayRandom("roundend_loss");

            //SoundCache.PlayRandom("roundend");
        }

        private void OnPlayerConnected()
        {
            SoundCache.PlayRandom("connected");
        }

        private void OnPlayerDisconnected()
        {
            SoundCache.PlayRandom("disconnected");
        }
        public override void Start()
        {
            base.Start();
        }
        public override void End()
        {
            base.End();
        }
    }
}
