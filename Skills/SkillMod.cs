using ResurrectedEternal.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRWAPI;
//using MemoryShare;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Events;
using SharpDX;

namespace ResurrectedEternal.Skills
{
    class SkillMod
    {
        public Modus ClientModus => StateMachine.ClientModus;

        public Config Config => g_Globals.Config;

        public Client Client;

        public Engine Engine;

        public MapManager MapManager;


        public SkillMod(Engine engine, Client client)
        {
            Client = client;
            Engine = engine;
            MapManager = client.MapManager;
        }

        public virtual void Start()
        {

        }
        public virtual void Before()
        {

        }

        public virtual bool Update()
        {
            return true;
        }

        public virtual void AfterUpdate()
        {

        }

        public virtual void End()
        {

        }
        public virtual bool IsVisibleCheck(LocalPlayer _p, BasePlayer _target, VisibleCheck _checkType = VisibleCheck.SlowTrace)
        {

            if (MapManager.m_bForceVisibleCheck)
            {
                var _pHead = _p.m_vEyePosition;
                return MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecHead + (SharpDX.Vector3.UnitZ * 6)) || MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecChest);
            }

            switch (_checkType)
            {
                case VisibleCheck.SlowTrace:
                    return VisibleByMask(_target);
                case VisibleCheck.RayTrace:
                    var _pHead = _p.m_vEyePosition;
                    return MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecHead + (SharpDX.Vector3.UnitZ * 6)) || MapManager.m_dwMap.IsVisible(_pHead, _target.m_vecChest);
                default:
                    return true;
            }
        }

        public virtual bool VisibleByMask(BaseEntity _entity)
        {
            return (_entity.m_iSpottedByMask & 1 << Client.m_iLocalPlayerIndex - 1) != 0;
        }

        public virtual bool IsVisibleCheck(Vector3 from, Vector3 tp)
        {
            return MapManager.m_dwMap.IsVisible(from, tp);

        }
        public virtual bool IsVisibleCheck(Vector3 from, Vector3[] tp)
        {
            return MapManager.m_dwMap.IsVisible(from, tp);

        }
    }
}
