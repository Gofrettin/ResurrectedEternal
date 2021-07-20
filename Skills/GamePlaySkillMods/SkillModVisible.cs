using RRFull.BaseObjects;
using RRFull.ClientObjects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRFull.Skills
{
    class SkillModVisible : SkillMod
    {
        private DateTime _lastUpdate = DateTime.Now;
        //no need to go hard on visual checks for the esp since the aimbot already does heavy checks
        private TimeSpan _interval = TimeSpan.FromMilliseconds(20);

        private MapManager MapManager;


        public SkillModVisible(Engine engine, Client client) : base(engine, client)
        {
            MapManager = client.MapManager;
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
                    VisibleCheck(Client.LocalPlayer, item);
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
                        if ((VisibleCheck)Config.OtherConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace && MapManager.VisibleCheckAvailable)
                            item.IsVisible = VisibleCheck(Client.LocalPlayer.m_vecHead, item.m_vecOrigin);
                        else
                            item.IsVisible = VisibleByMask(item);
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
                        if ((VisibleCheck)Config.OtherConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace && MapManager.VisibleCheckAvailable)
                            item.Visible = VisibleCheck(Client.LocalPlayer.m_vecHead, item.Head);
                        else
                            item.Visible = VisibleByMask(item);
                    }
                    else
                        item.Visible = false;
                }
            }
        }


        private void VisibleCheck(LocalPlayer _p, BasePlayer _target)
        {


            //var _pHead = _p.m_vecHead;
            var _pHead = _p.m_vEyePosition;
            //Console.WriteLine(_pHead);
            //eye to eye?
            if (MapManager.VisibleCheckAvailable && (VisibleCheck)Config.OtherConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace)
                _target.IsVisible = MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecHead + (SharpDX.Vector3.UnitZ * 6)) || MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecChest);
            else
                _target.IsVisible = VisibleByMask(_target);


        }
        private bool VisibleByMask(BaseEntity _entity)
        {
            return (_entity.m_iSpottedByMask & 1 << Client.m_iLocalPlayerIndex - 1) != 0;
        }

        private bool VisibleCheck(Vector3 from, Vector3 tp)
        {
            return MapManager.m_dwMap.IsVisible(from, tp);

        }

        public override bool Update()
        {
            return base.Update();
        }
    }
}
