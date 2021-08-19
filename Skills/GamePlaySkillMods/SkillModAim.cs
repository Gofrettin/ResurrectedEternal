using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Clockwork;
using ResurrectedEternal.Skills.GamePlaySkillMods.AimExtension;
using RRWAPI;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{

    class SkillModAim : SkillMod
    {

        private BasePlayer _target;

        private BasePlayer _previousTarget;

        private ObjectClock ObjectClock = new ObjectClock();
        float minAbsDistance = 0.25f;
        private Vector3 CurrentPunch;
        private Vector3 _oldPunch = Vector3.Zero;

        private Dictionary<ItemDefinitionIndex, WeaponAccuracy> acDict = new Dictionary<ItemDefinitionIndex, WeaponAccuracy>();

        public SkillModAim(Engine engine, Client client) : base(engine, client)
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


        float SnapshotTime => Engine.GlobalVars.m_flIntervalPerTick * Clock.DeltaTime;

        private bool m_dwMouseHeld
        {
            get
            {
                var _held = Convert.ToBoolean(WAPI.GetAsyncKeyState((ushort)Config.AimbotConfig.Key.Value) & 0x8000);
                if (!_held)
                    m_dwrcsSmooth = 0f;
                return _held;
            }
        }

        private void UpdateAccuracyPenalty()
        {
            var _active = Client.LocalPlayer.m_hActiveWeapon;
            if (_active == null)
                return;
            if (!acDict.ContainsKey(_active.m_iItemDefinitionIndex))
                acDict.Add(_active.m_iItemDefinitionIndex, new WeaponAccuracy(_active.m_iItemDefinitionIndex));
            acDict[_active.m_iItemDefinitionIndex].CanShoot(_active.m_fAccuracyPenalty, Client.LocalPlayer.m_bIsScoped);

        }

        /*
         * create a dictionary that keeps track of min and max of accuracy penalty.
         * take a value of * .25 of highest to shoot?
         * 
         */

        public override bool Update()
        {
            if (!Client.UpdateModules || Client.LocalPlayer == null || !Client.LocalPlayer.IsValid || !Client.LocalPlayer.m_bIsAlive || Client.m_bMouseEnabled || !Engine.IsInGame)
                return false;

            if ((!(bool)Config.AimbotConfig.AutoAim.Value && !m_dwMouseHeld) || !m_bValidWeaponType)
            {
                _oldPunch = Vector3.Zero;
                CurrentPunch = Vector3.Zero;
                m_dwrcsSmooth = 0f;
                Reset();
                return false;
            }

            UpdateAccuracyPenalty();

            if ((bool)Config.AimbotConfig.OnGround.Value && !((FL_TYPE)Client.LocalPlayer.m_fFlags).HasFlag(FL_TYPE.FL_ONGROUND))
            {
                _oldPunch = Vector3.Zero;
                CurrentPunch = Vector3.Zero;
                m_dwrcsSmooth = 0f;
                Reset();
                return false;
            }

            if (Client.LocalPlayer.m_vViewAngles == Vector3.Zero)
            {
                Reset();
                return false;
            }

            if ((bool)Config.AimbotConfig.ChickenAimbot.Value)
            {
                if (ChickenAimbot())
                    return false;
            }

            //lmao
            if ((bool)Config.AimbotConfig.Enable.Value)
            {
                var _filteredPlayers = Filter.GetActivePlayers((TargetType)Config.AimbotConfig.AimAt.Value);

                _target = SelectTarget(_filteredPlayers);


            }

            if (_target == null)
            {
                if ((bool)Config.AimbotConfig.EnableRCS.Value && m_bwpsrcs)
                    m_vWriteAngles = RecoilControl();
                Reset();
                return false;
            }

            //select best aimspot
            //but before that we'll check if that spot is hittable.
            if (!GetBestAimspot(_target, out var _aimspot))
            {
                Reset();
                return false;
            }

            //get new view angle

            var _newAngles = EngineMath.CalcAngle(Client.LocalPlayer.m_vEyePosition, _aimspot);

            if (_newAngles == Vector3.Zero)
            {
                Reset();
                return true;
            }

            //check if pistol / sniper
            //apply recoil control
            if ((bool)Config.AimbotConfig.RCS.Value && m_bwpsrcs)
                _newAngles = RecoilControl(_newAngles);

            //increase fov with recoil control applied
            //apply smooth
            if ((float)Config.AimbotConfig.Smooth.Value > 0f)
                _newAngles = SmoothAngles(_newAngles);

            // sanity check?
            if (float.IsNaN(_newAngles.X) || float.IsNaN(_newAngles.Y) || _newAngles == Vector3.Zero)
                return false;

            m_vWriteAngles = _newAngles;
            _previousTarget = _target;

            if ((bool)Config.AimbotConfig.Autoshoot.Value
                && !(bool)Config.AimbotConfig.trig_Enable.Value
                && Client.LocalPlayer.m_hActiveWeapon != null)
            {
                if ((float)Config.AimbotConfig.Smooth.Value == 0)
                {
                    if (m_bCanShoot)
                        Client.LocalPlayer.ForceAttack();
                    return true;
                }

                if (m_bInFov(_target.m_vecHead))
                    if (m_bCanShoot)
                        Client.LocalPlayer.ForceAttack();

            }
            return true;
        }

        private bool ChickenAimbot()
        {
            var chick = SelectChicken();
            if (chick == null)
                return false;

            var _newAngles = EngineMath.CalcAngle(Client.LocalPlayer.m_vEyePosition, chick.Head);
            if (float.IsNaN(_newAngles.X) || float.IsNaN(_newAngles.Y))
                return false;

            if (chick.m_vecOrigin == Vector3.Zero || chick.Head == Vector3.Zero)
                return false;

            if (_newAngles == Vector3.Zero)
                return false;

            //apply recoil control
            if ((bool)Config.AimbotConfig.RCS.Value)
                _newAngles = RecoilControl(_newAngles);

            //apply smooth
            if ((float)Config.AimbotConfig.Smooth.Value > 0f)
                _newAngles = SmoothAngles(_newAngles);


            if (float.IsNaN(_newAngles.X) || float.IsNaN(_newAngles.Y) || _newAngles == Vector3.Zero)
                return false;

            m_vWriteAngles = _newAngles;
            if ((bool)Config.AimbotConfig.Autoshoot.Value
              && !(bool)Config.AimbotConfig.trig_Enable.Value
              && Client.LocalPlayer.m_hActiveWeapon != null)
            {
                if (m_bInFov(chick.Head))
                    if (m_bCanShoot)
                        Client.LocalPlayer.ForceAttack();



            }
            return true;
        }


        private bool m_bwpsrcs
        {
            get
            {
                var _activeWeapon = Client.LocalPlayer.m_hActiveWeapon;

                if (_activeWeapon == null)
                    return false;

                var _wpClass = Generators.GetWeaponType(_activeWeapon.m_iItemDefinitionIndex);
                switch (_wpClass)
                {

                    case WeaponClass.HEAVY:
                    case WeaponClass.SMG:
                    case WeaponClass.RIFLE:
                        return true;
                    case WeaponClass.SNIPER:
                        return (bool)Config.AimbotConfig.Sniper.Value;
                    case WeaponClass.PISTOL:
                        return (bool)Config.AimbotConfig.Pistol.Value;
                    case WeaponClass.KNIFE:
                    case WeaponClass.OTHER:
                    default:
                        return false;
                }

            }
        }


        private Vector3 m_vWriteAngles
        {
            set
            {
                var _v = value;
                _v.Z = 0;
                _v = EngineMath.ClampAngle(_v);
                Client.LocalPlayer.m_vViewAngles = _v;
            }

        }

        private void Reset()
        {
            //StartAngle = Vector3.Zero;
            //CurrentPunch = Vector3.Zero;
            Client.LocalPlayer.m_bIsShooting = false;
            _target = null;
            _previousTarget = null;
            ResetSmoothTimer();
        }


        //retamper the recoil control to use a couple more parameters to smooth dynamics

        private Vector3 RecoilControl(Vector3 _newAngles)
        {
            //value * Percentage =  smt += snasphottime * rcstime;
            CurrentPunch = Client.LocalPlayer.m_vaimPunchAngle * m_dwrcsSmooth; // + oldPunch;
            _oldPunch = CurrentPunch;
            return _newAngles - CurrentPunch;
            //Config.fRecoilControlForce);
            //oldPunch = LocalPlayer.lpInfo.PunchAngle;
        }



        private Vector3 RecoilControl()
        {


            var _viewangle = Client.LocalPlayer.m_vViewAngles + _oldPunch;
            var _punch = Client.LocalPlayer.m_vaimPunchAngle * m_dwrcsSmooth;
            if (_punch == Vector3.Zero)
                return Client.LocalPlayer.m_vViewAngles;

            _viewangle -= _punch;
            _oldPunch = _punch;
            return _viewangle;
        }

        private Vector3 SmoothAngles(Vector3 _newAngles)
        {
            //_curangle - newangles > clamp > dest = 
            var _curAngle = Client.LocalPlayer.m_vViewAngles;
            var _angle = EngineMath.ClampAngle(_curAngle - _newAngles);
            var _dbg = _angle / (float)Config.AimbotConfig.SmoothOffset.Value;
            //System.IO.File.AppendAllText("Smooth.log", string.Format("{0} - {1} - {2}\n", _curAngle, _angle, _dbg));
            var _finalDestination = _curAngle - _dbg;

            return CreateSmooth(_curAngle, _finalDestination);

        }

        private Vector3 CreateSmooth(Vector3 start, Vector3 end)
        {

            if (_target != _previousTarget)
                ResetSmoothTimer();
            //Console.WriteLine(Percentage + "%");


            switch ((SmoothType)Config.AimbotConfig.SmoothType.Value)
            {
                case SmoothType.SmoothStep:
                    return Vector3.SmoothStep(start, end, Percentage);
                case SmoothType.Lerp:
                default:
                    return Vector3.Lerp(start, end, Percentage);
            }
        }


        private float _curValue = 0f;
        private float _smoothValue => (float)Config.AimbotConfig.Smooth.Value;

        private float Percentage
        {
            get
            {
                _curValue += SnapshotTime * (float)Config.AimbotConfig.SmoothTimeMultiplier.Value;
                if (_curValue >= _smoothValue)
                {
                    if ((bool)g_Globals.Config.AimbotConfig.NonStick.Value)
                        ResetSmoothTimer();
                    else
                        _curValue = _smoothValue;
                    if (_curValue <= 0)
                        return 0;
                    return _curValue / _smoothValue;
                }
                return _smoothValue;
            }
        }

        private float m_rcsSmooth = 0f;

        private float m_rcsCompensation => (float)Config.AimbotConfig.Compensation.Value;

        private float m_dwSmoothOffset => (float)Config.AimbotConfig.RCSSmoothOffset.Value;

        /// <summary>
        /// smooth factory defines the maximum.
        /// </summary>
        private float m_dwSmoothFactor => (float)Config.AimbotConfig.RCSSmoothFactor.Value;

        private RCSSmoothType m_dwrcsSmoothType => (RCSSmoothType)Config.AimbotConfig.RCSSmoothType.Value;

        private float m_dwCurrentShots => Convert.ToSingle(Client.LocalPlayer.m_iShotsFired);
        private float m_dwMaxShots => Convert.ToSingle(Config.AimbotConfig.Shots.Value);
        private float m_dwrcsSmooth
        {
            get
            {
                switch (m_dwrcsSmoothType)
                {
                    case RCSSmoothType.Adaptive:
                        m_rcsSmooth += SnapshotTime * m_dwSmoothOffset;
                        if (m_rcsSmooth >= m_dwSmoothFactor || m_rcsSmooth == 0)
                            return m_rcsCompensation;
                        return EngineMath.Clamp((m_rcsSmooth / m_dwSmoothFactor) * m_rcsCompensation, 0, m_rcsCompensation);
                    case RCSSmoothType.Incremental:
                        //use min shots max clip?
                        if (Client.LocalPlayer.m_hActiveWeapon == null)
                            return m_rcsCompensation;
                        if (m_dwCurrentShots >= m_dwMaxShots)
                            return m_rcsCompensation;
                        if (m_dwCurrentShots == 0)
                            return 1; //this should actually be right?

                        return EngineMath.Clamp(m_dwCurrentShots / m_dwMaxShots, 0f, 1f) * m_rcsCompensation;


                    case RCSSmoothType.None:
                    default:
                        if (m_dwCurrentShots < m_dwMaxShots)
                            return 1; //this should actually be right?
                        else
                            return m_rcsCompensation;
                }



            }
            set { m_rcsSmooth = value; }
        }

        private bool m_bCalcAccuracy => (bool)Config.AimbotConfig.accuracypenalty.Value;

        private void ResetSmoothTimer()
        {

            _curValue = 0;
        }

        private float m_fAngle => (float)Config.AimbotConfig.Angle.Value;

        //private AimPoint m_apSpotPriority => (AimPoint)Config.AimbotConfig.SpotPriority.Value;

        private BasePlayer SelectTarget(List<BasePlayer> _targets)
        {
            //remove players that have the immunity flag.
            var _select = Filter.ClearImmunity(_targets);
            var _curMin = double.MaxValue;
            BasePlayer _candidate = null;
            var _spot = Client.LocalPlayer.m_vEyePosition;
            var _vang = Client.LocalPlayer.m_vViewAngles - CurrentPunch; //subtract  current punch
            foreach (var item in _select)
            {
                if (!item.m_bIsActive) continue;
                if (!VisCheck(item) && !WB(item)) continue;

                double _fovRange = 0;
                switch (m_apPoint)
                {
                    case AimPoint.Head:
                        _fovRange = EngineMath.RealFovDistance(_spot, _vang, item.m_vecHead);
                        break;
                    case AimPoint.Chest:
                        _fovRange = EngineMath.RealFovDistance(_spot, _vang, item.m_vecChest);
                        break;
                    case AimPoint.Body:
                    case AimPoint.Any:
                    default:
                        _fovRange = EngineMath.RealFovDistance(_spot, _vang, item.m_vecBody);
                        break;
                }
                if (_fovRange <= m_fAngle && _fovRange < _curMin)
                {
                    _curMin = _fovRange;
                    _candidate = item;
                }

            }

            return _candidate;

        }

        private AimPoint m_apPoint => (AimPoint)Config.AimbotConfig.Aimpoint.Value;

        private bool m_bWallBang => (bool)Config.AimbotConfig.Wallbang.Value;

        private bool WB(BasePlayer _t)
        {
            if (!m_bWallBang)
                return false;
            if (Client.MapManager.VisibleCheckAvailable)
            {
                var _active = Client.LocalPlayer.m_hActiveWeapon;
                if (_active == null)
                    return false;
                var _v = _t.m_v3aPseudoPredict;
                for (int i = 0; i < _v.Length; i++)
                {
                    if (Client.MapManager.m_dwMap.Wrap(Client.LocalPlayer.m_vEyePosition, _v[i], _active.m_iItemDefinitionIndex))
                        return true;
                }
                return false;
            }

            else return false;
        }
        private bool VisCheck(BasePlayer _target)
        {
            var _pHead = Client.LocalPlayer.m_vEyePosition;
            //Console.WriteLine(_pHead);
            //eye to eye?
            if (Client.MapManager.VisibleCheckAvailable && (VisibleCheck)Config.AimbotConfig.VisibleCheckOption.Value == global::VisibleCheck.RayTrace)
                if (m_bWallBang)
                    return IsVisibleCheck(_pHead, _target.m_v3aPseudoPredict);
                else
                    return IsVisibleCheck(_pHead, _target.m_vecBody);
            else
                return VisibleByMask(_target);
        }

        private bool GetBestAimspot(BasePlayer _target, out Vector3 aimspot)
        {

            var _aimspot = (AimPoint)Config.AimbotConfig.Aimpoint.Value;

            switch (_aimspot)
            {
                case AimPoint.Head:
                    aimspot = _target.m_vecHead;
                    return true;
                case AimPoint.Chest:
                    aimspot = _target.m_vecChest;
                    return true;
                case AimPoint.Body:
                    aimspot = _target.m_vecBody;
                    return true;
                case AimPoint.Any:
                default:
                    //probably needs a visible check if we can actually aim at this point
                    //we can implement ratio for damage here.
                    var _targetPoints = _target.GetUpperBody();
                    aimspot = _target.m_vecHead;
                    double max = EngineMath.RealFovDistance(Client.LocalPlayer.m_vEyePosition, Client.LocalPlayer.m_vViewAngles, _target.m_vecHead);                  
                    //we want to always shoot in the head.
                    for (int i = 0; i < _targetPoints.Length; i++)
                    {
                        var _next = EngineMath.RealFovDistance(Client.LocalPlayer.m_vEyePosition, Client.LocalPlayer.m_vViewAngles, _targetPoints[i]);
                        if (_next < max)
                        {
                            max = _next;
                            aimspot = _targetPoints[i];
                        }
                            
                    }
                    return true;
            }

        }



        private ChickenEntity SelectChicken()
        {
            var eyePos = Client.LocalPlayer.m_vEyePosition;
            var _playerAngle = Client.LocalPlayer.m_vViewAngles;
            var _chicks = Filter.GetChickens(true).OrderBy(
                x => EngineMath.RealFovDistance(eyePos, _playerAngle, x.Head)
                ).Where(x => EngineMath.RealFovDistance(eyePos, _playerAngle, x.Head) < (float)Config.AimbotConfig.Angle.Value).ToList();
            if (_chicks == null || _chicks.Count == 0)
                return null;
            var _playerPos = Client.LocalPlayer.m_vecOrigin;
            return _chicks.OrderBy(x => x.Distance(_playerPos)).FirstOrDefault();

        }

        private bool m_bInFov(Vector3 _pos)
        {
            float distance = Client.LocalPlayer.Distance(_pos);
            float fov = Convert.ToSingle(EngineMath.GetFov(Client.LocalPlayer.m_vEyePosition, Client.LocalPlayer.m_vViewAngles, _pos));

            //the further away the smaller the fov, the close the greater

            float casualDistance = minAbsDistance * (fov * (distance / 1000f));

            if (casualDistance > 0.1333f)
                return false;
            return true;
        }

        private bool m_bCanShoot
        {
            get
            {
                //UpdateAccuracyPenalty();
                //apply accuracy penalty?
                var _active = Client.LocalPlayer.m_hActiveWeapon;
                if (_active == null)
                {
                    Client.LocalPlayer.m_bIsShooting = false;
                    return false;
                }
                if (m_bCalcAccuracy)
                    if (acDict.ContainsKey(_active.m_iItemDefinitionIndex))
                        if (!acDict[_active.m_iItemDefinitionIndex].CanShoot(_active.m_fAccuracyPenalty, Client.LocalPlayer.m_bIsScoped))
                        {
                            Client.LocalPlayer.m_bIsShooting = false;
                            return false;
                        }

                float time = Client.LocalPlayer.m_nTickBase * Engine.GlobalVars.m_flIntervalPerTick;
                if (_active.m_flNextPrimaryAttack <= time)
                    return true;

                Client.LocalPlayer.m_bIsShooting = false;
                return false;
            }
        }

        private bool m_bValidWeaponType
        {
            get
            {
                var _activeWeapon = Client.LocalPlayer.m_hActiveWeapon;

                if (_activeWeapon == null)
                    return false;
                if (!_activeWeapon.IsValid)
                    return false;
                //both of these trigger the jump when clip is empty after? we need to reset
                if (_activeWeapon.m_iClip == 0)
                    return false;
                if (_activeWeapon.m_bInReload)
                    return false;



                var _wpType = Client.LocalPlayer.GetWeaponClass(_activeWeapon.m_iItemDefinitionIndex);

                if (_wpType == WeaponClass.KNIFE
                    || _wpType == WeaponClass.OTHER)
                    return false;

                return true;
            }


        }
    }
}
