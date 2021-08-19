using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    class SkillModHBTrigger : SkillMod
    {
        private DateTime _lastTrigger = DateTime.Now;
        public SkillModHBTrigger(Engine engine, Client client) : base(engine, client)
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

        public class BestOffer
        {
            public int hitboxNum;
            public SharpDX.Vector3 _hbvecCenter;
        }

        private float GetLen(SharpDX.Vector3 _hbvec)
        {
            var _calcAngle = EngineMath.CalcAngle(Client.LocalPlayer.m_vEyePosition, _hbvec);
            var _delta = Client.LocalPlayer.m_vViewAngles - _calcAngle;
            _delta.Normalize();

            return (float)EngineMath.Lenght2D(_delta);
        }
        private TimeSpan _lastUpdate = TimeSpan.Zero;

        private BasePlayer _lastTarget;

        public override bool Update()
        {

            if (!Client.UpdateModules || !(bool)Config.AimbotConfig.trig_Enable.Value 
                || Client.LocalPlayer == null 
                || Client.m_bMouseEnabled 
                || !Engine.IsInGame
                || !Client.LocalPlayer.m_bIsAlive

                || Client.LocalPlayer.m_bIsSpectator)
                return false;


            //cancel trigger if we have user input
            if (Convert.ToBoolean(RRWAPI.WAPI.GetAsyncKeyState((ushort)VirtualKeys.LeftButton) & 0x8000)
                || Convert.ToBoolean(RRWAPI.WAPI.GetAsyncKeyState((ushort)VirtualKeys.RightButton) & 0x8000))
                return false;

            if ((bool)Config.AimbotConfig.trig_KeyEnable.Value)
                if (!Convert.ToBoolean(RRWAPI.WAPI.GetAsyncKeyState((ushort)(VirtualKeys)Config.AimbotConfig.trig_Key.Value) & 0x8000))
                    return false;


            var _localWeapon = Client.LocalPlayer.m_hActiveWeapon;

            if (_localWeapon == null)
                return false;

            var _wpType = Client.LocalPlayer.GetWeaponId;

            switch (_wpType)
            {
                case WeaponClass.RIFLE:
                case WeaponClass.HEAVY:
                case WeaponClass.SMG:
                    if (!(bool)Config.AimbotConfig.trig_Rifle.Value)
                        return false;
                    break;
                case WeaponClass.SNIPER:
                    if (!(bool)Config.AimbotConfig.trig_Snipers.Value)
                        return false;
                    break;
                case WeaponClass.PISTOL:
                    if (!(bool)Config.AimbotConfig.trig_Pistols.Value)
                        return false;
                    break;
                case WeaponClass.KNIFE:
                case WeaponClass.OTHER:
                    return false;
                default:
                    return false;
            }

            if (_localWeapon.m_iClip == 0
                || _localWeapon.m_bInReload)
            {
                return false;
            }

            var _filtered = Filter.GetActivePlayers(TargetType.Enemy);

            //var _filtered = Filter.GetPlayers(Client.LocalPlayer, Client.GetPlayers(), true, false, true, TargetType.Enemy, true);
            if (_filtered.Count == 0)
                return false;

            _lastTarget = GetBestOffer(_filtered);

            if (_lastTarget == null)
            {
                _lastTrigger = DateTime.Now;
                return false;
            }
               

            if (!_lastTarget.IsVisible)
                return false;
         
            //if ((float)Config.AimbotConfig.trig_Delay.Value > 0)
            //    if (DateTime.Now - _lastTrigger < TimeSpan.FromMilliseconds((float)Config.AimbotConfig.trig_Delay.Value))
            //        return false;

            if (Client.LocalPlayer.m_bCanShoot)
                Client.LocalPlayer.ForceAttack();

            //float time = Client.LocalPlayer.m_nTickBase * Engine.GlobalVars.m_flIntervalPerTick;

            //if (Client.LocalPlayer.m_hActiveWeapon.m_flNextPrimaryAttack <= time)
                


            return true;


        }

        private bool _isShooting = false;
        private async void HandleShot()
        {
            if (_isShooting)
                return;

        }

        private BasePlayer GetBestOffer(List<BasePlayer> _players)
        {
            var _eyePos = Client.LocalPlayer.m_vEyePosition;
            var _angles = Client.LocalPlayer.m_vViewAngles;
            foreach (var item in _players)
            {
                //item.ReadBones();
                if (!item.IsValid) continue;
                foreach (var bone in item.GetUpperBody())
                {
                    if ((float)EngineMath.GetFov(_eyePos, _angles, bone) < 1f)
                        return item;
                }
                //float _body = ;
                //float _head = (float)EngineMath.GetFov(_eyePos, _angle, item.Head);
                //float _chest = (float)EngineMath.GetFov(_eyePos, _angle, item.Chest);
                //float _lshoulder = (float)EngineMath.GetFov(_eyePos, _angle, item.LShoulder);
                //float _rshoulder = (float)EngineMath.GetFov(_eyePos, _angle, item.RShoulder);
                //float _neck = (float)EngineMath.GetFov(_eyePos, _angle, item.Neck);
                ////Console.WriteLine("TAGET {0} >>> {1} >>> {2}", item.Name, _body, _head);
                //if (_head < .555f
                //    || _body < .777f
                //    || _lshoulder < .333f
                //    || _rshoulder < .333f
                //    || _chest < .777f
                //    || _neck < .222f)
                //    return item;

            }
            return null;
        }
    }
}
