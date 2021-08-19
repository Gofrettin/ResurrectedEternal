using ResurrectedEternal.Configs;
using ResurrectedEternal.Menu.Controls;
using ResurrectedEternal.Menu.Controls.Button;
using ResurrectedEternal.Skills.Factory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu
{
    public class MainMenu
    {

        public static int MenuWidth = 640;
        public static int MenuHeight;

        public static float MenuPosXAbsolute = 70f;
        public static float MenuPosYAbsolute = 70f;

        public MenuBase MenuBase;
        public MenuButton[] Buttons;
        //public Navigation Navigation;
        public Vector3[] MenuDivider;
        private Container[] Containers = new Container[0];

        public static float MagicFloat;

        public int HeaderFontSize;
        public int NavigationFontSize;
        public int ContainerFontSize;
        private RectangleF Screen;
        public MainMenu(System.Drawing.Rectangle _rect, int headerSize = 23, int navigationFontSize = 16, int containerFontSize = 18)
        {
            MagicFloat = (float)_rect.Width / 1920.0f;
            HeaderFontSize = Convert.ToInt32(headerSize * MagicFloat);
            NavigationFontSize = Convert.ToInt32(navigationFontSize * MagicFloat);
            ContainerFontSize = Convert.ToInt32(containerFontSize * MagicFloat);
            Screen = new RectangleF(_rect.X, _rect.Y, _rect.Width, _rect.Height);

            MenuPosXAbsolute = Screen.Width / 16;
            MenuPosYAbsolute = Screen.Height / 16;
            MenuHeight = Convert.ToInt32(Screen.Height - (Screen.Height / 8));
            MenuWidth = Convert.ToInt32(MenuWidth * MagicFloat);
            //Navigation = new Navigation(Screen);
            MenuBase = new MenuBase(Screen, NavigationFontSize, HeaderFontSize);
            CreateButtons();
            RenderFromConfig();
            MenuBase.NavigationEntries[0].OnEnter();
            MenuBase.NavigationEntries[0].OnClick();
        }

        private void CreateButtons()
        {
            //float num = (MainMenu.MenuWidth - MenuBase.NavRect.Width) / 3;
            //num *= MagicFloat;
            Buttons = new MenuButton[1];
            Buttons[0] = new MenuButton(ConfigFactory.SaveConfig, MenuBase.NavRect, Convert.ToInt32(20 * MainMenu.MagicFloat), "Save Config", 10);
            //Buttons[1] = new MenuButton(ConfigFactory.SaveEnvironmentConfig, MenuBase.NavRect, 20, "Save Environment", 30 + Buttons[0].TextRect.Width);
            //Buttons[2] = new MenuButton(OnReloadConfig, MenuBase.NavRect, 20, "Load Config", 30 + Buttons[0].Rect.Width + Buttons[1].Rect.Width);

        }

        private void RenderFromConfig()
        {
            Type type = g_Globals.Config.GetType();
            var ConfigFields = type.GetFields();
            foreach (var item in ConfigFields)
            {
                Type _valtype = item.FieldType;
                var _newitem = item.GetValue(g_Globals.Config);
                if (_newitem == null || _newitem.GetType() == typeof(string)) continue;
                //MenuEntry _menEntry = new MenuEntry();
                //_menEntry.Index = _count;
                //_menEntry.Name = _valtype.Name.ToUpper();

                var _cfgvalEntryProps = _newitem.GetType().GetFields();

                if (_cfgvalEntryProps.Length == 0) continue;
                List<ConfigValueEntry> _vals = new List<ConfigValueEntry>();
                foreach (var _cfgvalEntry in _cfgvalEntryProps)
                {
                    if (_cfgvalEntry.FieldType == typeof(string)) continue;

                    var cfgItem = _cfgvalEntry.GetValue(_newitem) as ConfigValueEntry;

                    if (cfgItem == null || cfgItem.HiddenFromMenu) continue;
                    _vals.Add(cfgItem);
                    //_menEntry.Entries.Add(cfgItem);
                }
                MenuBase.AddNavEntry(_valtype.Name.Replace("Config", "").ToUpper(), AddContainer(_vals.ToArray()));
                //if (_menEntry.Entries.Count == 0) continue;
                //_count++;
                //_MenuEntries.Add(_menEntry);

            }
        }

        //private void OnReloadConfig()
        //{
        //    ConfigFactory.LoadEnvironmentConfig();
        //    Containers = new Container[0];
        //    RenderFromConfig();
        //}

        public Container AddContainer(ConfigValueEntry[] containerEntries)
        {
            Array.Resize(ref Containers, Containers.Length + 1);
            Containers[Containers.Length - 1] = new Container(containerEntries, Containers.Length - 1, MenuBase.NavRect, MenuBase.HeaderRect.Height, ContainerFontSize);
            return Containers[Containers.Length - 1];
        }
    }

    class MenuRenderer
    {
        public MainMenu MainMenu;
        public Point MousePos;

        private RenderObject _referencedRenderObject;

        private KeyHandle KeyHandles = new KeyHandle(null);

        private int m_nMouseOffsetX = 0;
        private int m_nMouseOffsetY = 0;


        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        public MenuRenderer(System.Drawing.Rectangle _rect, RenderObject _ro)
        {
            _referencedRenderObject = _ro;
            _referencedRenderObject.MouseDown += _referencedRenderObject_MouseDown;
            _referencedRenderObject.MouseUp += _referencedRenderObject_MouseUp;
            //_referencedRenderObject.MouseClick += _referencedRenderObject_MouseClick;
            _referencedRenderObject.MouseWheel += _referencedRenderObject_MouseWheel;
            m_nMouseOffsetX = _ro.Location.X;
            m_nMouseOffsetY = _ro.Location.Y;
            MainMenu = new MainMenu(_rect);
            new Thread(() => MouseHookThread()).Start();
        }

        private System.Windows.Forms.MouseButtons _mbutton;

        private void _referencedRenderObject_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Console.WriteLine("a");
            
            KeyHandles.Reset();
        }

        private void _referencedRenderObject_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_CaptureInput)
                return;
            CheckNavEntries(MousePos.X, MousePos.Y);
            CheckButtons(MousePos.X, MousePos.Y, e.Button);
            _mbutton = e.Button;
            KeyHandles.Down = true;
        }

        private void _referencedRenderObject_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {



        }

        private void CheckButtons(int x, int y, System.Windows.Forms.MouseButtons _mb)
        {
            MenuButton _button = null;
            foreach (var item in MainMenu.Buttons)
            {
                if (item.Rect.Contains(new Point(x, y)))
                {
                    _button = item;
                    break;
                }

            }
            if (_button != null)
            {
                _button.OnMouseDown();
                return;
            }
                

            var activeBit = MainMenu.MenuBase.GetActiveBit();
            if (activeBit == null)
                return;


            var _activeContainer = activeBit.GetActiveContainer();
            if (_activeContainer == null)
                return;

            if (_mb == System.Windows.Forms.MouseButtons.Left)
                _activeContainer.Increment();
            else if (_mb == System.Windows.Forms.MouseButtons.Right)
                _activeContainer.Decrement();
        }

        private void CheckNavEntries(int X, int Y)
        {
            NavigationEntry _navselection = null;
            foreach (var item in MainMenu.MenuBase.NavigationEntries)
            {
                if (item.Rect.Contains(new Point(X, Y)))
                {
                    _navselection = item;
                    break;
                }


                //Console.WriteLine(e.Button + "on" + item.Title);
            }
            if (_navselection == null)
                return;
            foreach (var item in MainMenu.MenuBase.NavigationEntries)
            {
                if (item != _navselection)
                    item.OnDeselect();
            }
            _navselection.OnClick();

        }

        private void _referencedRenderObject_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_CaptureInput)
                return;

            var activeBit = MainMenu.MenuBase.GetActiveBit();
            if (activeBit == null)
                return;


            var _activeContainer = activeBit.GetActiveContainer();
            if (_activeContainer == null)
                return;

            if (e.Delta > 0)
            {
                _activeContainer.Increment();
            }
            else
            {
                _activeContainer.Decrement();
            }
        }

        void MouseHookThread()
        {
            while (true)
            {
                try
                {
                    if (_CaptureInput)
                    {
                        GetCursorPos(ref MousePos);
                        MousePos.X -= m_nMouseOffsetX;
                        MousePos.Y -= m_nMouseOffsetY;
                        var active = MainMenu.MenuBase.GetActiveBit();
                        foreach (var item in MainMenu.MenuBase.NavigationEntries)
                        {
                            if (item == null)
                                continue;
                            if (item == active)
                                continue;

                            if (item.Rect.Contains(new Point(MousePos.X, MousePos.Y)))
                                item.OnEnter();
                            else
                                item.OnExit();
                        }

                        foreach (var item in MainMenu.Buttons)
                        {
                            if (item == null)
                                continue;
                            if (item.Rect.Contains(new Point(MousePos.X, MousePos.Y)))
                                item.OnMouseEnter();
                            else
                                item.OnMouseExit();
                        }

                        var container = MainMenu.MenuBase.GetActiveBit();
                        if (container != null)
                        {
                            //StringContainer _activeBit = null;
                            foreach (var _cont in container.ActiveContainer.StringContainers)
                            {
                                if (_cont.Rect.Contains(new Point(MousePos.X, MousePos.Y)))
                                {
                                    _cont.OnEnter();
                                }
                                else
                                {
                                    _cont.OnExit();
                                }

                            }
                        }
                        if (KeyHandles.Down)
                        {
                            if (KeyHandles.Elapsed)
                            {

                                var activeBit = MainMenu.MenuBase.GetActiveBit();
                                if (activeBit == null)
                                    continue;


                                var _activeContainer = activeBit.GetActiveContainer();
                                if (_activeContainer == null)
                                    continue;

                                if (_mbutton == System.Windows.Forms.MouseButtons.Left)
                                {
                                    _activeContainer.Increment();
                                }
                                else if (_mbutton == System.Windows.Forms.MouseButtons.Right)
                                {
                                    _activeContainer.Decrement();
                                }
                                //CheckNavEntries(MousePos.X, MousePos.Y);
                                //CheckButtons(MousePos.X, MousePos.Y, e.Button);
                            }
                        }
                    }
                } catch (Exception e)
                {
                    throw e;
                }


                Thread.Sleep(16);
            }
            Console.WriteLine("MouseHook thread exited");
        }

        private bool _CaptureInput = false;

        internal void DisableInput()
        {
            _CaptureInput = false;
        }

        internal void CaptureInput()
        {
            _CaptureInput = true;
        }
    }
}
