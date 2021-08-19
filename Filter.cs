using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using RRFull;
using System.Collections.Generic;
using System.Linq;

namespace ResurrectedEternal
{
    public static class Filter
    {
        private static Client Client => Henker.Singleton.Client;

        public static List<ChickenEntity> GetChickens()
        {
            return Client.GetChicks();
        }
        public static List<ChickenEntity> GetChickens(bool visible = false)
        {
            return Client.GetChicks().Where(x => x.Visible == visible).ToList();
        }

        public static IEnumerable<BasePlayer> GetSpectators()
        {
            return Client.GetPlayers().Where(x => !x.m_bDormant && x.m_iObserverTarget == Client.LocalPlayer.m_iIndex && !x.m_bIsAlive && x.m_iHealth <= 0 && x.m_iObserverMode == ObserverMode.OBS_MODE_IN_EYE);
        }

        public static IEnumerable<BasePlayer> GetSamePlayerSpectators()
        {
            return Client.GetPlayers().Where(x => !x.m_bDormant && x.m_iObserverTarget == Client.LocalPlayer.m_iObserverTarget && !x.m_bIsAlive && x.m_iHealth <= 0);
        }
        public static List<BasePlayer> GetActivePlayers(TargetType targetType, bool visibleFlag)
        {
            switch (targetType)
            {
                case TargetType.Enemy:
                    return Client.GetPlayers().Where(x => x.m_bIsActive && !x.IsFriendly(Client.LocalPlayer.Team) && x.IsVisible == visibleFlag && !x.m_bIsSpectator).ToList();
                case TargetType.Friendly:
                    return Client.GetPlayers().Where(x => x.m_bIsActive && x.IsFriendly(Client.LocalPlayer.Team) && x.IsVisible == visibleFlag && !x.m_bIsSpectator).ToList();
                case TargetType.All:
                default:
                    return Client.GetPlayers().Where(x => x.m_bIsActive && x.IsVisible == visibleFlag).ToList();
            }

        }
        //public static List<BasePlayer> GetActivePlayers(TargetType targetType, bool visibleFlag)
        //{
        //    switch (targetType)
        //    {
        //        case TargetType.Enemy:
        //            return Client.GetPlayers().Where(x => x.m_bIsActive && !x.IsFriendly(Client.LocalPlayer.Team) && x.IsVisible == visibleFlag && !x.m_bIsSpectator).ToList();
        //        case TargetType.Friendly:
        //            return Client.GetPlayers().Where(x => x.m_bIsActive && x.IsFriendly(Client.LocalPlayer.Team) && x.IsVisible == visibleFlag && !x.m_bIsSpectator).ToList();
        //        case TargetType.All:
        //        default:
        //            return Client.GetPlayers().Where(x => x.m_bIsActive && x.IsVisible == visibleFlag).ToList();
        //    }

        //}
        public static List<BasePlayer> GetActivePlayers(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Enemy:
                    return Client.GetPlayers().Where(x => x.m_bIsActive && !x.IsFriendly(Client.LocalPlayer.Team) && !x.m_bIsSpectator).ToList();
                case TargetType.Friendly:
                    return Client.GetPlayers().Where(x => x.m_bIsActive && x.IsFriendly(Client.LocalPlayer.Team) && !x.m_bIsSpectator).ToList();
                case TargetType.All:
                default:
                    return GetActivePlayers();
            }
            
        }

        public static List<BasePlayer> GetPlayersUnmanaged()
        {
            return Client.GetPlayers();
        }

        public static List<BasePlayer> GetActivePlayers()
        {
            return Client.GetPlayers().Where(x => x.m_bIsActive && !x.m_bIsSpectator).ToList();
        }
        public static BasePlayer[] ClearImmunity(List<BasePlayer> Players)
        {
            return Players.Where(x => !x.m_bGunGameImmunity).ToArray();
        }
    }
}
