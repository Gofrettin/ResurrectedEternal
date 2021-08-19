using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Objects;
using RRFull;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.Drawpackage
{
    public enum RectType
    {
        Fill,
        FillAndOutline,
        Outline
    }
    class RectanglePack
    {
        public Color Color;
        public RectangleF Rect;
        public RectType RectType;
    }

    class DrawPackage
    {
        public IntPtr Index => BasePlayer.BaseAddress;
        public bool Active = true;
        public List<string> TopStrings = new List<string>();
        public List<string> LeftStrings = new List<string>();
        public List<string> RightStrings = new List<string>();
        public List<string> BottomStrings = new List<string>();
        public Color FontColor = Color.White;
        public Color LastColor = Color.White;
        public float height;
        public Vector2 wtsHeadPos;
        public Vector2 wtsPlayerPos;

        private Vector3 LastWorldPlayerPosition;
        private Vector3 LastWorldHeadPosition;

        private DateTime FadeStarted;
        private TimeSpan FadeDuration = TimeSpan.FromMilliseconds(250);

        private Color FontFaceColor = Color.GhostWhite;
        private Color BackGroundColor = Color.DarkSlateGray;
        private Color HealthColor = Color.LimeGreen;
        private Color ArmorColor = Color.RoyalBlue;
        private Config Config => g_Globals.Config;
        private Client Client => Henker.Singleton.Client;
        public BasePlayer BasePlayer { set; get; }

        private RectanglePack[] Rectangles = new RectanglePack[5];

        //private Dictionary<string, RectanglePack> Rectangles = new Dictionary<string, RectanglePack>();
        public DrawPackage(BasePlayer _bp)
        {
            BasePlayer = _bp;
            //Rectangles.Add("armorback", new RectanglePack());
            //Rectangles.Add("armor", new RectanglePack());
            //Rectangles.Add("healthback", new RectanglePack());
            //Rectangles.Add("health", new RectanglePack());
            //Rectangles.Add("box", new RectanglePack());
        }

        public void SetRect(int index, float x, float y, float w, float h, RectType type, Color clr)
        {
            if (Rectangles[index] == null)
                Rectangles[index] = new RectanglePack();
            Rectangles[index].Rect = new RectangleF(x, y, w, h);
            Rectangles[index].RectType = type;
            Rectangles[index].Color = clr;
        }

        public RectanglePack GetRect(int index)
        {
            //if (!Rectangles.ContainsKey(name))
            //    throw new Exception("Invalid Operation!");
            return Rectangles[index];
        }

        public string GetString(DrawPosition DrawPos)
        {
            switch (DrawPos)
            {
                case DrawPosition.Top:
                    return string.Join("\n", TopStrings.ToArray());
                case DrawPosition.Left:
                    return string.Join("\n", LeftStrings.ToArray());
                case DrawPosition.Right:
                    return string.Join("\n", RightStrings.ToArray());
                case DrawPosition.Bottom:
                    return string.Join("\n", BottomStrings.ToArray());
                default:
                    return "";
            }
        }

        public void AddString(string text, DrawPosition drawPosition)
        {
            if (string.IsNullOrEmpty(text))
                return;
            switch (drawPosition)
            {
                case DrawPosition.Top:
                    TopStrings.Add(text);
                    break;
                case DrawPosition.Left:
                    LeftStrings.Add(text);
                    break;
                case DrawPosition.Right:
                    RightStrings.Add(text);
                    break;
                case DrawPosition.Bottom:
                    BottomStrings.Add(text);
                    break;
                default:
                    break;
            }
        }

        public bool CanDraw(Size2 Size)
        {
            ClearStrings();



            if (!BasePlayer.m_bIsActive)
                return false;
            if (Active)
            {
                LastWorldPlayerPosition = BasePlayer.m_vecOrigin;
                LastWorldHeadPosition = BasePlayer.m_vecHead;
            }
            if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Size, LastWorldPlayerPosition, out wtsPlayerPos))
                return false;
            if (!EngineMath.WorldToScreen(Client.LocalPlayer.view_matrix_t, Size, LastWorldHeadPosition + (Vector3.UnitZ * 8), out wtsHeadPos))
                return false;
            if (wtsPlayerPos == Vector2.Zero && wtsHeadPos == Vector2.Zero)
                return false;

            height =  wtsHeadPos.Y- wtsPlayerPos.Y;

            if ((bool)Config.VisualConfig.DrawBox.Value)
                CalculateBox(wtsHeadPos, wtsPlayerPos);
            else
                Rectangles[0] = null;

            if ((bool)Config.VisualConfig.DrawHealth.Value)
                CalculateHealth();
            else
            {
                Rectangles[1] = null;
                Rectangles[2] = null;
            }
                

            if ((bool)Config.VisualConfig.DrawArmor.Value)
                CalculateArmor();
            else
            {
                Rectangles[3] = null;
                Rectangles[4] = null;
            }
                

            if ((bool)Config.VisualConfig.DrawName.Value)
                AddString(BasePlayer.m_szPlayerName, (DrawPosition)Config.VisualConfig.NamePos.Value);
            if ((bool)Config.VisualConfig.DrawScoped.Value)
                AddString(BasePlayer.m_bIsScoped ? "In Scope" : "", (DrawPosition)Config.VisualConfig.ScopedPos.Value);
            if ((bool)Config.VisualConfig.DrawSneaking.Value)
                AddString(BasePlayer.m_bIsSneaking ? "Silent" : "", (DrawPosition)Config.VisualConfig.SneakingPos.Value);
            if ((bool)Config.VisualConfig.DrawDefuser.Value)
                AddString(BasePlayer.m_bHasDefuser ? "Defuse Kit" : "", (DrawPosition)Config.VisualConfig.DefuserPos.Value);
            if ((bool)Config.VisualConfig.DrawDefusing.Value)
                AddString(BasePlayer.m_bIsDefusing ? "Defusing!!" : "", (DrawPosition)Config.VisualConfig.DefusingPos.Value);
            if ((bool)Config.VisualConfig.DrawWeapon.Value)
            {
                if((bool)Config.VisualConfig.DrawAllWeapon.Value)
                {
                    var _weps = BasePlayer.m_hMyWeapons;
                    for (int i = _weps.Length - 1; i > -1; i--)
                    {
                        if (_weps[i] == null) continue;
                        AddString(_weps[i].m_iItemDefinitionIndex.ToString(), (DrawPosition)Config.VisualConfig.WeaponPos.Value);
                    }
                }
                else
                {
                    var _weapon = BasePlayer.m_hActiveWeapon;
                    if (_weapon != null && _weapon.IsValid)
                    {
                        AddString(_weapon.m_iItemDefinitionIndex.ToString(), (DrawPosition)Config.VisualConfig.WeaponPos.Value);
                    }
                }



            }
           
            if ((bool)Config.VisualConfig.DrawDistance.Value)
                AddString(Client.LocalPlayer.DistanceInMetresString(LastWorldPlayerPosition), (DrawPosition)Config.VisualConfig.DistancePos.Value);
            return true;

        }

        private void CalculateBox(Vector2 theHead, Vector2 worldPos)
        {
            float thicknessOffset = (float)Config.VisualConfig.LineThickness.Value;
            float espHeight = theHead.Y - worldPos.Y;
            float espHeightOffset = .333f;
            //SetRect(
            //    "outer",
            //    worldPos.X + (espHeight / 4) - (thicknessOffset),
            //    (theHead.Y - espHeightOffset) - (thicknessOffset),
            //    EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), (theHead.Y - espHeightOffset)) + (1.8f * thicknessOffset)),
            //    EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), worldPos.Y) + (1.3f * thicknessOffset)));
            //SetRect(
            //    "inner",
            //    worldPos.X + (espHeight / 4) + (thicknessOffset),
            //    (theHead.Y - espHeightOffset) + (thicknessOffset),
            //    EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), (theHead.Y - espHeightOffset)) - (1.8f * thicknessOffset)),
            //    EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), worldPos.Y) - (1.3f * thicknessOffset)));
            SetRect(0,
                worldPos.X + (espHeight / 4),
                (theHead.Y - espHeightOffset),
                EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), (theHead.Y - espHeightOffset))),
                EngineMath.GetDistanceToPoint(new Vector2(worldPos.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(worldPos.X - (espHeight / 4), worldPos.Y)), RectType.Outline,
                Generators.GetColorBySetting(Config, BasePlayer));

        }

        private void CalculateHealth()
        {
            float dist = EngineMath.GetDistanceToPoint(new Vector2(wtsPlayerPos.X + (height / 4), wtsHeadPos.Y), new Vector2(wtsPlayerPos.X - (height / 4), wtsPlayerPos.Y));
            float val = BasePlayer.m_iHealth / 100.0f;
            float _x = wtsPlayerPos.X + (height / 4) - 10;
            float _y = (wtsHeadPos.Y) + dist;
            SetRect(1, _x, _y + 4, 3, -dist - 7f, RectType.FillAndOutline, Color.DarkSlateGray);
            SetRect(2, _x, _y, 3, -(dist * val), RectType.Fill, Color.Lime);

        }
        private void CalculateArmor()
        {
            float dist = EngineMath.GetDistanceToPoint(new Vector2(wtsPlayerPos.X + (height / 4), wtsHeadPos.Y), new Vector2(wtsPlayerPos.X - (height / 4), wtsPlayerPos.Y));
            float val = BasePlayer.m_iArmor / 100.0f;
            float _x = wtsPlayerPos.X
                + (height / 4)
                + 5
                + EngineMath.GetDistanceToPoint(
                    new Vector2(wtsPlayerPos.X + (height / 4), (wtsHeadPos.Y)),
                    new Vector2(wtsPlayerPos.X - (height / 4), (wtsHeadPos.Y)));

            float _y = (wtsHeadPos.Y) + dist;
            SetRect(3, _x, _y + 4, 3, -dist - 7f, RectType.FillAndOutline, Color.DarkSlateGray);
            SetRect(4, _x, _y, 3, -(dist * val), RectType.Fill, Color.RoyalBlue);

        }

        private void ClearStrings()
        {
            TopStrings.Clear();
            LeftStrings.Clear();
            RightStrings.Clear();
            BottomStrings.Clear();
        }

        public void StartFade()
        {
            Active = false;
            FadeStarted = DateTime.Now;
        }

        public void Destroy()
        {
            TopStrings.Clear();
            TopStrings = null;
            LeftStrings.Clear();
            LeftStrings = null;
            RightStrings.Clear();
            RightStrings = null;
            BottomStrings.Clear();
            BottomStrings = null;
            Rectangles = null;
        }

        internal IEnumerable<RectanglePack> GetRectangles()
        {
            return Rectangles;
        }
    }
}
