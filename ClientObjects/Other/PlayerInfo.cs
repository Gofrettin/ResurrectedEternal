using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects.Other
{
    public class PlayerInfo
    {
        public string PlayerName;
        public string FriendsName;
        public string SteamID;

        public PlayerInfo(string pn, string fn, string sid)
        {
            PlayerName = pn;
            FriendsName = fn;
            SteamID = sid;
        }
    }
}
