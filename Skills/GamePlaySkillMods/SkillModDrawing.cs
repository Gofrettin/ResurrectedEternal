using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Draw;
using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Menu;
using ResurrectedEternal.Objects;
using ResurrectedEternal.Skills.Drawpackage;
using ResurrectedEternal.Skills.Factory;
using ResurrectedEternal.Skills.PushClips;
using RRFull;
using SharpDX;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ResurrectedEternal.Events.EventManager;

namespace ResurrectedEternal.Skills
{
    public enum DrawOffset
    {
        OnPoint,
        Center,
        Left,
        Right,
        Top
    }
    class SkillModDrawing : SkillMod
    {




        private Drawing Drawing => _renderObject.Drawing;

        private RenderObject _renderObject;
        private PushClipManager PushClipManager;
        private Menu.Menu _Menu;
        private MenuRenderer MenuRenderer;

        private bool MenuAvailable = false;

        private bool _hasDrawn = false;

        private bool _isForeGround = true;
        private MenuState CurrentMenuState = MenuState.Close;
        string name = "";

        private string PlayerInfo = "";

        Vector2 _oneQuart;
        private List<Tuple<string, Vector2, Color, DrawOffset>> BackgroundStrings = new List<Tuple<string, Vector2, Color, DrawOffset>>();
        private List<Tuple<string, Vector2, Color, DrawOffset>> ForegroundString = new List<Tuple<string, Vector2, Color, DrawOffset>>();

        private List<Tuple<string, Vector2, SharpDX.Color>> _menuEntrieRenderer = new List<Tuple<string, Vector2, Color>>();
        Vector2 xOffset = Vector2.Zero;
        private float CurrentLonges = 0f;
        private Vector2 PushClipPosition = Vector2.Zero;
        private Vector2 PushClipOffset = new Vector2(0f, 22f);
        private int MaximumClipLength = 250;

        float _maxAngle = 180.0f;
        private Vector2 _screenCenter;

        private bool m_bDrawFov => (bool)g_Globals.Config.HudConfig.bDrawFov.Value;

        //›‹ ﴾﴿
        string selector = "› ";
        string subSelector = " ‹";

        public SkillModDrawing(Engine engine, Client client) : base(engine, client)
        {
            _renderObject = OverlayFactory.CreateRenderForm(Memory.MemoryLoader.instance.GetWindowRect());
            _renderObject.OnLoad += _renderObject_OnLoad;
            _renderObject.Run();

            _Menu = new Menu.Menu(Config);
            PushClipManager = new PushClipManager();

            OnPlayerChanged += EventManager_OnPlayerChanged;
            MenuStateChanged += EventManager_MenuStateChanged;
            WindowStateChanged += EventManager_WindowStateChanged;
        }

        private void EventManager_WindowStateChanged(WindowState obj)
        {
            switch (obj)
            {
                case WindowState.Background:
                    //state is now background
                    if (CurrentMenuState == MenuState.Close)
                        _isForeGround = false;
                    break;
                case WindowState.Foreground:
                default:
                    _isForeGround = true;
                    break;
            }
        }

        private void EventManager_MenuStateChanged(MenuState obj)
        {

            CurrentMenuState = obj;
            switch (obj)
            {
                case MenuState.Open:
                    MenuRenderer.CaptureInput();
                    _renderObject.DisableClickThrough();
                    break;
                case MenuState.Close:
                    _renderObject.EnableClickThrough();
                    MenuRenderer.DisableInput();
                    break;
                default:
                    break;
            }
        }

        private void EventManager_OnPlayerChanged(BasePlayerChangedEventArgs obj)
        {
            //korrekt
            switch (obj.NewState)
            {
                case BasePlayerStateChange.Connected:
                    Client_OnPushClip(string.Format("{0} {1} has connected!", obj.Id, obj.Name), 5f, Color.LimeGreen);
                    break;
                case BasePlayerStateChange.Disconnected:
                default:
                    Client_OnPushClip(string.Format("{0} {1} just left us...", obj.Id, obj.Name), 5f, Color.Red);
                    break;
            }

        }

        private void _renderObject_OnLoad()
        {
            MenuRenderer = new Menu.MenuRenderer(Memory.MemoryLoader.instance.GetWindowRect(), _renderObject);
            MenuAvailable = true;
        }

        private void Client_OnPushClip(string message, float duration, Color clr)
        {
            PushClipManager.AddClip(message, duration, clr);
        }

        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammodefull
                || ClientModus == Events.Modus.novisuals)
                return false;
            return true;
        }


        void GhettoStart()
        {
            if (Drawing == null)
                return;
            Drawing.BeginDraw();
        }

        void GhettoEnd()
        {
            if (Drawing == null)
                return;
            Drawing.EndDraw();

        }

        public override void Start()
        {

            if (Drawing == null)
                return;

            GhettoStart();

            if (!CanProcess())
                return;
            _hasDrawn = true;
            BackgroundStrings.Clear();
            ForegroundString.Clear();
            _screenCenter = new Vector2(Drawing.Size.Width / 2, Drawing.Size.Height / 2) + EngineMath.ScreenOffset;
        }

        public override void End()
        {
            if (Drawing != null && _hasDrawn && CanProcess() && _isForeGround)
            {
                RenderPushClips();
                RenderBackground();

                RenderForeground();

                DrawCrosshair();
                DrawInfo();
            }
            GhettoEnd();


        }



        private void DrawWatermark()
        {
            if (!(bool)Config.VisualConfig.WaterMark.Value)
                return;
            _oneQuart = new Vector2(Drawing.Size.Width / 6, 5);
            if (Client.LocalPlayer != null)
                name = Client.LocalPlayer.BaseAddress.ToString("X");
            else
                name = "FFFFFFFF";
            PlayerInfo = "Hello0o0 [" + g_Globals.Nickname + "]~ hell broke loose on 0x" + name + "\nInsert brings up the menu =) averaging on " + Henker.Singleton.m_lCurrentFPS + "fps";


            Drawing.DrawText(PlayerInfo, SharpDX.Color.White, _oneQuart, "Calibri", 15);
        }

        private void DrawInfo()
        {
            DrawWatermark();

            if (CurrentMenuState == MenuState.Close)
                return;

            DrawMenuDebug();
            if (Engine.IsInGame)
                System.Threading.Thread.Sleep(1);
        }


        private void RenderBackground()
        {
            RenderBack();
        }

        private  void RenderForeground()
        {
            RenderFore();
        }

        private void RenderBack()
        {
            foreach (var item in BackgroundStrings)
            {
                var pos = item.Item4;
                switch (pos)
                {
                    case DrawOffset.OnPoint:
                        Drawing.DrawText(item.Item1, item.Item3, item.Item2);
                        break;
                    default:
                        var _measure = Drawing.MeasureString(item.Item1, Drawing.FontSize);
                        if (pos == DrawOffset.Center)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Center.X, item.Item2.Y + _measure.Center.Y));
                        else if (pos == DrawOffset.Left)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Width, item.Item2.Y));
                        else if (pos == DrawOffset.Right)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X + _measure.Width, item.Item2.Y));
                        else if (pos == DrawOffset.Top)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Center.X, item.Item2.Y - _measure.Height));
                        break;
                }

            }

        }
        private void RenderFore()
        {
            foreach (var item in ForegroundString)
            {
                var pos = item.Item4;
                switch (pos)
                {
                    case DrawOffset.OnPoint:
                        Drawing.DrawText(item.Item1, item.Item3, item.Item2);
                        break;
                    default:
                        var _measure = Drawing.MeasureString(item.Item1, Drawing.FontSize);
                        if (pos == DrawOffset.Center)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Center.X, item.Item2.Y + _measure.Center.Y));
                        else if (pos == DrawOffset.Left)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Width, item.Item2.Y));
                        else if (pos == DrawOffset.Right)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X + _measure.Width, item.Item2.Y));
                        else if (pos == DrawOffset.Top)
                            Drawing.DrawText(item.Item1, item.Item3, new Vector2(item.Item2.X - _measure.Center.X, item.Item2.Y - _measure.Height));
                        break;
                }
            }

        }




        private void AddBackgroundString(string text, Color clr, Vector2 pos, DrawOffset drawOffset = DrawOffset.OnPoint)
        {
            BackgroundStrings.Add(Tuple.Create(text, pos, clr, drawOffset));
        }
        private void AddForeGround(string text, Color clr, Vector2 pos, DrawOffset drawOffset = DrawOffset.OnPoint)
        {
            if (string.IsNullOrEmpty(text))
                return;
            BackgroundStrings.Add(Tuple.Create(text, pos, clr, drawOffset));
        }

        private void AddRend(string text, Color clr, Vector2 pos)
        {
            CurrentLonges = 0;
            _menuEntrieRenderer.Add(Tuple.Create(text, pos, clr));
            foreach (var item in _menuEntrieRenderer)
            {
                float w = Drawing.MeasureString(item.Item1, Drawing.FontSize).Width;
                if (w > CurrentLonges)
                    CurrentLonges = w;
            }

        }


        private void RenderPushClips()
        {
            //if (PushClipPosition == Vector2.Zero)
            //    PushClipPosition = new Vector2(Drawing.Size.Width - MaximumClipLength, 5f);

            var pClips = PushClipManager.GetClips();
            if (pClips.Length == 0)
                return;

            for (int i = 0; i < pClips.Length; i++)
            {
                string clipText = pClips[i].Message;
                //if (clipText.Length > MaximumClipLength)
                //    clipText = PushClipManager.Cut(clipText, MaximumClipLength);
                var _measurement = Drawing.MeasureString(clipText, Drawing.FontSize);
                //this should render now slowly.
                var _nextPosition = new Vector2(Drawing.Size.Width - _measurement.Width - 2, 5 + (PushClipOffset.Y * i));
                //AddForeGround(clipText, pClips[i].Color, PushClipPosition + (PushClipOffset * i));
                AddForeGround(clipText, pClips[i].Color, _nextPosition);
                //maybe add a special class to fade in/out text elements.
            }

        }

        private void RenderMenuText()
        {
            foreach (var item in _menuEntrieRenderer)
                Drawing.DrawText(item.Item1, item.Item3, item.Item2);
        }

        private void DrawMenuDebug()
        {

            //Drawing.FillRectangle(new RectangleF(0,0, Drawing.Size.Width, Drawing.Size.Height), new Color(0, 0, 0, 222));
            Drawing.FillRectangleWithOutline(MenuRenderer.MainMenu.MenuBase.MenuRect, MenuRenderer.MainMenu.MenuBase.MenuBackgroundColor);

            Drawing.FillRectangle(MenuRenderer.MainMenu.MenuBase.NavRect, MenuRenderer.MainMenu.MenuBase.NavBackgroundColor);
            var _container = MenuRenderer.MainMenu.MenuBase.GetActiveBit();

            if (_container == null)
                return;

            Drawing.FillRectangle(_container.ActiveContainer.ContainerPos, _container.BackgroundColor);
            foreach (var item in MenuRenderer.MainMenu.MenuBase.NavigationEntries)
            {
                Drawing.DrawText(item.Title, item.ForeColor, item.Position, MenuRenderer.MainMenu.MenuBase.NavFontSize);
            }
            foreach (var item in _container.GetAllStringContainers())
            {
                //Drawing.DrawRectangleOutline(item.Rect, .1f, Color.Black);
                switch (item.StringContainerType)
                {
                    case Menu.Controls.StringContainerType.Header:
                        var headingItem = item as Menu.Controls.StringContainers.StringHeaderContainer;
                        Drawing.DrawText(headingItem.ConfigValue.Header, headingItem.ForeColor, headingItem.m_vRenderPosition, headingItem.StringContainerFontSize);
                        break;
                    case Menu.Controls.StringContainerType.Entry:
                    case Menu.Controls.StringContainerType.MultiEntry:
                        var valueItem = item as Menu.Controls.StringContainers.StringEntryContainer;
                        Drawing.DrawText(valueItem.ConfigValue.Name, valueItem.ForeColor, valueItem.m_vRenderPosition, valueItem.StringContainerFontSize);
                        Drawing.DrawText(_Menu.MenuConfigCaster.CorrectValue(valueItem.ConfigValue), valueItem.ValueForeColor, valueItem.m_vValueRenderPosition, valueItem.StringContainerFontSize);
                        break;
                    case Menu.Controls.StringContainerType.None:
                    default:
                        break;
                }


            }
            foreach (var item in MenuRenderer.MainMenu.Buttons)
            {
                Drawing.DrawRoundedRectangle(item.Rect, .3f, 1f, item.RectColor);
                Drawing.DrawText(item.Text, item.TextColor, item.m_vpRenderPosition, item.FontSize);
            }
            Drawing.FillRectangleWithOutline(MenuRenderer.MainMenu.MenuBase.HeaderRect, MenuRenderer.MainMenu.MenuBase.HeaderBackgroundColor);
            Drawing.DrawText(MenuRenderer.MainMenu.MenuBase.MenuHeaderText, Color.Crimson, MenuRenderer.MainMenu.MenuBase.MenuHeaderTextPosition, MenuRenderer.MainMenu.MenuBase.MainMenuFontSize);
            DrawFov();
        }




        private void DrawFov()
        {
            if (!m_bDrawFov)
                return;
            //if (_screenCenter == Vector2.Zero)
            var _fovOffset = new Vector2(Drawing.Size.Width / 2, Drawing.Size.Height / 2);

            float curAngle = (float)Config.AimbotConfig.Angle.Value;
            //float _percent = (curAngle / _maxAngle); // 30% = 0.3;
            float _inverspercent = (curAngle / _maxAngle) - 1f; //30% = 0.7
            float _x = (_fovOffset.X * _inverspercent); //left
            float _x2 = Drawing.Size.Width + (_fovOffset.X * _inverspercent); //right
            float _y2 = Drawing.Size.Height + (_fovOffset.Y * _inverspercent);
            Drawing.DrawLine(new Vector2(_fovOffset.X - 3, Drawing.Size.Height), new Vector2(-_x + 3, _y2), 2f, Color.Black);
            Drawing.DrawLine(new Vector2(_fovOffset.X + 3, Drawing.Size.Height), new Vector2(_x2 - 3, _y2), 2f, Color.Black);

            Drawing.DrawLine(new Vector2(_fovOffset.X, Drawing.Size.Height), new Vector2(-_x, _y2), 3f, Color.Cyan);
            Drawing.DrawLine(new Vector2(_fovOffset.X, Drawing.Size.Height), new Vector2(_x2, _y2), 3f, Color.Cyan);
            //Drawing.DrawCircle(_screenCenter, (float)Config.AimbotConfig.Angle.Value * 2, (float)Config.AimbotConfig.Angle.Value * 2, Color.Green);

        }


        void DrawPlantedBomb()
        {
            var _b = Client.GetPlantedC4();
            if (_b == null)
                return;
            if (_b.m_vecOrigin == Vector3.Zero)
                return;
            //if(PlantedBombEntity == null)
            //    PlantedBombEntity = new PlantedBombEntity(PlantedBomb.BaseAddress);
            //_b.SetSize(10f);
            if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, _b.m_vecOrigin, out var _pos))
                return;

            AddForeGround(_b.m_szBombName + "\n" + _b.m_szExplosionTime, (Color)Config.OtherConfig.cBombColor.Value, _pos, DrawOffset.Center);
            //Drawing.DrawText(form, Color.White, _pos);


        }

        private BaseEntity DroppedC4;

        void DrawDroppedBomb()
        {
            DroppedC4 = Henker.Singleton.Client.GetC4();
            if (DroppedC4 == null)
                return;

            if (DroppedC4.m_vecOrigin != Vector3.Zero)
            {
                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, DroppedC4.m_vecOrigin, out var _pos))
                    return;
                AddForeGround("le bömb", (Color)Config.OtherConfig.cBombColor.Value, _pos, DrawOffset.Center);
                //Drawing.DrawText();
            }
        }

        private void DrawBomb()
        {
            if (!(bool)Config.VisualConfig.DrawBomb.Value)
                return;

            DrawPlantedBomb();
            DrawDroppedBomb();
        }

        private void DrawWeapons()
        {
            if (!(bool)Config.VisualConfig.DrawDroppedWeapons.Value)
                return;
            foreach (var item in Henker.Singleton.Client.GetGroundWeapons())
            {
                if (item.m_vecOrigin == Vector3.Zero) continue;

                if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;
                AddForeGround(Generators.GetClassIdName(item.ClientClass), (Color)Config.OtherConfig.cWeaponColor.Value, _screenpos, DrawOffset.Center);
                //Drawing.DrawText(item.vmtClassId.ToString(), Color.White, _screenpos);
            }
        }

        //private void DrawInferno()
        //{
        //    foreach (var item in Client.GetInfernos())
        //    {
        //        if (item.m_vecOrigin == Vector3.Zero) continue;
        //        if (!item.IsValid) continue;
        //        if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
        //            continue;
        //        AddForeGround("FIRE", Color.Crimson, _screenpos, DrawOffset.Center);
        //    }
        //}
        private void DrawGrenades()
        {
            if (!(bool)Config.VisualConfig.DroppedGrenades.Value)
                return;
            foreach (var item in Henker.Singleton.Client.GetGroundGrenades())
            {
                if (item.m_vecOrigin == Vector3.Zero) continue;

                if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;
                AddForeGround(Generators.GetClassIdName(item.ClientClass), (Color)Config.OtherConfig.cGrenadeColor.Value, _screenpos, DrawOffset.Center);
                //Drawing.DrawText(item.vmtClassId.ToString(), Color.White, _screenpos);
            }
        }
        private List<string> _ergh = new List<string>();
        private void DrawThrowables()
        {
            if (!(bool)Config.VisualConfig.DrawGrenades.Value)
                return;
            foreach (var item in Henker.Singleton.Client.GetProjectiles())
            {
                if (item.m_vecOrigin == Vector3.Zero) continue;

                //if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;

                if (Generators.IsFlashbang(item.m_szModelName) && (bool)Config.VisualConfig.FlashWarning.Value)
                {

                    var _grenadeName = Generators.GetGrenadeName(item.m_szModelName);
                    AddForeGround(_grenadeName, (Color)Config.OtherConfig.cProjectileColor.Value, _screenpos, DrawOffset.Center);
                    if (item.IsVisible)
                    {
                        var _calc = Drawing.MeasureString("WARNING", (int)Config.VisualConfig.FontSize.Value);
                        var _nextpos = new Vector2(_screenpos.X, _screenpos.Y + _calc.Height);
                        AddForeGround("WARNING", SharpDX.Color.Red, _nextpos, DrawOffset.Center);
                    }


                }
                else
                {
                    AddForeGround(Generators.GetGrenadeName(item.m_szModelName), (Color)Config.OtherConfig.cProjectileColor.Value, _screenpos, DrawOffset.Center);
                }



                //if (!_ergh.Contains(item.m_szModelName))
                //    _ergh.Add(item.m_szModelName);
                //Drawing.DrawText(item.vmtClassId.ToString(), Color.White, _screenpos);
            }
            //DrawSimplePositionFromList();
        }

        private void DrawChickens()
        {
            foreach (var item in Client.GetChicks())
            {

                if (item.m_vecOrigin == Vector3.Zero) continue;

                if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;

                AddBackgroundString("Chicken Nuggets", (Color)Config.OtherConfig.cChickenColor.Value, _screenpos, DrawOffset.Center);

                // Drawing.DrawText("nugget", Color.White, _screenpos);
            }
        }
        private void DrawEconEntities()
        {
            foreach (var item in Client.GetDefuseKits())
            {

                if (item.m_vecOrigin == Vector3.Zero) continue;

                if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;

                AddBackgroundString("Defuse Kit", (Color)Config.OtherConfig.cDefuseKitColor.Value, _screenpos, DrawOffset.Center);

                // Drawing.DrawText("nugget", Color.White, _screenpos);
            }
        }
        private void DrawAll()
        {
            if (!(bool)Config.VisualConfig.DrawEverything.Value)
                return;
            foreach (var item in Client.GetAll())
            {
                if (!item.IsValid) continue;
                if (item.m_vecOrigin == Vector3.Zero) continue;

                if (item.Distance(Client.LocalPlayer.m_vecOrigin) > (float)Config.VisualConfig.RenderDistance.Value) continue;

                if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, item.m_vecOrigin, out var _screenpos))
                    continue;
                AddBackgroundString(Generators.GetClassIdName(item.ClientClass) + "\n" + item.m_szModelName, (Color)Config.OtherConfig.cFontColor.Value, _screenpos, DrawOffset.Center);
                //Drawing.DrawText(item.vmtClassId.ToString(), Color.White, _screenpos);
            }
            //DrawSimplePositionFromList(Henker.Singleton.Client.GetAll());
        }


        private Vector2 SpectatorStartPos;
        private Vector2 SpectatorOffset = new Vector2(0, 22);
        private void DrawSpectators()
        {
            if (SpectatorStartPos == Vector2.Zero)
                SpectatorStartPos = new Vector2(Drawing.Size.Width - Drawing.Size.Width / 4, Drawing.Size.Height / 2);
            if (!(bool)Config.VisualConfig.ShowSpectators.Value)
                return;
            int currentNumt = 0;
            AddForeGround("SPECTATORS: ", Color.White, SpectatorStartPos - SpectatorOffset);
            if (Client.LocalPlayer.m_bIsAlive)
            {
                var _specs = Filter.GetSpectators();
                foreach (var item in _specs)
                {

                    AddForeGround(Generators.StartPadString(item.m_szPlayerName, 5), Color.Magenta, SpectatorStartPos + (SpectatorOffset * currentNumt));
                    currentNumt++;
                    //Console.WriteLine("{0} IS SPECTATING LOCALPLAYEr", item.Name);
                }
            }
            else
            {
                foreach (var item in Filter.GetSamePlayerSpectators())
                {
                    AddForeGround(Generators.StartPadString(item.m_szPlayerName, 5), Color.Magenta, SpectatorStartPos + (SpectatorOffset * currentNumt));
                    currentNumt++;
                }
            }

        }

        private DrawpackageManager DrawpackageManager = new DrawpackageManager();
        public override bool Update()
        {
            if (Drawing == null || !MenuAvailable || !CanProcess() || !_isForeGround)
                return false;

            if (Client.LocalPlayer == null || !Engine.IsInGame)
                return false;

            if (!(bool)Config.VisualConfig.Enable.Value)
                return false;





            DrawCustomHud();
            DrawRadar();
            //DrawCrosshair();
            //var _matrix = Client.LocalPlayer.ReadMatrix();
            DrawAll();
            DrawBomb();
            DrawWeapons();
            DrawGrenades();
            DrawThrowables();
            //DrawBones();
            DrawChickens();
            DrawSpectators();
            DrawEconEntities();
            RenderPackages();

            //var _p = Filter.GetActivePlayers(TargetType.Enemy);
            //foreach (var item in _p)
            //{
            //    var _b = item.m_v3aPseudoPredict;
            //    for (int i = 0; i < _b.Length; i++)
            //    {
            //        if (EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Drawing.Size, _b[i], out var _pos))
            //            Drawing.DrawDot(_pos, 3f, 3f, Color.AliceBlue);
            //    }

            //}

            //DrawInferno();
            //if(_once)
            //{
            //var _sprites = Client.GetSprites();

            //foreach (var sprite in _sprites)
            //{
            //    sprite.m_flSpriteScale = 20f;
            //    sprite.m_flGlowProxySize = .50f;
            //    sprite.m_nBrightness = 177;
            //    sprite.m_clrRender = new Color(0, 255, 0, 255);
            //    sprite.m_flHDRColorScale = 5.0f;
            //}
            //var _beams = Client.GetBeams();

            //var _beamEnds = Client.GetBeamEnds();

            //foreach (var item in _beamEnds)
            //{
            //    item.m_clrRender = new Color(9, 0, 255, 255);
            //    item.m_flLightScale = 71f;
            //    item.m_Radius = 90f;
            //}

            //foreach (var item in _beams)
            //{
            //    item.m_clrRender = new Color(255, 0, 0, 255);
            //    item.m_flBeamHDRColorScale = 15f;
            //    item.m_fHaloScale = 7f;
            //    //item.m_nBeamFlags = Beam_Flags_h.NUM_BEAM_FLAGS;
            //    //item.m_nBeamType = BeamType.NUM_BEAM_TYPES;
            //    //item.m_fAmplitude = 200f;
            //    item.m_fWidth = 90f;
            //    item.m_fEndWidth = 666f;
            //    item.m_fFadeLength = 500f;
            //}

            //    _once = true;
            //}

            //var _adresses = _drawPackages.Where(x => x.Index == _filterTargets.Where(z => z.BaseAddress == x.Index).First().BaseAddress);

            /*
             * get drawpackages -> add missing to drawmanager
             * those that have disconnect must be selected from the packagemanager
             * set them as inactive and start fade
             * render();
             * 
             */


            //AddForeGround(Client.GetGameRules().m_totalRoundsPlayed.ToString(), Color.Yellow, new Vector2(100, 120));
            //AddForeGround(Client.GetGameRules().m_eRoundWinReason.ToString(), Color.Yellow, new Vector2(100, 125));
            //string t = ;
            //if (Client.LocalPlayer.m_hActiveWeapon != null)
            //{

            //}
            //AddForeGround((Client.LocalPlayer.m_nTickBase * (Engine.GlobalVars.m_flCurTime * Engine.GlobalVars.m_flIntervalPerTick)).ToString(), Color.Yellow, new Vector2(100, 100));






            return base.Update();
        }

        private void RenderPackages()
        {
            var _filterTargets = Filter.GetActivePlayers((TargetType)g_Globals.Config.VisualConfig.Type.Value);
            var _drawPackages = DrawpackageManager.GetPackages(_filterTargets.ToArray());
            foreach (var item in _drawPackages)
                if (!_filterTargets.Where(x => x.BaseAddress == item.Index).Any())
                    DrawpackageManager.AddFade(item.Index);
            foreach (var item in _drawPackages.OrderByDescending(x => x.BasePlayer.Distance(Client.LocalPlayer.m_vecOrigin)))
            {
                if (!Client.LocalPlayer.m_bIsAlive && Client.LocalPlayer.m_iObserverMode == ObserverMode.OBS_MODE_IN_EYE)
                    if (Client.LocalPlayer.m_iObserverTarget == item.BasePlayer.m_iIndex)
                        continue;
                if (item.CanDraw(Drawing.Size))
                {
                    DrawPackage(item);
                }
            }
            foreach (var item in DrawpackageManager.GetFades())
            {
                //item.Destroy();
            }
        }

        private void DrawPackage(DrawPackage dp)
        {
            foreach (var item in dp.GetRectangles())
            {
                if (item == null) continue;
                switch (item.RectType)
                {

                    case RectType.Fill:
                        Drawing.FillRectangle(item.Rect, item.Color);
                        break;
                    case RectType.Outline:
                        Drawing.DrawRectagleWithOutline(item.Rect, item.Color);
                        break;
                    case RectType.FillAndOutline:
                        Drawing.FillRectangleWithOutline(item.Rect, item.Color);
                        break;
                    default:
                        break;
                }

            }
            DrawDrawpackageStrings(dp);
        }

        private void DrawHead(BaseEntity target, Vector2 theHead)
        {

            Drawing.DrawDot(new SharpDX.Mathematics.Interop.RawVector2(theHead.X, theHead.Y), (float)Config.VisualConfig.HeadSize.Value, (float)Config.VisualConfig.HeadSize.Value, (Color)Config.OtherConfig.cHeadCircleColor.Value);
            Drawing.DrawCircle(new SharpDX.Mathematics.Interop.RawVector2(theHead.X, theHead.Y), (float)Config.VisualConfig.HeadSize.Value, (float)Config.VisualConfig.HeadSize.Value, SharpDX.Color.Black);
        }

        private int DrawPositionCount = Enum.GetValues(typeof(DrawPosition)).Length;
        private void DrawDrawpackageStrings(DrawPackage dp)
        {
            RectangleF _rect = new RectangleF();
            for (int i = 0; i < DrawPositionCount; i++)
            {
                var _next = (DrawPosition)i;
                string _nextString = "";
                switch (_next)
                {
                    case DrawPosition.Top:
                        _nextString = dp.GetString(_next);
                        _rect = Drawing.MeasureString(_nextString, Drawing.FontSize);
                        AddBackgroundString(_nextString, Color.GhostWhite, GetTopPosition(dp.wtsPlayerPos, dp.wtsHeadPos, _rect.Width, _rect.Height));
                        break;
                    case DrawPosition.Left:
                        _nextString = dp.GetString(_next);
                        _rect = Drawing.MeasureString(_nextString, Drawing.FontSize);
                        AddBackgroundString(_nextString, Color.GhostWhite, GetLeftPosition(dp.wtsPlayerPos, dp.wtsHeadPos, dp.height, _rect.Center.Y, _rect.Width));
                        break;
                    case DrawPosition.Right:
                        _nextString = dp.GetString(_next);
                        _rect = Drawing.MeasureString(_nextString, Drawing.FontSize);
                        AddBackgroundString(_nextString, Color.GhostWhite, GetRightPosition(dp.wtsPlayerPos, dp.wtsHeadPos, dp.height, _rect.Center.Y));
                        break;
                    case DrawPosition.Bottom:
                        _nextString = dp.GetString(_next);
                        _rect = Drawing.MeasureString(_nextString, Drawing.FontSize);
                        AddBackgroundString(_nextString, Color.GhostWhite, GetBottomPosition(dp.wtsPlayerPos, dp.wtsHeadPos, dp.height, _rect.Center.X));
                        break;
                    default:
                        break;
                }
            }

        }

        private void DrawRadar()
        {
            if (!(bool)Config.HudConfig.bEnableRadar.Value)
                return;
            DrawRadarBase();
            var _players = Filter.GetActivePlayers(TargetType.All);
            foreach (var item in _players)
            {
                var _itemPos = item.m_vecOrigin;
                var _localPlayerPos = Client.LocalPlayer.m_vecOrigin;
                var direction = _itemPos - _localPlayerPos;
                var entityDirection = new Vector2(direction.X, -direction.Y);

                float fEntDist = EngineMath.GetDistanceToPoint(_localPlayerPos, _itemPos) / 10f;

                //float fEntDist = entityDirection.Length() * (0.02f * 4.7f);
                fEntDist = Math.Min(fEntDist, m_fRadarSize);
                entityDirection.Normalize();
                entityDirection *= fEntDist;
                entityDirection += m_vRadarPos;
                Vector2 absolution = EngineMath.RotatePoint(entityDirection, m_vRadarPos, Client.LocalPlayer.m_vViewAngles.Y - 90);

                Drawing.DrawDot((absolution), 2f, 2f, Generators.GetColorBySetting(Config, item));
            }

        }

        private void DrawCustomHud()
        {
            ////health, armor, money, weapon slots, ammo,

            if (!(bool)Config.HudConfig.bCustomHud.Value)
                return;

            var _active = Client.LocalPlayer.m_hActiveWeapon;

            if (_active != null)
                if (m_dwDrawAmmo(_active))
                {
                    var _text = _active.m_iClip + " / " + _active.m_iClip2;
                    var _measure = Drawing.MeasureString(_text, 30);
                    Drawing.DrawText(_text, Color.White, new Vector2((Drawing.Size.Width / 2) - _measure.Center.X - 2, Drawing.Size.Height - _measure.Height - 1), 30);
                }

            if (Client.LocalPlayer.m_bIsActive)
            {
                Drawing.FillRectangle(new RectangleF(
                    0, Drawing.Size.Height + 1,
                    12, -(Drawing.Size.Height * ((float)Client.LocalPlayer.m_iHealth / 100f)) - 2), Color.Black);
                Drawing.FillRectangle(new RectangleF(1, Drawing.Size.Height, 10, -(Drawing.Size.Height * ((float)Client.LocalPlayer.m_iHealth / 100f))), Generators.ColorByHealth(Client.LocalPlayer.m_iHealth));
                Drawing.FillRectangle(new RectangleF(Drawing.Size.Width - 12, Drawing.Size.Height,
                    12, -(Drawing.Size.Height * ((float)Client.LocalPlayer.m_iArmor / 100f)) - 2), Color.Black);
                Drawing.FillRectangle(new RectangleF(Drawing.Size.Width - 11, Drawing.Size.Height, 10, -(Drawing.Size.Height * ((float)Client.LocalPlayer.m_iArmor / 100f))), Color.RoyalBlue);
            }

            //Client.LocalPlayer.m_iTotalHitsOnServer
            //AddForeGround(_text, );
        }

        private bool m_dwDrawAmmo(BaseCombatWeapon _wp)
        {

            var _wpType = Client.LocalPlayer.GetWeaponClass(_wp.m_iItemDefinitionIndex);

            if (_wpType == WeaponClass.KNIFE
                || _wpType == WeaponClass.OTHER)
                return false;

            return true;

        }


        Vector2 CrossLineStartPos, CrossLineEndPos;
        private void DrawRadarBase()
        {

            //Calc Y axis
            switch ((RadarStyle)Config.HudConfig.rsRadarStyle.Value)
            {
                case RadarStyle.Circular:
                    Drawing.DrawDot(m_vRadarPos, m_fRadarSize, m_fRadarSize, (Color)Config.OtherConfig.m_cRadarBaseColor.Value);
                    Drawing.DrawCircle(m_vRadarPos, m_fRadarSize + 1, m_fRadarSize + 1, (Color)Config.OtherConfig.m_cRadarCrossColor.Value);
                    break;
                case RadarStyle.Box:
                    Drawing.DrawRectangle(m_vRadarPos, m_fRadarSize, (Color)Config.OtherConfig.m_cRadarBaseColor.Value);
                    Drawing.DrawRectangleOutline(m_vRadarPos, m_fRadarSize, (Color)Config.OtherConfig.m_cRadarCrossColor.Value);
                    break;
                default:

                    break;
            }

            CrossLineStartPos = new Vector2(m_vRadarPos.X, m_vRadarPos.Y + m_fRadarSize);
            CrossLineEndPos = new Vector2(m_vRadarPos.X, m_vRadarPos.Y - m_fRadarSize);
            Drawing.DrawLine(CrossLineStartPos, CrossLineEndPos, 1f, (Color)Config.OtherConfig.m_cRadarCrossColor.Value);
            CrossLineStartPos = new Vector2(m_vRadarPos.X + m_fRadarSize, m_vRadarPos.Y);
            CrossLineEndPos = new Vector2(m_vRadarPos.X - m_fRadarSize, m_vRadarPos.Y);
            Drawing.DrawLine(CrossLineStartPos, CrossLineEndPos, 1f, (Color)Config.OtherConfig.m_cRadarCrossColor.Value);
        }
        private void DrawCrosshair()
        {
            if (!(bool)Config.HudConfig.bCrosshair.Value)
                return;

            var _vec = Vector2.Zero;
            if ((bool)Config.VisualConfig.ShowRecoil.Value && Client.LocalPlayer != null)
            {
                var _punch = Client.LocalPlayer.m_vaimPunchAngle;
                _vec = new Vector2(-_punch.Y, _punch.X) * 5.141f;
            }

            Drawing.DrawCrosshair(_screenCenter + _vec,
                (float)Config.HudConfig.fCrosshairLength.Value,
                (float)Config.HudConfig.fCrosshairWidth.Value,
                (Color)Config.OtherConfig.m_cCrossBase.Value,
                (Color)Config.OtherConfig.m_cCrossOutline.Value);
        }
        private Vector2 m_vRadarPos
        {
            get
            {
                return new Vector2((float)Config.HudConfig.xRadarPos.Value, (float)Config.HudConfig.yRadarPos.Value);
            }
        }

        private float m_fRadarSize => (float)Config.HudConfig.fRadarSize.Value;


        private Vector2 GetTopPosition(Vector2 playerPos, Vector2 headPos, float widthoffset, float heightoffset)
        {
            return new Vector2(playerPos.X - widthoffset / 2, headPos.Y - (heightoffset + 1));
        }

        private Vector2 GetRightPosition(Vector2 playerPos, Vector2 headPos, float height, float offset)
        {
            float dist = EngineMath.GetDistanceToPoint(new Vector2(playerPos.X + (height / 4f), headPos.Y), new Vector2(playerPos.X - (height / 4f), headPos.Y));
            float dist2 = EngineMath.GetDistanceToPoint(new Vector2(playerPos.X + (height / 8f), headPos.Y), new Vector2(playerPos.X - (height / 8f), headPos.Y));
            //float _newHeight = playerPos.Y - headPos.Y;
            //float _newWidth = _newHeight / 2;
            return new Vector2(playerPos.X + dist2 + 20f, (headPos.Y + dist) - offset);//(_newHeight / 2) + nameRect.Height / 4);

        }
        private Vector2 GetLeftPosition(Vector2 playerPos, Vector2 headPos, float height, float heightoffset, float widthoffset)
        {
            float dist = EngineMath.GetDistanceToPoint(new Vector2(playerPos.X - (height / 4f), headPos.Y), new Vector2(playerPos.X + (height / 4f), headPos.Y));
            float dist2 = EngineMath.GetDistanceToPoint(new Vector2(playerPos.X - (height / 8f), headPos.Y), new Vector2(playerPos.X + (height / 8f), headPos.Y));
            //return new Vector2(playerPos.X + dist2 + 20f, (headPos.Y + dist) - heightoffset);//(_newHeight / 2) + nameRect.Height / 4);
            return new Vector2((playerPos.X - dist2 - 20f) - widthoffset, (headPos.Y + dist) - heightoffset);//(_newHeight / 2) + nameRect.Height / 4);

        }
        private Vector2 GetBottomPosition(Vector2 playerPos, Vector2 headPos, float height, float offset)
        {
            var dist = EngineMath.GetDistanceToPoint(new Vector2(playerPos.X + (height / 4), headPos.Y), new Vector2(playerPos.X - (height / 4), playerPos.Y) + 1);
            return new Vector2(playerPos.X - offset, headPos.Y + dist);
        }
    }
}
