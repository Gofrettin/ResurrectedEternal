using ResurrectedEternal.Configs.ConfigSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events
{
    public enum Modus
    {
        full,
        novisuals,
        streammodefull,
        streammode,
        leaguemode
    }
    public static class StateMachine
    {
        public static Modus ClientModus = Modus.full;


        static StateMachine()
        {
            ClientModus = Serializer.LoadJson<Modus>(g_Globals.ModeConfig);
            //if (_resultset.Length == 0)
            //    return;

             //(Modus)_resultset[0];
        }

        public static void SetNewClientMode(Modus _clMode)
        {
            ClientModus = _clMode;
        }


    }
}
