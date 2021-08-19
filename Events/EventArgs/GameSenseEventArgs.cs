using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events.EventArgs
{
    public enum GameSenseState
    {
        WarmupStart,
        WarmupEnd,
        Live,
        GamePhaseChanged,
        RoundStateChanged
    }

    public class GameSenseChangedEventArgs
    {
        public GameSenseState GameSenseState;
        public GameSenseChangedEventArgs(GameSenseState _newState)
        {
            GameSenseState = _newState;
            EventManager.Notify(this);
        }
    }

    public class GameSenseRoundChangedEventArgs
    {
        public e_RoundEndReason RoundState;
        public GameSenseRoundChangedEventArgs(e_RoundEndReason _r)
        {
            RoundState = _r;
            EventManager.Notify(this);
        }
    }

    public class GameSenseGamePhaseChangedEventArgs
    {
        public GamePhase GamePhase;
        public GameSenseGamePhaseChangedEventArgs(GamePhase _gp)
        {
            GamePhase = _gp;
            EventManager.Notify(this);
        }
    }


    //public class GameSenseEventArgs
    //{
        

    //    public e_RoundEndReason CurrentRoundState;
    //    public GamePhase CurrentGamePhase;
    //    //there might be a bug in here that makes it call multiple times inside fucking everything.
    //    public GameSenseEventArgs(GameSenseState _newState)
    //    {
    //        GameSenseState = _newState;
    //        EventManager.Notify(this);
    //    }
    //    public GameSenseEventArgs(GameSenseState _newState, e_RoundEndReason roundChange)
    //    {
    //        GameSenseState = _newState;
    //        CurrentRoundState = roundChange;
    //        EventManager.Notify(this);
    //    }
    //    public GameSenseEventArgs(GameSenseState _newState, GamePhase _phaseChange)
    //    {
    //        GameSenseState = _newState;
    //        CurrentGamePhase = _phaseChange;
    //        EventManager.Notify(this);
    //    }
    //    public GameSenseEventArgs(GameSenseState _newState, e_RoundEndReason roundChange, GamePhase _phaseChange)
    //    {
    //        GameSenseState = _newState;
    //        CurrentRoundState = roundChange;
    //        CurrentGamePhase = _phaseChange;
    //        EventManager.Notify(this);
    //    }
    //}
}
