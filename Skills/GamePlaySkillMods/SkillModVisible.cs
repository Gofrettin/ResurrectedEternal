using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    class SkillModVisible : SkillMod
    {
        private DateTime _lastUpdate = DateTime.Now;
        //no need to go hard on visual checks for the esp since the aimbot already does heavy checks
        private TimeSpan _interval = TimeSpan.FromMilliseconds(20);

        public SkillModVisible(Engine engine, Client client) : base(engine, client)
        {
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        public override void Before()
        {
            base.Before();
            if (Client == null || !Client.UpdateModules || Client.LocalPlayer == null || !Client.LocalPlayer.IsValid /*|| !MapManager.VisibleCheckAvailable*/)
                return;

            if (DateTime.Now - _lastUpdate < _interval)
                return;

            CheckPlayers();
            CheckFlashProjectiles();
            CheckNuggets();

            _lastUpdate = DateTime.Now;

        }

        private void CheckPlayers()
        {
            foreach (var item in Filter.GetActivePlayers((TargetType)Config.VisualConfig.Type.Value))
            {
                if (item.m_bIsActive)
                {
                    //if (item.ValidBoneMatrix)
                    item.IsVisible = IsVisibleCheck(Client.LocalPlayer, item, (VisibleCheck)Config.AimbotConfig.VisibleCheckOption.Value);
                    item.m_dtLastVisCheck = DateTime.Now;
                    continue;
                }
                else
                    item.IsVisible = false;
            }
        }

        private void CheckFlashProjectiles()
        {
            if ((bool)Config.VisualConfig.FlashWarning.Value)
            {
                //var _flashes = .Where(x => Generators.IsFlashbang(x.m_szModelName));
                var _locPos = Client.LocalPlayer.m_vecHead;
                foreach (var item in Client.GetProjectiles())
                {
                    if (!Generators.IsFlashbang(item.m_szModelName)) continue;
                    if (item.IsValid)
                    {
                        if ((VisibleCheck)Config.AimbotConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace && MapManager.VisibleCheckAvailable)
                            item.IsVisible = IsVisibleCheck(Client.LocalPlayer.m_vecHead, item.m_vecOrigin);
                        else
                            item.IsVisible = false;
                    }
                    else
                        item.IsVisible = false;
                }

            }
        }

        private void CheckNuggets()
        {
            if ((bool)Config.AimbotConfig.ChickenAimbot.Value)
            {
                foreach (var item in Client.GetChicks())
                {
                    if (!item.m_bIsActive)
                    {
                        if ((VisibleCheck)Config.AimbotConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace && MapManager.VisibleCheckAvailable)
                            item.Visible = IsVisibleCheck(Client.LocalPlayer.m_vecHead, item.Head);
                        else
                            item.Visible = VisibleByMask(item);
                    }
                    else
                        item.Visible = false;
                }
            }
        }


        public override bool IsVisibleCheck(LocalPlayer _p, BasePlayer _target, VisibleCheck _checkType)
        {
            return base.IsVisibleCheck(_p, _target, _checkType);
        }

        public override bool VisibleByMask(BaseEntity _entity)
        {
            return base.VisibleByMask(_entity);
        }

        public override bool IsVisibleCheck(Vector3 from, Vector3 tp)
        {
            return base.IsVisibleCheck(from, tp);
        }

        public override bool Update()
        {
            return base.Update();
        }
    }
}
