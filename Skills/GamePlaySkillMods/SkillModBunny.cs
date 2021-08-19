using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using RRWAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    class SkillModBunny : SkillMod
    {
        public SkillModBunny(Engine engine, Client client) : base(engine, client)
        {
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        public override void Before()
        {
            base.Before();
        }
        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammode
                || ClientModus == Events.Modus.streammodefull)
                return false;
            return true;
        }
        public override bool Update()
        {
            if (!CanProcess())
                return false;
            if (!Client.UpdateModules || Client.m_bMouseEnabled
                || Client.LocalPlayer == null
                || !Client.LocalPlayer.IsValid
                || !Engine.IsInGame
                || !Client.LocalPlayer.m_bIsAlive
                || Client.LocalPlayer.m_bIsSpectator)
            {
                return false;
            }

            if ((bool)Config.ExtraConfig.Bunnyhop.Value && Convert.ToBoolean(WAPI.GetAsyncKeyState((int)VirtualKeys.Space) & 0x8000))
            {
                Client.LocalPlayer.BunnyJump();
                //Autstrafer causes vac errors for some reason, enable with care.
				//if ((bool)Config.ExtraConfig.autostrafe.Value)
                    //Client.LocalPlayer.AutoStrafe();
            }
            return true;

        }
    }
}
