using ResurrectedEternal.Events;
using RRWAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResurrectedEternal.Menu
{
    class KeyHandle
    {
        public KeyHandle(Action func)
        {
            Handle = func;
        }

        public bool Down;
        public bool Press;
        public Action Handle;

        public bool Elapsed => GetElapsed();
        private float _cur = 0;
        private float _max = .2f;
        private bool GetElapsed()
        {
            _cur += Clockwork.Clock.DeltaTime;
            if (_cur >= _max)
            {
                _cur = _max;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Reset()
        {
            Press = false;
            Down = false;
            _cur = 0;
        }
    }
    class MenuEntry
    {
        public string Name;
        public int Index;
        public List<ConfigValueEntry> Entries = new List<ConfigValueEntry>();
    }

    class Menu
    {
        public static Menu instance;

        public MenuConfigCaster MenuConfigCaster;

        public int CurrentIndex = 0;

        public int CurrentSubIndex = 0;

        public bool HasSelected = false;

        public int MinIndex = 0;
        public int MaxIndex => _MenuEntries.Count - 1;

        public List<MenuEntry> _MenuEntries = new List<MenuEntry>();

        public SharpDX.Color HoverColor = SharpDX.Color.Crimson;
        public SharpDX.Color SelectedColor = SharpDX.Color.Red;
        public SharpDX.Color NormalColor = SharpDX.Color.Yellow;


        private Config Config;

        public bool _menuActive = false;

        private void Construct()
        {
            Type type = Config.GetType();
            var _fieldInfo = type.GetFields();
            int _count = 0;
            foreach (var item in _fieldInfo)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(Config);
                if (_newitem == null || _newitem.GetType() == typeof(string)) continue;
                MenuEntry _menEntry = new MenuEntry();
                _menEntry.Index = _count;
                _menEntry.Name = _valtype.Name.ToUpper();

                var _cfgvalEntryProps = _newitem.GetType().GetFields();

                if (_cfgvalEntryProps.Length == 0) continue;

                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;

                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                    if (cfgItem == null || cfgItem.HiddenFromMenu) continue;
                    _menEntry.Entries.Add(cfgItem);
                }
                if (_menEntry.Entries.Count == 0) continue;
                _count++;
                _MenuEntries.Add(_menEntry);

            }
            //MenuEntries = _newDict;
        }

        bool _isPanic = false;
        private void Panic()
        {
            _isPanic = !_isPanic;

            EventManager.Notify(_isPanic);
        }

        public Menu(Config _cfg)
        {
            MenuConfigCaster = new MenuConfigCaster();
            instance = this;
            Config = _cfg;
            Construct();

            MyKeys.Add(VirtualKeys.Insert, new KeyHandle(ShowHide));
            MyKeys.Add(VirtualKeys.Home, new KeyHandle(Panic));
            //MyKeys.Add(VirtualKeys.Up, new KeyHandle(MoveDown));
            //MyKeys.Add(VirtualKeys.Down, new KeyHandle(MoveUp));
            //MyKeys.Add(VirtualKeys.Left, new KeyHandle(Decrement));
            //MyKeys.Add(VirtualKeys.Right, new KeyHandle(Increment));
            //MyKeys.Add(VirtualKeys.Back, new KeyHandle(Return)); 
            Task.Factory.StartNew(() => HandleInput());
            //m_GlobalHook = Hook.AppEvents();
            //m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            //m_GlobalHook.KeyPress += M_GlobalHook_KeyPress;
        }
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2,
            Press = 3
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);



        private Dictionary<VirtualKeys, KeyHandle> MyKeys = new Dictionary<VirtualKeys, KeyHandle>();

        private void HandleInput()
        {


            while (true)
            {
                try
                {
                    foreach (var item in MyKeys)
                    {
                        var _res = KeyboardInfo.GetKeyState((Keys)item.Key);


                        if (_res.IsPressed && !item.Value.Down)
                        {
                            item.Value.Down = true;
                            item.Value.Press = false;
                            item.Value.Handle();
                        }
                        else if (_res.IsPressed && item.Value.Down && item.Value.Elapsed)
                        {
                            item.Value.Press = true;
                            item.Value.Handle();
                        }
                        else
                        {
                            if (!_res.IsPressed && item.Value.Down)
                            {
                                item.Value.Reset();
                            }

                        }
                        //if (_res.IsPressed)
                        //    Console.WriteLine("Down {0} Press {1}", item.Value.Down, item.Value.Press);
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }



                Thread.Sleep(16);
            }
            Console.WriteLine("MouseHook thread exited");
        }

        void ShowHide()
        {
            _menuActive = !_menuActive;

            if (_menuActive)
                EventManager.Notify(EventManager.MenuState.Open);
            else
                EventManager.Notify(EventManager.MenuState.Close);

            //OnEnableMenu?.Invoke(_menuActive);
            //Console.WriteLine("Menu " + _menuActive);
        }


    }
}
