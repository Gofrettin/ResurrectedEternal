using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects.ThreadUpdate
{
    public struct EntUpdate
    {
        public IntPtr m_dwEntity;
        public ClientClass Class;
    }
    class ThreadedUpdate
    {
        private ConcurrentQueue<BaseEntity> FinishedObjects = new ConcurrentQueue<BaseEntity>();
        private ConcurrentQueue<IntPtr> PendingObjects = new ConcurrentQueue<IntPtr>();

        private Dictionary<IntPtr, ClientClass> StaticObjects = new Dictionary<IntPtr, ClientClass>();

        private Thread UpdateThread;

        public bool Get(out BaseEntity next)
        {
            if (!FinishedObjects.TryDequeue(out next))
                return false;
            return true;
        }

        public void Add(IntPtr _entityAddress)
        {
            if (_cancel)
                return;
            PendingObjects.Enqueue(_entityAddress);
        }

        private bool _cancel = false;
        private void RunThread()
        {
            ClearAll();
            _cancel = false;
            while (!_cancel)
            {
                if (_cancel) continue;
                if (PendingObjects.IsEmpty)
                {
                    Thread.Sleep(1);
                    continue;
                }
                if (!PendingObjects.TryDequeue(out IntPtr result))
                    continue;
                if (result == IntPtr.Zero)
                    continue;

                if (StaticObjects.ContainsKey(result)) continue;

                BaseEntity _be = new BaseEntity(result, ClientClass.CAI_BaseNPC);

                if (!_be.IsValid)
                    continue;

                if (ReadClassId(result, out ClientClass cclass))
                {
                    //if (IsClassStatic(cclass))
                    //    StaticObjects.Add(result, cclass);
                    FinishedObjects.Enqueue(Generators.CreateFromClassId(result, cclass));
                }

            }
           
            
        }

        private void ClearAll()
        {
            new PushClipEventArgs("Clearing Cache", 3f, SharpDX.Color.Crimson);
            //Console.WriteLine("Clearing Cache");
            while (PendingObjects.TryDequeue(out _))
            {

            }
            StaticObjects.Clear();
        }

        //Cache static objects because the address wont change anyway.
        private bool ReadClassId(IntPtr _EntityAddress, out ClientClass _result)
        {
            _result = ClientClass.CAI_BaseNPC;
            try
            {
                if (_EntityAddress == IntPtr.Zero)
                    return false;
                var _f = MemoryLoader.instance.Reader.Read<IntPtr>(_EntityAddress + 0x8);
                if ((int)_f < 1000)
                    return false;
                if (_f == IntPtr.Zero)
                    return false;
                var _s = MemoryLoader.instance.Reader.Read<IntPtr>(_f + 0x8);
                if (_s == IntPtr.Zero)
                    return false;
                var _t = MemoryLoader.instance.Reader.Read<IntPtr>(_s + 0x1);
                if (_t == IntPtr.Zero)
                    return false;
                _result = (ClientClass)MemoryLoader.instance.Reader.Read<int>(_t + 0x14);
                return true;
            }
            catch { return false; }
        }

        private Engine Engine;
        public ThreadedUpdate(Engine engine)
        {
            Engine = engine;
            //Events.EventManager.OnMapChanged += EventManager_OnMapChanged;
            Events.EventManager.OnEngineStateChanged += EventManager_OnEngineStateChanged;
            UpdateThread = new Thread(RunThread);
            //Task.Factory.StartNew(() => EngineAgent());
        }

        private void EventManager_OnEngineStateChanged(SignonState obj)
        {
            //throw new NotImplementedException();
            switch (obj)
            {
                case SignonState.Full:
                    switch (UpdateThread.ThreadState)
                    {
                        case ThreadState.Running:
                        case ThreadState.StopRequested:
                        case ThreadState.SuspendRequested:
                        case ThreadState.Background:
                        case ThreadState.WaitSleepJoin:
                        case ThreadState.AbortRequested:
                            break;
                        case ThreadState.Unstarted:
                        case ThreadState.Stopped:
                        case ThreadState.Suspended:
                        case ThreadState.Aborted:
                        default:
                            UpdateThread = new Thread(RunThread);
                            UpdateThread.Name = Generators.GetRandomString(10);
                            UpdateThread.Priority = ThreadPriority.Normal;
                            UpdateThread.Start();
                            break;

                    }
                    break;
                case SignonState.None:
                case SignonState.Challenge:
                case SignonState.Connected:
                case SignonState.New:
                case SignonState.PreSpawn:
                case SignonState.Spawn:
                case SignonState.ChangingLevel:
                default:
                    _cancel = true;
                    //ClearAll();
                    
                    break;
            }
        }

        private bool IsClassStatic(ClientClass _class)
        {
            switch (_class)
            {
                case ClientClass.CSprite:
                    return true;
                default:
                    return false;
            }
        }
    }
}
