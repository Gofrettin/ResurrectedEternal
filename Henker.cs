
using ResurrectedEternal.MemoryManager;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.ClientObjects.Cvars;
using ResurrectedEternal.Clockwork;
using ResurrectedEternal.Configs;
using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Events;
using ResurrectedEternal.GenericObjects;
using ResurrectedEternal.Memory;
using ResurrectedEternal.Params.CSHelper;
using ResurrectedEternal.Skills;
using ResurrectedEternal.Skills.EnvironmentSkillMods;
using ResurrectedEternal.Skills.GamePlaySkillMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ResurrectedEternal;

namespace RRFull
{
    class Henker
    {
        public static Henker Singleton;
        public MemoryLoader Memory;
        public Engine Engine;
        public Client Client;
        public ConvarManager ConvarManager;
        public long m_lCurrentFPS = 0;
        private long m_lRenderedFrames = 0;
        private DateTime m_dtPOreviousFrameUpdate = DateTime.Now;
        private DateTime m_dtPreviousDeltaUpdate = DateTime.Now;
        private Thread m_tEntityUpdateThread;
        private List<SkillMod> m_aSkillMods = new List<SkillMod>();
        private bool m_bPprocessActive = false;

        public Henker()
        {
            ConsoleHelper.Write("[Resurrected SoL F8 Framework]\n", ConsoleColor.Blue);
            g_Globals.Offset = Offsets.Load();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            Clock.Initialize();

            if (!System.IO.File.Exists(g_Globals.NickConfig))
                Serializer.SaveJson(ConsoleHelper.ColorCaptureInput("Enter Nickname"), g_Globals.NickConfig);

            g_Globals.Nickname = Serializer.LoadJson<string>(g_Globals.NickConfig);

            ConsoleHelper.ShowAction("User Found! ", 33);
            ConsoleHelper.ConfirmAction("Welcome " + g_Globals.Nickname + "! =)");

            // create event listener for panic key press
            EventManager.OnPanic += EventManager_OnPanic;

            //we might aswell call Start in here and leave it private.
            Singleton = this;
            Start();

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
           //Console.WriteLine(e.ToString());
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            //Console.WriteLine(e.Exception.ToString());
        }

        private Modus previousMode;
        private void EventManager_OnPanic(bool obj)
        {
            if (obj)
            {
                previousMode = StateMachine.ClientModus;
                StateMachine.ClientModus = Modus.leaguemode;
                return;
            }

            StateMachine.ClientModus = previousMode;


        }

        private void Start()
        {
            Memory = new MemoryLoader("csgo");
            Memory.OnProcessLoaded += Memory_OnProcessLoaded;
            Memory.OnProcessExited += Memory_OnProcessExited;
            Memory.Query();

        }

        private void Memory_OnProcessExited()
        {
            Environment.Exit(0);
        }

        private ParamManager paramManager;


        private void Memory_OnProcessLoaded()
        {
            
            ConfigFactory.TryLoadConfig();
            //instantiate netvarmanager once.
            new NetVarManager();
            if (g_Globals.Offset.dwViewMatrix == 0
                || g_Globals.Offset.dwEntityList == 0
                || g_Globals.Offset.dwGameRulesProxy == 0
                || g_Globals.Offset.dwGlowObjectManager == 0
                || g_Globals.Offset.dwRadarBase == 0
                || g_Globals.Offset.dwForceJump == 0
                || g_Globals.Offset.dwForceAttack == 0)
                ConsoleHelper.ConfirmAction("Couldnt catch all Unicorns!\n Starting anyway...");
            

            Engine = new Engine(Memory.Modules["engine.dll"], (uint)g_Globals.Offset.dwClientState);
            Client = new Client(Memory.Modules["client.dll"], (uint)g_Globals.Offset.dwEntityList, Engine);
            ConvarManager = new ConvarManager();
            m_bPprocessActive = true;
            paramManager = new ParamManager();
            paramManager.ParseParams();
            Create();
            m_tEntityUpdateThread = new Thread(EntityUpdate);
            m_tEntityUpdateThread.Name = "#" + Generators.GetRandomString(10);
            m_tEntityUpdateThread.SetApartmentState(ApartmentState.STA);
            m_tEntityUpdateThread.Start();
            paramManager.Hook();
        }



        private void Create()
        {
            m_aSkillMods.Add(new SkillModSoundEffects(Engine, Client));
            m_aSkillMods.Add(new SkillModVisible(Engine, Client));
            m_aSkillMods.Add(new SkillModAim(Engine, Client));
            m_aSkillMods.Add(new SkillModNeon(Engine, Client));
            m_aSkillMods.Add(new SkillModHBTrigger(Engine, Client));
            m_aSkillMods.Add(new SkillModDrawing(Engine, Client));
            m_aSkillMods.Add(new SkillModBunny(Engine, Client));
            m_aSkillMods.Add(new SkillModConvar(Engine, Client));
            m_aSkillMods.Add(new SkillModCham(Engine, Client));
            m_aSkillMods.Add(new SkillModSkin(Engine, Client));
            m_aSkillMods.Add(new SkillModEnvironmentControl(Engine, Client));
            m_aSkillMods.Add(new SkillModSpriteController(Engine, Client));
        }

        private void EntityUpdate()
        {


            while (!Memory.Reader.IsDisposed)
            {
                if (Memory.Reader.IsDisposed)
                    break;

                try
                {
                    foreach (var item in m_aSkillMods)
                    {
                        item.Start();
                    }
                    var _updateResult = Client.Update();
                    switch (_updateResult)
                    {
                        case UpdateResult.OK:
                            RunModules();
                            break;
                        case UpdateResult.NewState:
                        //Thread.Sleep(100);
                        //break;
                        case UpdateResult.LevelChanged:
                        case UpdateResult.Pending:
                        case UpdateResult.None:
                        default:
                            Thread.Sleep(1);
                            break;
                    }
                    foreach (var item in m_aSkillMods)
                    {
                        item.End();
                    }

                    m_dtPreviousDeltaUpdate = DateTime.Now;
                    CalculateFramesPerSecond();

                }
                catch (Exception e)
                {
                    //throw e;
                    //Program.Log(e.ToString());
                    Client.Dirty();
                }

            }
            //Console.WriteLine("Process Exited?");
            Environment.Exit(0);
        }

        private void RunModules()
        {
            foreach (var item in m_aSkillMods)
                item.Before();

            foreach (var item in m_aSkillMods)
                item.Update();

            foreach (var item in m_aSkillMods)
                item.AfterUpdate();
        }

        private void CalculateFramesPerSecond()
        {
            m_lRenderedFrames++;
            if ((DateTime.Now - m_dtPOreviousFrameUpdate).TotalSeconds >= 1)
            {
                m_lCurrentFPS = m_lRenderedFrames;
                m_lRenderedFrames = 0;
                m_dtPOreviousFrameUpdate = DateTime.Now;
            }
        }



        private Dictionary<ClientClass_t, DataTable> Tables = new Dictionary<ClientClass_t, DataTable>();


        //if we have a proxy table we can reiterate the props by getting the others.

        private RecvProp[] ReadTable(RecvTable _table)
        {
            var _tblname = Memory.Reader.ReadString(_table.m_pNetTableName, Encoding.UTF8);
            //_table.
            List<RecvProp> _props = new List<RecvProp>();
            //RecvProp[] _props = new RecvProp[_table.m_nProps];
            for (int i = 0; i < _table.m_nProps; i++)
            {
                var _prop = Memory.Reader.Read<RecvProp>(new IntPtr((int)_table.m_pProps + (i * 0x3C)));
                var _name = Memory.Reader.ReadString(new IntPtr(_prop.m_pVarName), Encoding.UTF8);

                Console.WriteLine(_name);

                if (_prop.m_Offset == 0)
                    continue;
                if (!g_Globals.NetVars.ContainsKey(_tblname + "::" + _name))
                {
                    g_Globals.NetVars.Add(_tblname + "::" + _name, _prop.m_Offset);
                }
                _props.Add(_prop);
                //_props[i] 
                if (_prop.m_RecvType == ePropType.DataTable)
                    _props.AddRange(ReadTable(Memory.Reader.Read<RecvTable>((IntPtr)_prop.m_pDataTable)));

            }
            return _props.ToArray();
        }
        //private Dictionary<string, Dictionary<string, int>> _omg = new Dictionary<string, Dictionary<string, int>>();
    }

    public class DataTable
    {
        public ClientClass_t ClientClass;
        public RecvTable Table;
        public List<RecvProp> Props;
    }
}
