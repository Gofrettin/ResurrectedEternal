using RedRain;
using ResurrectedEternal.Events;
using ResurrectedEternal.Params.CSHelper;
using RRWAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ResurrectedEternal.Events.EventManager;

namespace ResurrectedEternal.Memory
{
    public class MemoryLoader
    {
        public static MemoryLoader instance;

        public Dictionary<string, IntPtr> Modules = new Dictionary<string, IntPtr>();
        public Dictionary<string, ProcessModule> ProcessModules = new Dictionary<string, ProcessModule>();
        public Process pProcess { get; private set; }
        public int ApplicationPid { get; private set; }

        private IntPtr WindowHandle => ProcessWindowHandle;

        public event Action OnProcessLoaded;

        public event Action OnProcessExited;

        private string _processName { get; set; }
        private bool _inFocus { get; set; }

        public NativeMemory Reader;

        private Timer _windowWatch;

        private IntPtr ProcessHandle;
        private IntPtr ProcessWindowHandle;

        public MemoryLoader(string _procName)
        {
            instance = this;
            _processName = _procName;
            ApplicationPid = GetMainWindowHandle();
            //Process.EnterDebugMode();
        }

        public void Query()
        {
            Load();
        }

        async void Load()
        {
            ConsoleHelper.ShowAction("Waiting for CS:GO...",33);
            await WaitForProcess();
            await Task.Delay(1333);
            await WaitForModule("engine.dll");
            await WaitForModule("client.dll");
            await WaitForModule("vstdlib.dll");
            GetModules();
            
            
            _windowWatch = new Timer(CheckWindow, null, 1000, 1000);
            ProcessHandle = pProcess.Handle;
            ProcessWindowHandle = pProcess.MainWindowHandle;
            m_dwpszProcessDirectory = System.IO.Path.GetDirectoryName(pProcess.MainModule.FileName) + @"\" + _processName + @"\";
            AppDomain.CurrentDomain.SetData("_procDir", m_dwpszProcessDirectory);
            Attach();
            //pProcess.Close();
            //pProcess.Dispose();
            ConsoleHelper.ConfirmAction("OK!");
            OnProcessLoaded?.Invoke();

        }


        private void Attach()
        {
            if (Reader != null)
                Reader.Dispose();
            Reader = new ExternalProcessMemory(pProcess);
            ExternalProcessMemory.OnProcessInvalid += ExternalProcessMemory_OnProcessInvalid;
        }

        private void ExternalProcessMemory_OnProcessInvalid()
        {
            OnProcessExited?.Invoke();
        }

        private void GetModules()
        {
            foreach (var item in pProcess.Modules.OfType<ProcessModule>())
            {
                Modules.Add(item.ModuleName, item.BaseAddress);
                ProcessModules.Add(item.ModuleName, item);
            }

        }

        /// <summary>
        /// Locks the thread and waits for the process to become active.
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="pPid"></param>
        /// <returns></returns>
        private async Task<bool> WaitForProcess()
        {
            var proc = Process.GetProcesses();
            while (true)
            {
                var procList = Process.GetProcesses();

                if (!procList.Where(x => x.ProcessName == _processName).Any())
                {
                    await Task.Delay(100);
                    continue;
                }
               
                pProcess = procList.Where(x => x.ProcessName == _processName).FirstOrDefault();
                //pProcess.Exited += _process_Exited;
                break;

                
            }

            return false;
        }
        /// <summary>
        /// Locks the thread and waits for the modulename to be available.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        private async Task<bool> WaitForModule(string moduleName)
        {
            while (!pProcess.Modules.OfType<ProcessModule>().Any(x => x.ModuleName == moduleName))
            {
                pProcess.Refresh();
                await Task.Delay(333);
            }
            return true;
        }

        /// <summary>
        /// Invoke process exit event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _process_Exited(object sender, EventArgs e)
        {
            //ProcessExited?.Invoke();
            OnProcessExited?.Invoke();
        }

        private string m_dwPath;
        public string m_dwpszProcessDirectory
        {
            get { return m_dwPath;  }
            set { m_dwPath = value; }
        }

        //public string GetProcessDirectory()
        //{
        //    return System.IO.Path.GetDirectoryName(pProcess.MainModule.FileName) + @"\" + _processName + @"\";
        //}

        /// <summary>
        /// Returns the unique id of the applications process.
        /// </summary>
        /// <returns></returns>
        public int GetMainWindowHandle()
        {
            return Process.GetCurrentProcess().Id;
        }

        public IntPtr GetProcessWindowHandle()
        {
            return ProcessWindowHandle;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();
        private void CheckWindow(object state)
        {
            SyncWindowState();
            var _activeWindow = WAPI.GetForegroundWindow();

            if(_activeWindow != WindowHandle && CurrentWindowState == WindowState.Foreground)
            {
                CurrentWindowState = WindowState.Background;
                EventManager.Notify(CurrentWindowState);
            }
            else if(_activeWindow == WindowHandle && CurrentWindowState == WindowState.Background)
            {
                CurrentWindowState = WindowState.Foreground;
                EventManager.Notify(CurrentWindowState);
            }
            
        }

        private WindowState CurrentWindowState = WindowState.Background;

        bool _onFirstRun = true;
        private void SyncWindowState()
        {
            if (!_onFirstRun)
                return;

            var _active = WAPI.GetForegroundWindow();
            if (_active == WindowHandle)
                CurrentWindowState = WindowState.Foreground;
            else
                CurrentWindowState = WindowState.Background;

            _onFirstRun = false;
        }

        public System.Drawing.Rectangle GetWindowRect()
        {
            RRWAPI.RECT _RECT = new RRWAPI.RECT();
            RRWAPI.WAPI.GetWindowRect(ProcessWindowHandle, out _RECT);

            //System.Drawing.Rectangle _windowRect = new System.Drawing.Rectangle();
            //_windowRect.Width = _RECT.Right - _RECT.Left;
            //_windowRect.Height = _RECT.Bottom - _RECT.Top;
            return _RECT;
        }
        [DllImport("user32.dll")]
        public static extern long GetWindowRect(int hWnd, ref System.Drawing.Rectangle lpRect);

    }
}
