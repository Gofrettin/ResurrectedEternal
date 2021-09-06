using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.BaseObjects.Fire;
using ResurrectedEternal.ClientObjects.Cvars;
using ResurrectedEternal.ClientObjects.ThreadUpdate;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects
{

    class Client : ClientObject
    {



        public bool UpdateModules               = false;
        public bool DontGlow                    = false;
        private bool MouseEnabled               = false;
        public IntPtr PlayerResources           => MemoryLoader.instance.Reader.Read<IntPtr>(ModuleAddress + g_Globals.Offset.dwPlayerResource);
        public IntPtr GlowPointer               => MemoryLoader.instance.Reader.Read<IntPtr>(ModuleAddress + g_Globals.Offset.dwGlowObjectManager);

        private DateTime LastEntityUpdate       = DateTime.Now;
        private TimeSpan EntityUpdateInterval   = TimeSpan.FromMilliseconds(25);
        private BaseEntity[] WorldEntites       = new BaseEntity[4095];
        private SignonState PreviousState       = SignonState.None;

        public LocalPlayer LocalPlayer;
        public NetworkStringTable NetworkStringTable;
        public MapManager MapManager;
        private Engine Engine;
        public GameSense GameSense;
        private ThreadedUpdate ThreadedUpdate;

        public void PushCip(string message, float duration, SharpDX.Color clr)
        {
            new PushClipEventArgs(message, duration, clr);
        }
        public Client(IntPtr moduleAddress, uint offset, Engine engine) : base(moduleAddress, offset)
        {
            Pointer = moduleAddress + (int)offset;
            Engine = engine;
            EventManager.WindowStateChanged += EventManager_WindowStateChanged;
            EventManager.MenuStateChanged += EventManager_MenuStateChanged;
            EventManager.OnGameSenseChanged += EventManager_OnGameSenseChanged1;
            EventManager.OnGameSenseRoundChanged += EventManager_OnGameSenseRoundChanged;
            EventManager.OnGameSenseRoundPhaseChanged += EventManager_OnGameSenseRoundPhaseChanged;
            EventManager.OnMapChanged += EventManager_OnMapChanged;
            NetworkStringTable = new NetworkStringTable(Engine.Pointer, (uint)g_Globals.Offset.dwModelPrecacheTable);
            ThreadedUpdate = new ThreadedUpdate(Engine);
            GameSense = new GameSense(moduleAddress, (uint)g_Globals.Offset.dwGameRulesProxy);
            MapManager = new MapManager(this);

        }

        private void EventManager_OnGameSenseRoundPhaseChanged(GameSenseGamePhaseChangedEventArgs obj)
        {
            GameSense_OnGamePhaseChanged(obj.GamePhase);
        }

        private void EventManager_OnGameSenseRoundChanged(GameSenseRoundChangedEventArgs obj)
        {
            GameSense_OnRoundChanged(obj.RoundState);
        }

        private void EventManager_OnGameSenseChanged1(GameSenseChangedEventArgs obj)
        {
            switch (obj.GameSenseState)
            {
                case GameSenseState.WarmupStart:
                    GameSense_OnWarmupStart();
                    break;
                case GameSenseState.WarmupEnd:

                    GameSense_OnWarmupEnd();
                    break;
                case GameSenseState.Live:
                case GameSenseState.GamePhaseChanged:
                case GameSenseState.RoundStateChanged:
                default:
                    break;
            }
        }

        private void EventManager_MenuStateChanged(EventManager.MenuState obj)
        {
            switch (obj)
            {
                case EventManager.MenuState.Open:
                    MouseEnabled = true;
                    break;
                case EventManager.MenuState.Close:
                default:
                    MouseEnabled = false;
                    break;
            }
        }

        private void EventManager_WindowStateChanged(EventManager.WindowState _newState)
        {
            switch (_newState)
            {
                case EventManager.WindowState.Foreground:
                    MouseEnabled = false;
                    break;
                case EventManager.WindowState.Background:
                    MouseEnabled = true;

                    break;
                default:
                    break;
            }
        }

        private void EventManager_OnMapChanged(MapChangedEventArgs obj)
        {
            var _oldMap = string.IsNullOrEmpty(obj.OldMap) ? "-" : obj.OldMap;

            PushCip(string.Format("Map Changed from {0} to {1}: ", _oldMap, obj.MapName), 6.66f, SharpDX.Color.Gainsboro);
        }

        ConvarEntity _clMouse;

        private void Clear()
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null) continue;
                WorldEntites[i] = null;
            }
            LocalPlayer = null;
            NetworkStringTable.IsValid = false;

        }


        private bool HandleGameState()
        {
            if (PreviousState != Engine.SignonState)
            {
                //Console.WriteLine(Engine.SignonState);
                PreviousState = Engine.SignonState;
                EventManager.Notify(PreviousState);
                PushCip(string.Format("New State: {0}", Engine.SignonState), 5f, SharpDX.Color.Red);
                if (PreviousState == SignonState.Full)
                {
                    GameSense = new GameSense(ModuleAddress, (uint)g_Globals.Offset.dwGameRulesProxy);
                    GameSense.Recreate();
                    NetworkStringTable = new NetworkStringTable(Engine.Pointer, (uint)g_Globals.Offset.dwModelPrecacheTable);
                    NetworkStringTable.ReValidate();
                    return true;
                }
                else
                {
                    Clear();
                    return false;
                }

            }

            if (PreviousState == SignonState.Full)
                return true;

            return false;

        }

        public ClientMode ClientMode = ClientMode.Continue;

        public new UpdateResult Update()
        {
            ClientMode = ClientMode.Continue;
            if (!HandleGameState())
                return UpdateResult.None;

            //var _peter = Engine.Pointer + 0x52C0;


            MapManager.Update();
            if (ClientMode == ClientMode.Return)
                return UpdateResult.Pending;

            LoadEntities();

            if ((DateTime.Now - LastEntityUpdate) > EntityUpdateInterval)
            {

                var _first = MemoryLoader.instance.Reader.Read<CEntInfo>(Pointer);
                while (_first.m_pNext != IntPtr.Zero)
                {
                    if (_first.m_pNext == IntPtr.Zero)
                        continue;
                    SortEntity(_first.m_pEntity);
                    _first = MemoryLoader.instance.Reader.Read<CEntInfo>(_first.m_pNext);
                }
                LastEntityUpdate = DateTime.Now;
            }

            UpdateEntities();
            if (GameSense != null)
                GameSense.Update();
            //if (LocalPlayer == null || !LocalPlayer.IsValid)
            //    return UpdateResult.Pending;
            return UpdateResult.OK;
        }


        //We have to load the configuration for each map individually when the map changes, so we might want to implement that





        public GlowManager m_dwGlowManager
        {
            get { return MemoryLoader.instance.Reader.Read<GlowManager>(ModuleAddress + g_Globals.Offset.dwGlowObjectManager); }
        }

        internal BaseEntity GetEntityByClass(ClientClass _requestedEntity)
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null) continue;
                if (WorldEntites[i].ClientClass == _requestedEntity) return WorldEntites[i];
            }
            return null;
        }
        internal BaseEntity GetEntityById(int id)
        {
            return WorldEntites[id];
        }


        public bool m_bMouseEnabled
        {
            get
            {
                if (MouseEnabled)
                    return true;

                if (_clMouse == null)
                    _clMouse = ConvarManager.instance.GetConvar("cl_mouseenable");

                return _clMouse.m_nValue == 0;
            }
        }
        private void UpdateEntities()
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null || WorldEntites[i].ClientClass == ClientClass.CRopeKeyframe) continue;

                //just in case we somehow ended up in here
                if (LocalPlayer != null)
                    if (WorldEntites[i].BaseAddress == LocalPlayer.BaseAddress)
                    {
                        WorldEntites[i] = null;
                        continue;
                    }

                if (WorldEntites[i].IsValid) continue;

                if (WorldEntites[i].ClientClass == ClientClass.CCSPlayer)
                {
                    //var _p =  as BasePlayer;
                    SpawnPlayerEvent(WorldEntites[i], BasePlayerStateChange.Disconnected);
                    //new BasePlayerChangedEventArgs(_p.m_iIndex, _p.m_szPlayerName, BasePlayerStateChange.Disconnected);
                    //OnPlayerDisconnected?.Invoke(_p.m_iIndex, _p.m_szPlayerName);
                }
                WorldEntites[i] = null;

            }
            ValidateLocalPlayer();
        }

        private void SortEntity(IntPtr _entityAdress)
        {
            if (_entityAdress == IntPtr.Zero)
                return;
            if (LocalPlayer != null)
                if (_entityAdress == LocalPlayer.BaseAddress)
                    return;
            ThreadedUpdate.Add(_entityAdress);
        }

        internal void Dirty()
        {
            Clear();
        }

        private void LoadEntities()
        {
            while (ThreadedUpdate.Get(out var _ent))
            {

                if (_ent == null)
                    continue;

                var _index = _ent.m_iIndex;

                if (_index < 0 || _index > 4095) continue;

                if (LocalPlayer != null)
                    if (_ent.BaseAddress == LocalPlayer.BaseAddress)
                        continue;

                Resize(_index);
                //var _worldEnd = ;
                if (WorldEntites[_index] == null)
                {
                    if (_ent.IsValid)
                    {
                        WorldEntites[_index] = _ent;
                        SpawnPlayerEvent(_ent, BasePlayerStateChange.Connected);
                    }

                    continue;
                }
                else
                {


                    if (WorldEntites[_index].ClientClass == _ent.ClientClass
                        && WorldEntites[_index].BaseAddress == _ent.BaseAddress
                        && WorldEntites[_index].IsValid
                        && WorldEntites[_index].m_iIndex == _index)
                        continue;

                    if (!WorldEntites[_index].IsValid)
                        WorldEntites[_index] = null;

                    if (_ent.IsValid)
                    {
                        WorldEntites[_index] = _ent;
                        if (WorldEntites[_index].ClientClass == ClientClass.CCSPlayer)
                        {
                            SpawnPlayerEvent(_ent, BasePlayerStateChange.Connected);
                        }
                    }
                }






            }
        }



        private void SpawnPlayerEvent(BaseEntity bp, BasePlayerStateChange _state)
        {
            if (bp.ClientClass != ClientClass.CCSPlayer)
                return;

            var _p = bp as BasePlayer;
            new BasePlayerChangedEventArgs(_p.m_iIndex, _p.m_szPlayerName, _state);
        }

        private void GameSense_OnWarmupStart()
        {
            PushCip("Currently in warmup...", 7f, SharpDX.Color.White);
        }

        private void GameSense_OnWarmupEnd()
        {
            PushCip("Warmup has ended!", 7f, SharpDX.Color.Green);
        }

        //is there actually a bug or is this some kind of wizardry?

        public int m_iLocalPlayerIndex => MemoryLoader.instance.Reader.Read<int>(Engine.Pointer + g_Globals.Offset.m_dwLocalPlayerIndex) + 1;

        private void ValidateLocalPlayer()
        {
            var _localPlayerIndex = m_iLocalPlayerIndex;

            if (_localPlayerIndex == 0)
            {
                LocalPlayer = null;
                return;
            }

            if (WorldEntites[_localPlayerIndex] == null)
                return;

            var _localPlayer = WorldEntites[_localPlayerIndex];

            if (_localPlayer != null)
            {

                //there's a base entity sitting here that is the local player.

                //are we a null? lets assign ourselves.
                if (LocalPlayer == null)
                    LocalPlayer = new LocalPlayer(_localPlayer.BaseAddress, ClientClass.CCSPlayer);
                //if (LocalPlayer.BaseAddress == _localPlayer.BaseAddress)
                //    return;
                //are we valid tho?
                if (!LocalPlayer.IsValid || LocalPlayer.BaseAddress != _localPlayer.BaseAddress)
                {
                    LocalPlayer = new LocalPlayer(_localPlayer.BaseAddress, ClientClass.CCSPlayer);
                }
                //remove us from the world entity list so we dont get caught up
                WorldEntites[_localPlayerIndex] = null;
                return;
            }

        }

        private void GameSense_OnRoundChanged(e_RoundEndReason _roundState)
        {
            switch (_roundState)
            {
                case e_RoundEndReason.RoundEndReason_StillInProgress:
                    //SoundEngine.OnRoundStart();
                    LastEntityUpdate.AddSeconds(-2);
                    //Delay the dont glow by 250ms
                    Task.Factory.StartNew(() =>
                    {
                        Task.Delay(1111).Wait();
                        DontGlow = false;
                    });
                    break;
                default:
                    DontGlow = true;
                    //The round has ended.
                    break;
            }
            PushCip(Generators.FormatNiceResult(_roundState), 12f, SharpDX.Color.RoyalBlue);
        }

        private void GameSense_OnGamePhaseChanged(GamePhase _gamePhase)
        {
            switch (_gamePhase)
            {

                case GamePhase.GAMEPHASE_MATCH_ENDED:
                    UpdateModules = false;

                    break;
                case GamePhase.GAMEPHASE_MAX:
                case GamePhase.GAMEPHASE_WARMUP_ROUND:
                case GamePhase.GAMEPHASE_PLAYING_STANDARD:
                case GamePhase.GAMEPHASE_PLAYING_FIRST_HALF:
                case GamePhase.GAMEPHASE_PLAYING_SECOND_HALF:
                case GamePhase.GAMEPHASE_HALFTIME:
                default:
                    UpdateModules = true;
                    break;
            }

            PushCip(_gamePhase.ToString(), 10f, SharpDX.Color.Cyan);

        }

        private void Resize(int newLength)
        {
            if (WorldEntites.Length < newLength)
                Array.Resize(ref WorldEntites, newLength + 1);

        }

        public IEnumerable<Sprite> GetSprites()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CSprite).Cast<Sprite>();
        }
        public IEnumerable<Beam> GetBeams()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CBeam).Cast<Beam>();
        }
        public IEnumerable<BeamEnd> GetBeamEnds()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CSpotlightEnd).Cast<BeamEnd>();
        }
        public List<BasePlayer> GetPlayers()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CCSPlayer).Cast<BasePlayer>().ToList();
        }
        public List<ChickenEntity> GetChicks()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CChicken).Cast<ChickenEntity>().ToList();
            //return PlayerEntities.Values.Where(x => x.ReadClassId() == vmtClassId.CCSPlayer && x.IsValid).ToList();
        }
        public List<BaseCombatWeapon> GetGroundWeapons()
        {
            return WorldEntites.Where(x => x != null && Generators.IsWeapon(x.ClientClass)).Cast<BaseCombatWeapon>().ToList();
            //return PlayerEntities.Values.Where(x => x.ReadClassId() == vmtClassId.CCSPlayer && x.IsValid).ToList();
        }

        public List<BaseGrenade> GetGroundGrenades()
        {
            return WorldEntites.Where(x => x != null && Generators.IsGrenade(x.ClientClass) && x.GetType() == typeof(BaseGrenade)).Cast<BaseGrenade>().ToList();
        }

        public List<ProjectileEntity> GetProjectiles()
        {
            return WorldEntites.Where(x => x != null && Generators.IsProjectile(x.ClientClass)).Cast<ProjectileEntity>().ToList();
        }
        public List<EconEntity> GetDefuseKits()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CEconEntity).Cast<EconEntity>().ToList();
        }
        public List<BaseEntity> GetAll()
        {
            return WorldEntites.Where(x => x != null).ToList();
            //return BaseEntities.Values.ToList();
        }

        public IEnumerable<Inferno> GetInfernos()
        {
            return WorldEntites.Where(x => x != null && x.ClientClass == ClientClass.CInferno).Cast<Inferno>();
        }

        public IEnumerable<T> GetEntitiesByClassId<T>(ClientClass _className)
        {
            return (IEnumerable<T>)WorldEntites.Where(x => x != null && x.ClientClass == _className);
        }

        public PlantedBombEntity GetPlantedC4()
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null) continue;
                if (WorldEntites[i].ClientClass == ClientClass.CPlantedC4)
                    return WorldEntites[i] as PlantedBombEntity;
            }
            return null;
        }


        public BombEntity GetC4()
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null) continue;
                if (WorldEntites[i].ClientClass == ClientClass.CC4)
                    return WorldEntites[i] as BombEntity;
            }
            return null;
        }
        public PredictedViewModel GetViewModel(int id)
        {
            if (id == 4095)
                return null;
            if (WorldEntites[id] == null || WorldEntites[id].ClientClass != ClientClass.CPredictedViewModel)
            {
                var _ent = GetEntityPtr(id - 1);
                if (_ent == IntPtr.Zero)
                    return null;
                WorldEntites[id] = new PredictedViewModel(_ent, ClientClass.CPredictedViewModel);
            }

            //HAHAHAHAHAHA WAS IST DAS FÜR EINE MENTALE RETARDATION

            return WorldEntites[id] as PredictedViewModel;
            //return GroundWeapons.Where(x => x.Value.m_iIndex == id).First().Value;
        }
        public BaseCombatWeapon GetWeaponById(int id)
        {
            if (id >= 4095)
                return null;

            if (WorldEntites[id] == null)
            {
                var _ent = GetEntityPtr(id - 1);
                if (_ent == IntPtr.Zero)
                    return null;
                WorldEntites[id] = new BaseCombatWeapon(_ent, ReadClassId(_ent));
            }

            return WorldEntites[id] as BaseCombatWeapon;
            //return GroundWeapons.Where(x => x.Value.m_iIndex == id).First().Value;
        }
        public BaseCombatWeapon[] GetWeaponById(int[] ids)
        {
            BaseCombatWeapon[] _weapons = new BaseCombatWeapon[ids.Length];
            for (int i = 0; i < ids.Length; i++)
                _weapons[i] = WorldEntites[(ids[i])] as BaseCombatWeapon;

            return _weapons;
        }

        public BasePlayer GetPlayerById(int idx)
        {
            if (LocalPlayer.m_iIndex == idx)
                return LocalPlayer;
            if (WorldEntites.Length > idx)
            {
                return WorldEntites[idx] as BasePlayer;
            }
            return null;
        }

        private IntPtr GetEntityPtr(int index)
        {
            // ptr = entityList + (idx * size)
            return MemoryLoader.instance.Reader.Read<IntPtr>(Pointer + index * (int)g_Globals.Offset.EntitySize);
        }

        public BaseEntity GetEntityByAddress(IntPtr addr)
        {
            for (int i = 0; i < WorldEntites.Length; i++)
            {
                if (WorldEntites[i] == null) continue;
                if (WorldEntites[i].BaseAddress == addr && WorldEntites[i].IsValid)
                    return WorldEntites[i];
            }
            return null;
        }

        private ClientClass ReadClassId(IntPtr _EntityAddress)
        {
            try
            {
                return (ClientClass)MemoryLoader.instance.Reader.Read<int>(MemoryLoader.instance.Reader.Read<IntPtr>(MemoryLoader.instance.Reader.Read<IntPtr>(MemoryLoader.instance.Reader.Read<IntPtr>(_EntityAddress + 0x8) + 0x8) + 0x1) + 0x14);
            }
            catch { return 0; }
        }

    }
}
