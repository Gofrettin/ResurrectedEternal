using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.ClientObjects.Cvars;
using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.GenericObjects;
using ResurrectedEternal.Memory;
using ResurrectedEternal.Objects;
using ResurrectedEternal.Params.CSHelper;
using RRWAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    //public class SkillModConvarUpdateEntry
    //{
    //    public ConVar Convar;
    //    public ConfigValueEntry ConfigValueEntry;
    //}

    public class ConvarWrapper
    {
        /// <summary>
        /// The convar entry
        /// </summary>
        public ConvarEntity ConvarEntity;

        /// <summary>
        /// pointer to the config value entry
        /// </summary>
        public ConfigValueEntry Entry;


        public ConvarWrapper(ConvarEntity ent)
        {
            ConvarEntity = ent;
        }

        public ConvarWrapper(ConvarEntity ent, ConfigValueEntry entry)
        {
            ConvarEntity = ent;
            Entry = entry;
        }

        public void UpdateValueEntry(ConfigValueEntry entry)
        {
            if (Entry != entry)
                Entry = entry;
        }

        public void UpdateConvar()
        {
            ConvarEntity.m_pThis = ConvarManager.instance.GetConVarAddress(Entry.ConvarName);
        }
    }

    class SkillModConvar : SkillMod
    {
        private TimeSpan _verifyTime = TimeSpan.FromMilliseconds(20);
        private DateTime _lastUpdate = DateTime.Now;

        private Dictionary<string, ConvarWrapper> ConvarHashtable = new Dictionary<string, ConvarWrapper>();
        private Dictionary<string, ConfigValueEntry> RequirementHeaders = new Dictionary<string, ConfigValueEntry>();
        private List<ConvarEntityEventArgs> SavedConvars = new List<ConvarEntityEventArgs>();
        private GenericQueue<ConvarEntityEventArgs> ConvarQueue = new GenericQueue<ConvarEntityEventArgs>();
        private GenericQueue<RemoveConvarEventArgs> RemoveConvarQueue = new GenericQueue<RemoveConvarEventArgs>();
        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammode
                || ClientModus == Events.Modus.streammodefull)
                return false;
            return true;
        }
        public SkillModConvar(Engine engine, Client client) : base(engine, client)
        {
            EventManager.OnConvarChanged += EventManager_OnConvarAdded;
            EventManager.OnRemoveConvar += EventManager_OnRemoveConvar;
            EventManager.OnConvarShow += EventManager_OnConvarShow;
            GetConvars();
        }

        private void EventManager_OnConvarShow()
        {
            if (SavedConvars == null)
                return;
            foreach (var item in SavedConvars)
            {
                ConsoleHelper.Write(item.m_pszConvarName, 1, ConsoleColor.Cyan);
                ConsoleHelper.Write(item.m_flValue.ToString("0.00"), 33, ConsoleColor.Red);
                ConsoleHelper.Write(item.m_nValue.ToString(), 2, ConsoleColor.Red);
                ConsoleHelper.Write(item.m_pszValue + "\n", 2, ConsoleColor.Red);

            }
            ConsoleHelper.Write(SavedConvars.Count + " convars active!\n", ConsoleColor.Green);
        }

        private void EventManager_OnRemoveConvar(RemoveConvarEventArgs _removeme)
        {
            RemoveConvarQueue.m_pAdd = _removeme;
        }

        private void EventManager_OnConvarAdded(ConvarEntityEventArgs convarEvent)
        {
            ConvarQueue.m_pAdd = convarEvent;
        }

        private void RemovePendingConvars()
        {
            if (!RemoveConvarQueue.m_bAvailable)
                return;

            while (RemoveConvarQueue.m_bAvailable)
            {
                var _nextEntry = RemoveConvarQueue.m_pNext;

                if (_nextEntry == null)
                    return;

                //this would also remove any convar that is defaulted to be used lol
                if (ConvarHashtable.ContainsKey(_nextEntry.m_pszConvarName))
                    ConvarHashtable.Remove(_nextEntry.m_pszConvarName);

                if (SavedConvars.Where(x => x.m_pszConvarName == _nextEntry.m_pszConvarName).Any())
                {
                    var _select = SavedConvars.Where(x => x.m_pszConvarName == _nextEntry.m_pszConvarName).First();
                    SavedConvars.Remove(_select);
                }

            }
            Serializer.SaveJson(SavedConvars, g_Globals.ConvarConfig);
        }

        private void HandleQueue()
        {
            if (!ConvarQueue.m_bAvailable)
                return;

            while (ConvarQueue.m_bAvailable)
            {
                var _nextEntry = ConvarQueue.m_pNext;

                //LOL
                if (ConvarHashtable.ContainsKey(_nextEntry.m_pszConvarName))
                {
                    //did the value change?
                    var _ent = ConvarHashtable[_nextEntry.m_pszConvarName];
                    if (_ent.Entry.Value.GetType() == typeof(int) || _ent.Entry.Value.GetType() == typeof(bool))
                    {
                        //dont bother

                        if (_nextEntry.m_nValue == Convert.ToInt32(_ent.Entry.Value))
                            continue;
                        _ent.Entry.Value = _nextEntry.m_nValue;
                        //SavedConvars.Where(x => x.m_pszConvarName == _nextEntry.m_pszConvarName).FirstOrDefault();
                    }
                    else if (_ent.Entry.Value.GetType() == typeof(float))
                    {
                        if (_nextEntry.m_flValue == (float)_ent.Entry.Value)
                            continue;
                        _ent.Entry.Value = _nextEntry.m_flValue;
                    }
                    else
                    {
                        if (_nextEntry.m_pszValue == (string)_ent.Entry.Value)
                            continue;
                        _ent.Entry.Value = _nextEntry.m_pszValue;
                        //string
                    }
                    continue;


                }
                if (!SavedConvars.Where(x => x.m_pszConvarName == _nextEntry.m_pszConvarName).Any())
                {
                    SavedConvars.Add(_nextEntry);
                }

                ConvarHashtable.Add(_nextEntry.m_pszConvarName, new ConvarWrapper(_nextEntry.ConvarEntity, Create(_nextEntry)));


                //handle this motherfucker

            }

            //save json.
            Serializer.SaveJson(SavedConvars, g_Globals.ConvarConfig);


        }

        private ConfigValueEntry Create(ConvarEntityEventArgs _nextEntry)
        {
            var _cfgEntry = new ConfigValueEntry();
            if (_nextEntry.isFloat)
            {
                _cfgEntry.DefaultValue = Convert.ToSingle(_nextEntry.ConvarEntity.m_pszDefaultValue.Replace("f", "").Replace(".", ","));
                _cfgEntry.Value = _nextEntry.m_flValue;
            }
            else if (_nextEntry.isInt)
            {
                _cfgEntry.DefaultValue = Convert.ToInt32(_nextEntry.ConvarEntity.m_pszDefaultValue);
                _cfgEntry.Value = _nextEntry.m_nValue;
            }
            else
            {
                _cfgEntry.DefaultValue = _nextEntry.ConvarEntity.m_pszDefaultValue;
                _cfgEntry.Value = _nextEntry.m_pszValue;
            }
            return _cfgEntry;
        }

        private void GetConvars()
        {
            Type type = Config.GetType();
            var _fieldInfo = type.GetFields();
            //ITerate over Aimbot, Visual etc.
            foreach (var item in _fieldInfo)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(Config);
                if (_newitem == null) continue;
                var _cfgvalEntryProps = _newitem.GetType().GetFields();

                //entry to Aimbot and its entries
                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    //Console.WriteLine(" >>>> Name: " + prop.Name + ", Value: " + prop.GetValue(_newitem));
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;

                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                    if (cfgItem == null) continue;



                    if (!string.IsNullOrEmpty(cfgItem.ConvarName))
                    {
                        if (cfgItem.ConvarName == "fov_cs_debug") continue;

                        if (ConvarHashtable.ContainsKey(cfgItem.ConvarName)) continue;


                        if (!string.IsNullOrEmpty(cfgItem.ConvarRequire) && !RequirementHeaders.ContainsKey(cfgItem.ConvarRequire))
                        {
                            RequirementHeaders.Add(
                                cfgItem.ConvarRequire,
                                _cfgvalEntryProps.Where(x => x.Name == cfgItem.ConvarRequire).FirstOrDefault().GetValue(_newitem) as ConfigValueEntry);
                        }

                        ConvarHashtable.Add(cfgItem.ConvarName, new ConvarWrapper(ConvarManager.instance.GetConvar(cfgItem.ConvarName.ToLower()), cfgItem));

                    }
                }


            }


            //var _result = Henker.RPC.Config(1, ConfigType.Convars, new byte[0]);
            //if (_result.Length == 0)
            //    return;
            SavedConvars = Configs.ConfigSystem.Serializer.LoadJson<List<ConvarEntityEventArgs>>(g_Globals.ConvarConfig);
            if (SavedConvars == null)
                return;
            foreach (var item in SavedConvars)
            {
                if (!ConvarHashtable.ContainsKey(item.m_pszConvarName))
                {
                    item.ConvarEntity = ConvarManager.instance.GetConvar(item.m_pszConvarName);
                    ConvarHashtable.Add(item.m_pszConvarName, new ConvarWrapper(item.ConvarEntity, Create(item)));
                }

            }


        }

        public override void End()
        {
            base.End();
            if (!CanProcess())
                return;
            foreach (var item in ConvarHashtable)
            {
                if (item.Value.ConvarEntity == null)
                    item.Value.ConvarEntity = ConvarManager.instance.GetConvar(item.Value.Entry.ConvarName);

                if (!string.IsNullOrEmpty(item.Value.Entry.ConvarRequire))
                    if (!IsActiveConvar(item.Value.Entry.ConvarRequire))
                    {
                        Revert(item.Value);
                        //however, we need to revert to default values.
                        continue;
                    }


                if (!item.Value.ConvarEntity.IsValid)
                {
                    item.Value.UpdateConvar();
                    continue;
                }

                Apply(item.Value);
                //what is this convar type


            }
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        private bool IsActiveConvar(string header)
        {
            return (bool)RequirementHeaders[header].Value;
        }

        public override void Start()
        {
            base.Start();
            HandleQueue();
            RemovePendingConvars();

        }

        public override void Before()
        {
            base.Before();



        }


        public override bool Update()
        {
            if (!CanProcess())
                return false;
            //if ((DateTime.Now - _overrideUpdate < _override))

            PerformLag();
            DropPackets();

            if ((DateTime.Now - _lastUpdate) < _verifyTime)
                return false;

            HandleModelScaler();
            HandleFovConvar();
            HandleNoFlash();

            _lastUpdate = DateTime.Now;
            return base.Update();
        }


        private void HandleNoFlash()
        {
            if (Client.LocalPlayer == null || !Client.LocalPlayer.IsValid)
                return;
            if (Client.LocalPlayer.m_dwFlashAlpha != (float)g_Globals.Config.ExtraConfig.FlashAlpha.Value)
            {
                Client.LocalPlayer.m_dwFlashAlpha = (float)g_Globals.Config.ExtraConfig.FlashAlpha.Value;
            }
        }

        private void Revert(ConvarWrapper _wrap)
        {
            //what is this type
            var _type = _wrap.Entry.Value.GetType();

            object _typus;
            //???
            if (_wrap.Entry.DefaultValue != null)
                _typus = _wrap.Entry.DefaultValue;

            if (_type == typeof(float))
            {
                _typus = Convert.ToSingle(_wrap.ConvarEntity.m_pszDefaultValue.Replace("f", ""));
                if (_wrap.ConvarEntity.m_flValue != (float)_typus)
                    _wrap.ConvarEntity.m_flValue = (float)_typus;
            }

            else if (_type == typeof(int) || _type == typeof(bool))
            {
                if (_type == typeof(bool))
                    _typus = Convert.ToInt32(_wrap.ConvarEntity.m_pszDefaultValue);
                else
                    _typus = (int)_wrap.Entry.Value;

                if (_wrap.ConvarEntity.m_nValue != (int)_typus)
                    _wrap.ConvarEntity.m_nValue = (int)_typus;
            }

            else
            {
                _typus = _wrap.ConvarEntity.m_pszDefaultValue;
                if (_wrap.ConvarEntity.m_pszValue != (string)_typus)
                    _wrap.ConvarEntity.m_pszValue = (string)_typus;
            }

        }

        private void Apply(ConvarWrapper _wrap)
        {
            var _type = _wrap.Entry.Value.GetType();

            //if(_wrap.ConvarEntity.m_pszName == "cl_ragdoll_gravity")

            //{
            //    Console.WriteLine("breakpoint");
            //}

            if (_type == typeof(float))
            {
                var _float = (float)_wrap.Entry.Value;
                if (_wrap.ConvarEntity.m_flValue != _float)
                    _wrap.ConvarEntity.m_flValue = _float;
                //we want to write m_flValue
            }
            else if (_type == typeof(int) || _type == typeof(bool))
            {
                //we want to write m_nValue
                var _numValue = 0;

                if (_type == typeof(bool))
                    _numValue = Convert.ToInt32(_wrap.Entry.Value);
                else
                    _numValue = (int)_wrap.Entry.Value;

                if (_wrap.ConvarEntity.m_nValue != _numValue)
                    _wrap.ConvarEntity.m_nValue = _numValue;

            }
            else
            {
                //we want to write m_pszValue
                var _stringValue = _wrap.Entry.Value.ToString();

                if (_wrap.ConvarEntity.m_pszValue != _stringValue)
                    _wrap.ConvarEntity.m_pszValue = _stringValue;

            }
        }

        /// <summary>
        /// ghetto shit
        /// </summary>
        private ConvarEntity FovConvar;

        private void HandleFovConvar()
        {
            if (FovConvar == null)
                FovConvar = ConvarManager.instance.GetConvar("fov_cs_debug");

            if (!(bool)Config.HudConfig.FOVChanger.Value)
            {
                if (FovConvar.m_flValue != 0f)
                    FovConvar.m_flValue = 0f;
                return;
            }





            if (Client.LocalPlayer == null || !Client.LocalPlayer.m_bIsAlive)
                return;
            if (Client.LocalPlayer.m_bIsScoped)
            {
                if (FovConvar.m_flValue > 0f)
                    FovConvar.m_flValue = 0f;
                return;
            }

            if (FovConvar.m_flValue != (float)Config.HudConfig.FOVAmount.Value)
                FovConvar.m_flValue = (float)Config.HudConfig.FOVAmount.Value;


        }

        private void HandleModelScaler()
        {
            if (!(bool)Config.ExtraConfig.EnableModelScale.Value)
                return;

            ScalePlayers();
            ScaleProjectiles();
            ScaleWeapons();
            ScaleDefuseKits();
            ScaleGrenades();
            ScaleBombs();


        }

        private void ScaleProjectiles()
        {
            var _projectiles = Client.GetProjectiles();

            foreach (var item in _projectiles)
            {
                if (item.IsValid)
                    if (item.m_flModelScale != (float)Config.ExtraConfig.ProjectileScale.Value)
                        item.m_flModelScale = (float)Config.ExtraConfig.ProjectileScale.Value;
            }

        }

        private void ScaleBombs()
        {
            var _droppedBomb = Client.GetEntityByClass(ClientClass.CC4);
            if (_droppedBomb != null)
            {
                if (_droppedBomb.m_flModelScale != (float)Config.ExtraConfig.BomScale.Value)
                    _droppedBomb.m_flModelScale = (float)Config.ExtraConfig.BomScale.Value;
            }

            var _plantedBomb = Client.GetEntityByClass(ClientClass.CPlantedC4);
            if (_plantedBomb != null)
            {
                if (_plantedBomb.m_flModelScale != (float)Config.ExtraConfig.BomScale.Value)
                    _plantedBomb.m_flModelScale = (float)Config.ExtraConfig.BomScale.Value;
            }

        }

        private void ScaleDefuseKits()
        {
            var _defKits = Client.GetDefuseKits();
            foreach (var item in _defKits)
            {
                if (item.IsValid)
                    if (item.m_flModelScale != (float)Config.ExtraConfig.DefuseScale.Value)
                        item.m_flModelScale = (float)Config.ExtraConfig.DefuseScale.Value;
            }
        }

        private void ScaleWeapons()
        {
            var _weps = Client.GetGroundWeapons();
            foreach (var item in _weps)
            {
                if (item.IsValid)
                    if (item.m_flModelScale != (float)Config.ExtraConfig.WeaponScale.Value)
                        item.m_flModelScale = (float)Config.ExtraConfig.WeaponScale.Value;
            }
            //if (Client.LocalPlayer == null || Client.LocalPlayer.m_hViewModelWeapon == null)
            //    return;
            //Client.LocalPlayer.m_hViewModelWeapon.m_flModelScale = (float)Config.ExtraConfig.WeaponScale.Value;
        }

        private void ScaleGrenades()
        {

            var grenades = Client.GetGroundGrenades();

            foreach (var item in grenades)
            {
                if (item.IsValid)
                    if (item.m_flModelScale != (float)Config.ExtraConfig.GrenadeScale.Value)
                        item.m_flModelScale = (float)Config.ExtraConfig.GrenadeScale.Value;
            }
        }

        private void ScalePlayers()
        {
            var _targets = Filter.GetActivePlayers();
            foreach (var item in _targets)
            {
                if (item.m_bIsActive)
                    if (item.m_flModelScale != (float)Config.ExtraConfig.PlayerScale.Value)
                        item.m_flModelScale = (float)Config.ExtraConfig.PlayerScale.Value;
            }
        }

        private ConvarEntity m_ceFakeLag;
        private ConvarEntity m_ceFakeJitter;
        private ConvarEntity m_ceFakeLoss;
        private ConvarEntity m_ceDropPackets;

        private TimeSpan m_tsMs;
        private DateTime m_dwtLastLag;

        private TimeSpan m_tsMsDrop;
        private DateTime m_dtLastDrop;

        private Random _random = new Random();

        private int _nextFakeLagRandom = 0;
        private int _nextFakeLossRandom = 0;
        private int _nextFakeJitterRandom = 0;
        private int _nextDropPacketsRandom = 0;

        private void PerformLag()
        {
            if (m_ceFakeLag == null)
                m_ceFakeLag = ConvarManager.instance.GetConvar("net_fakelag");

            if (m_ceFakeJitter == null)
                m_ceFakeJitter = ConvarManager.instance.GetConvar("net_fakejitter");

            if (m_ceFakeLoss == null)
                m_ceFakeLoss = ConvarManager.instance.GetConvar("net_fakeloss");

            if (!(bool)Config.OtherConfig.bEnableFakeLag.Value || Client.LocalPlayer.m_bIsShooting)
            {
                if (m_ceFakeLag != null)
                    if (m_ceFakeLag.m_flValue != 0f)
                        m_ceFakeLag.m_flValue = 0f;

                if (m_ceFakeJitter != null)
                    if (m_ceFakeJitter.m_flValue != 0f)
                        m_ceFakeJitter.m_flValue = 0f;

                if (m_ceFakeLoss != null)
                    if (m_ceFakeLoss.m_flValue != 0f)
                        m_ceFakeLoss.m_flValue = 0f;
                return;
            }

            if (!m_dwLagKey)
                return;

            if (DateTime.Now - m_dwtLastLag < m_tsMs)
                return;

            SetLag();

            if ((bool)Config.OtherConfig.bEnableRandomize.Value)
            {

                m_dwtLastLag = DateTime.Now;

                m_tsMs = TimeSpan.FromMilliseconds(
                    EngineMath.GetRandomNumber(
                        (int)Config.OtherConfig.RandomMin.Value,
                        (int)Config.OtherConfig.RandomMax.Value, _random)
                    );


            }

            if (m_ceFakeLag != null)
                if (m_ceFakeLag.m_flValue != _nextFakeLagRandom)
                    m_ceFakeLag.m_flValue = _nextFakeLagRandom;

            if (m_ceFakeJitter != null)
                if (m_ceFakeJitter.m_flValue != _nextFakeJitterRandom)
                    m_ceFakeJitter.m_flValue = _nextFakeJitterRandom;

            if (m_ceFakeLoss != null)
                if (m_ceFakeLoss.m_flValue != _nextFakeLossRandom)
                    m_ceFakeLoss.m_flValue = _nextFakeLossRandom;
        }

        private void SetLag()
        {
            if ((bool)Config.OtherConfig.bEnableRandomize.Value)
            {

                m_dwtLastLag = DateTime.Now;

                m_tsMs = TimeSpan.FromMilliseconds(
                    EngineMath.GetRandomNumber(
                        (int)Config.OtherConfig.RandomMin.Value,
                        (int)Config.OtherConfig.RandomMax.Value, _random)
                    );

                _nextFakeLagRandom = _random.Next(
                    (int)Config.OtherConfig.FakeLagMin.Value,
                    (int)Config.OtherConfig.FakeLagMax.Value
                    );

                _nextFakeLossRandom = _random.Next(
                    (int)Config.OtherConfig.FakeLossMin.Value,
                    (int)Config.OtherConfig.FakeLossMax.Value
                    );

                _nextFakeJitterRandom = _random.Next(
                    (int)Config.OtherConfig.FakeJitterMin.Value,
                    (int)Config.OtherConfig.FakeJitterMax.Value
                    );

                _nextDropPacketsRandom = _random.Next(
                    (int)Config.OtherConfig.DropPacketMin.Value,
                    (int)Config.OtherConfig.DropPacketMax.Value
                    );
                return;
            }
            
            
                _nextFakeLagRandom = (int)Config.OtherConfig.FakeLagMin.Value;
                _nextFakeLossRandom = (int)Config.OtherConfig.FakeLossMin.Value;
                _nextFakeJitterRandom = (int)Config.OtherConfig.FakeJitterMin.Value;
                _nextDropPacketsRandom = (int)Config.OtherConfig.DropPacketMin.Value;
            
        }

        private void DropPackets()
        {
            if (m_ceDropPackets == null)
                m_ceDropPackets = ConvarManager.instance.GetConvar("net_droppackets");

            if (!(bool)Config.OtherConfig.EnableDropPackets.Value || Client.LocalPlayer.m_bIsShooting)
            {
                if (m_ceDropPackets != null)
                    if (m_ceDropPackets.m_nValue != 0)
                        m_ceDropPackets.m_nValue = 0;
                return;
            }

            if (DateTime.Now - m_dtLastDrop < m_tsMsDrop || !m_dwLagKey)
                return;


            if (m_ceDropPackets != null)
                if (m_ceDropPackets.m_nValue != _nextDropPacketsRandom)
                    m_ceDropPackets.m_nValue = _nextDropPacketsRandom;

            m_dtLastDrop = DateTime.Now;

            if (!(bool)Config.OtherConfig.bEnableRandomize.Value)
            {
                m_tsMsDrop = TimeSpan.FromMilliseconds((float)Config.OtherConfig.PacketIntervalMin.Value);
                return;
            }
            m_tsMsDrop = TimeSpan.FromMilliseconds(
                    EngineMath.GetRandomNumber(
                        (float)Config.OtherConfig.PacketIntervalMin.Value,
                        (float)Config.OtherConfig.PacketIntervalMax.Value, _random)
                    );
           
        }

        private bool m_dwLagKey
        {
            get
            {
                if (!(bool)Config.OtherConfig.bUseLagKey.Value)
                    return true;
                return Convert.ToBoolean(WAPI.GetAsyncKeyState((ushort)Config.OtherConfig.vkLagey.Value) & 0x8000);
            }
        }
    }
}
