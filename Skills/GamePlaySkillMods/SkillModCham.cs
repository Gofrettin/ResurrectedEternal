using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    class SkillModCham : SkillMod
    {
        private DateTime _lastUpdate = DateTime.Now;
        private TimeSpan _interval = TimeSpan.FromMilliseconds(30);
        public SkillModCham(Engine engine, Client client) : base(engine, client)
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        SharpDX.Color _first;
        SharpDX.Color _defaultColor;
        bool _defaultColorSaved = false;
        bool _defaultColorRestored = false;

        bool viewmodelSaved = false;
        bool viewmodelRestored = false;


        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode)
                return false;
            if (ClientModus == Events.Modus.streammode)
                return false;
            return true;
        }

        public override bool Update()
        {
            if (!CanProcess())
                return false;
            if (!Client.UpdateModules || Client.DontGlow || Client.LocalPlayer == null
                || !Client.LocalPlayer.IsValid
                || !Engine.IsInGame)
                return false;

            if (DateTime.Now - _lastUpdate < _interval)
                return false;


            if ((bool)Config.NeonConfig.SetViewmodelColor.Value)
            {
                if (!viewmodelSaved)
                {
                    if (Client.LocalPlayer.m_hViewModelWeapon != null)
                    {
                        _first = Client.LocalPlayer.m_hViewModelWeapon.m_clrRender;
                        viewmodelSaved = true;
                        return false;
                    }
                    return false;
                }
                if (Client.LocalPlayer.m_hViewModelWeapon != null)/* Config.ColorConfig.cViewmodelColor*/
                    if (Client.LocalPlayer.m_hViewModelWeapon.m_clrRender != (SharpDX.Color)Config.OtherConfig.cViewmodelColor.Value)
                        Client.LocalPlayer.m_hViewModelWeapon.m_clrRender = (SharpDX.Color)Config.OtherConfig.cViewmodelColor.Value;
                //Client.LocalPlayer.m_clrRender = ;
                viewmodelRestored = false;

            }
            else
            {
                if (!viewmodelRestored)
                {
                    if (Client.LocalPlayer.m_hViewModelWeapon != null)/* Config.ColorConfig.cViewmodelColor*/
                    {
                        if (Client.LocalPlayer.m_hViewModelWeapon.m_clrRender != _first)
                            Client.LocalPlayer.m_hViewModelWeapon.m_clrRender = _first;
                        viewmodelRestored = true;
                    }

                }

            }






            if ((bool)Config.NeonConfig.EnableCham.Value)
            {
                var _players = Filter.GetActivePlayers(TargetType.Enemy);

                foreach (var p in _players)
                {
                    if (!p.m_bIsActive || !p.IsValid) continue;

                    if (!_defaultColorSaved)
                    {
                        _defaultColor = p.m_clrRender;
                        _defaultColorSaved = true;
                        _defaultColorRestored = false;
                    }
                    if ((bool)Config.NeonConfig.Modelcolors.Value)
                        p.SetRenderColor(Generators.GetColorBySetting(Config, p.Team, p.IsVisible, p.m_bGunGameImmunity));
                    //p.SetRenderColor(c.R, c.G, c.B, c.A);
                }
            }
            else
            {
                if (!_defaultColorRestored)
                {
                    var _players = Filter.GetActivePlayers(TargetType.Enemy);

                    foreach (var p in _players)
                    {
                        if (!p.m_bIsActive || !p.IsValid) continue;

                        p.SetRenderColor(_defaultColor);
                        //p.SetRenderColor(c.R, c.G, c.B, c.A);
                    }
                    _defaultColorRestored = true;
                }
            }


            _lastUpdate = DateTime.Now;

            return true;
        }
    }
}
