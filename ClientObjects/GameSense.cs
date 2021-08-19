using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects
{
    public enum CurrentWarmupState
    {
        Warmup,
        Live
    }

    public enum CurrentGameMode
    {
        Normal,
        Deathmatch,
        Other
    }

    public enum BombState
    {
        Dropped,
        Carried
    }

    public enum RestartState
    {
        Restarting,
        None
    }

    class GameSense : ClientObject
    {

        //public event Action OnWarmupStart;
        //public event Action OnWarmupEnd;
        //public event Action<e_RoundEndReason> OnRoundChanged;
        //public event Action<GamePhase> OnGamePhaseChanged;

        private GameRulesProxy GameRulesProxy;

        private e_RoundEndReason CurrentRoundState;
        private GamePhase CurrentGamePhase;
        private CurrentWarmupState CurrentWarmupState;
        private CurrentGameMode CurrentGameMode;

        private BombState BombState = BombState.Dropped;

        private RestartState RestartState = RestartState.None;

        public GameSense(IntPtr moduleAddress, uint offset) : base(moduleAddress, offset)
        {
            
        }

        public void Recreate()
        {
            OnInvalidated();
        }

        private void OnInvalidated()
        {
            Pointer = MemoryLoader.instance.Reader.Read<IntPtr>(ModuleAddress + g_Globals.Offset.dwGameRulesProxy);
            GameRulesProxy = new GameRulesProxy(Pointer, ClientClass.CCSGameRulesProxy);
        }

        public new void Update()
        {
            if (GameRulesProxy == null)
                OnInvalidated();
            if (!GameRulesProxy.IsValid) OnInvalidated();
            CheckRestart();
            CheckWarmupState();
            CheckRoundState();
            CheckGamePhase();
            CheckBombState();
        }

        //private void CheckGameMode()
        //{
            
        //}

        private void CheckBombState()
        {
            
            if (!GameRulesProxy.m_bMapHasBombTarget)
                return;

            var _bomb = GameRulesProxy.m_bBombDropped;

            if(_bomb && BombState == BombState.Carried)
            {
                new BombEntityChangedEventArgs(BombEntityStateChange.Dropped);
                BombState = BombState.Dropped;
            }
            else if(!_bomb && BombState == BombState.Dropped)
            {
                new BombEntityChangedEventArgs(BombEntityStateChange.Picked);
                BombState = BombState.Carried;
            }
        }

        private void CheckRoundState()
        {
            var _roundState = GameRulesProxy.m_eRoundWinReason;
            if(CurrentRoundState != _roundState)
            {
                CurrentRoundState = _roundState;
                new GameSenseRoundChangedEventArgs(CurrentRoundState);
                //OnRoundChanged?.Invoke(_roundState);
                
            }
        }

        private void CheckGamePhase()
        {
            var _gamePhase = GameRulesProxy.m_gamePhase;
            if (CurrentGamePhase != _gamePhase)
            {
                CurrentGamePhase = _gamePhase;
                new GameSenseGamePhaseChangedEventArgs(CurrentGamePhase);
                //OnGamePhaseChanged?.Invoke(_gamePhase);
                
            }
        }

        private void CheckRestart()
        {
            var restart = GameRulesProxy.m_bGameRestart;
            if(restart && RestartState == RestartState.None)
            {
                //we're restarting.
                RestartState = RestartState.Restarting;
            }
            else if(!restart && RestartState == RestartState.Restarting)
            {
                //maybe live now i dunno
                RestartState = RestartState.None;
            }
        }

        private void CheckWarmupState()
        {
            var _warmup = GameRulesProxy.m_bWarmupPeriod;
            if (_warmup && CurrentWarmupState != CurrentWarmupState.Warmup)
            {
                new GameSenseChangedEventArgs(GameSenseState.WarmupStart);
                //OnWarmupStart?.Invoke();
                CurrentWarmupState = CurrentWarmupState.Warmup;
            }
            else if(!_warmup && CurrentWarmupState != CurrentWarmupState.Live)
            {
                new GameSenseChangedEventArgs(GameSenseState.WarmupEnd);
                //OnWarmupEnd?.Invoke();
                CurrentWarmupState = CurrentWarmupState.Live;
            }
        }


    }
}
