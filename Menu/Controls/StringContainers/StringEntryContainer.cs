using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls.StringContainers
{

    class StringEntryContainer : StringContainer
    {
        private Vector2 m_vpRenderPosition;
        private Vector2 m_vpvRenderPosition;
        private RectangleF ContainerStart;
        private Color _activeColor = Color.PaleGoldenrod;
        private Color _inactiveColor = Color.Goldenrod;
        private Color _onClickColor = Color.Crimson;
        private RectangleF TitleFontRect;
        private RectangleF ValueFontRect;

        private int Row, Column;
        private float Width, Height;
        private float ContainerWidth;

        private float StartPosX, StartPosY;

        private float Padding = 15f;

        public StringEntryContainer(RectangleF _ContainerStart, ConfigValueEntry configEntry, int row, int column, float height, float width, int fontSize, float containerWidth, PadStyle padStyle = PadStyle.Center)
            : base(_ContainerStart, configEntry, row, column, height, width, fontSize, containerWidth)
        {
            StringContainerType = StringContainerType.Entry;
            ContainerWidth = containerWidth;
            StringContainerFontSize = fontSize;
            ForeColor = _inactiveColor;
            ContainerStart = _ContainerStart;
            Row = row;
            Column = column;
            Height = height;
            Width = width;
            ConfigValue = configEntry;
            TitleFontRect = Draw.Drawing.instance.MeasureString(ConfigValue.Name, StringContainerFontSize);
            float cnt_width = ContainerWidth / 2;
            switch (padStyle)
            {

                case PadStyle.Left:
                    StartPosX = ContainerStart.X + (Column * Width) + Padding;
                    StartPosY = ContainerStart.Y + (Row * Height);
                    m_vpRenderPosition = new Vector2(StartPosX, StartPosY);
                    Rect = new RectangleF(m_vpRenderPosition.X, m_vpRenderPosition.Y, ContainerWidth - Padding - cnt_width, TitleFontRect.Height);

                    break;
                case PadStyle.Right:
                    StartPosX = ContainerStart.X + (Column * Width) + Padding + cnt_width;
                    StartPosY = ContainerStart.Y + (Row * Height);
                    m_vpRenderPosition = new Vector2(StartPosX, StartPosY);
                    Rect = new RectangleF(m_vpRenderPosition.X, m_vpRenderPosition.Y, cnt_width - Padding, TitleFontRect.Height);
                    break;
                case PadStyle.Center:
                default:
                    StartPosX = ContainerStart.X + (Column * Width) + Padding;
                    StartPosY = ContainerStart.Y + (Row * Height);
                    m_vpRenderPosition = new Vector2(StartPosX, StartPosY);
                    Rect = new RectangleF(m_vpRenderPosition.X, m_vpRenderPosition.Y, ContainerWidth - Padding, TitleFontRect.Height);
                    break;
            }


        }
        public StringEntryContainer(RectangleF _ContainerStart, ConfigValueEntry configEntry, int row, int column, float height, float width, int fontSize, float containerWidth, int Position, int Count)
    : base(_ContainerStart, configEntry, row, column, height, width, fontSize, containerWidth)
        {
            StringContainerType = StringContainerType.MultiEntry;
            ContainerWidth = containerWidth;
            StringContainerFontSize = fontSize;
            ForeColor = _inactiveColor;
            ContainerStart = _ContainerStart;
            Row = row;
            Column = column;
            Height = height;
            Width = width;
            ConfigValue = configEntry;

            float cntNewWidth = containerWidth / Count;
            TitleFontRect = Draw.Drawing.instance.MeasureString(ConfigValue.Name, StringContainerFontSize);
            var a = Position * cntNewWidth;
            StartPosX = ContainerStart.X + (Column * Width) + a + Padding;
            //if pos is 0 well increment by 0!
            //pos y wil always be the same

            m_vpRenderPosition = new Vector2(StartPosX, ContainerStart.Y + (Row * Height)); //Needs to be on the same row!
            Rect = new RectangleF(m_vpRenderPosition.X, m_vpRenderPosition.Y, cntNewWidth - Padding, TitleFontRect.Height);
        }
        public override Vector2 m_vRenderPosition
        {
            get
            {
                return m_vpRenderPosition;
            }
        }
        public override Vector2 m_vValueRenderPosition
        {
            get
            {
                //m_vpRenderPosition = new Vector2(ContainerStart.X + (Column * Width), ContainerStart.Y + (Row * Height) + Rect.Center.Y);
                ValueFontRect = Draw.Drawing.instance.MeasureString(MenuConfigCaster.CorrectValue(ConfigValue), StringContainerFontSize);
                ValueFontRect.Width -= -Padding;
                m_vpvRenderPosition = new Vector2(StartPosX + Rect.Width - ValueFontRect.Width, ContainerStart.Y + (Row * Height));
                return m_vpvRenderPosition;
            }
        }
        public override void Decrement()
        {
            SetValue(ref ConfigValue.Value, ConfigValue.Incremental, ConfigValue.MinValue, ConfigValue.MaxValue, false);
        }

        public override void Increment()
        {
            SetValue(ref ConfigValue.Value, ConfigValue.Incremental, ConfigValue.MinValue, ConfigValue.MaxValue);
        }

        public override void OnClick()
        {
            ForeColor = _onClickColor;
        }

        public override void OnEnter()
        {
            if (Active)
                return;
            Active = true;
            ForeColor = _activeColor;
        }

        public override void OnExit()
        {
            if (!Active)
                return;
            Active = false;
            ForeColor = _inactiveColor;
        }

        private void SetValue(ref object value, object incremental, object min, object max, bool increment = true)
        {
            var _type = value.GetType();
            if (_type == typeof(float))
            {
                float _fVal = (float)value;
                float _icmt = Convert.ToSingle(incremental);


                if (!increment)
                    _fVal -= _icmt;
                else
                    _fVal += _icmt;
                value = EngineMath.Clamp(_fVal, Convert.ToSingle(min), Convert.ToSingle(max));
            }
            else if (_type == typeof(bool))
            {
                value = !(bool)value;
            }
            else if (_type == typeof(int))
            {
                int _iVal = (int)value;
                int _icmt = Convert.ToInt32(incremental);
                if (!increment)
                    _iVal -= _icmt;
                else
                    _iVal += _icmt;
                value = EngineMath.Clamp(_iVal, Convert.ToInt32(min), Convert.ToInt32(max));
            }
            else if (_type == typeof(TargetType) || _type == typeof(SmoothType) || _type == typeof(AimPriority))
            {

                if (_type == typeof(TargetType))
                {
                    if (!increment)
                        value = EnumCaster.Previous((TargetType)value);
                    else
                        value = EnumCaster.Next((TargetType)value);
                }
                else if (_type == typeof(SmoothType))
                {
                    if (!increment)
                        value = EnumCaster.Previous((SmoothType)value);
                    else
                        value = EnumCaster.Next((SmoothType)value);
                }
                else if (_type == typeof(AimPriority))
                {
                    if (!increment)
                        value = EnumCaster.Previous((AimPriority)value);
                    else
                        value = EnumCaster.Next((AimPriority)value);
                }

            }
            else if (_type == typeof(VirtualKeys))
            {
                if (!increment)
                    value = EnumCaster.Previous((VirtualKeys)value);
                else
                    value = EnumCaster.Next((VirtualKeys)value);
                //value = (VirtualKeys)MenuConfigCaster.GetKeyByIndex(_iVal);
            }
            else if (_type == typeof(KNIFEINDEX))
            {
                if (!increment)
                    value = EnumCaster.Previous((KNIFEINDEX)value);
                else
                    value = EnumCaster.Next((KNIFEINDEX)value);
            }
            else if (_type == typeof(DrawPosition))
            {
                if (!increment)
                    value = EnumCaster.Previous((DrawPosition)value);
                else
                    value = EnumCaster.Next((DrawPosition)value);
            }
            else if (_type == typeof(SharpDX.Color))
            {
                if (!increment)
                {
                    value = g_Globals.ColorManager.GetPrevious((SharpDX.Color)value);
                }
                else
                {
                    value = g_Globals.ColorManager.GetNextColor((SharpDX.Color)value);
                }
            }
            else if (_type == typeof(GlowRenderStyle_t))
            {
                if (!increment)
                    value = EnumCaster.Previous((GlowRenderStyle_t)value);
                else
                    value = EnumCaster.Next((GlowRenderStyle_t)value);
            }
            else if (_type == typeof(SharpDX.DirectWrite.FontStyle))
            {
                if (!increment)
                    value = EnumCaster.Previous((SharpDX.DirectWrite.FontStyle)value);
                else
                    value = EnumCaster.Next((SharpDX.DirectWrite.FontStyle)value);
            }
            else if (_type == typeof(SharpDX.DirectWrite.FontWeight))
            {
                if (!increment)
                    value = EnumCaster.Previous((SharpDX.DirectWrite.FontWeight)value);
                else
                    value = EnumCaster.Next((SharpDX.DirectWrite.FontWeight)value);
            }
            else if (_type == typeof(string))
            {
                if (!increment)
                    value = GetNextFont(GetIndexOfFont((string)value));
                else
                    value = GetPreviousFont(GetIndexOfFont((string)value));
            }
            else if(_type == typeof(Skyboxes))
            {
                if (!increment)
                    value = EnumCaster.Previous((Skyboxes)value);
                else
                    value = EnumCaster.Next((Skyboxes)value);
            }
            else if(_type == typeof(RCSSmoothType))
            {
                if (!increment)
                    value = EnumCaster.Previous((RCSSmoothType)value);
                else
                    value = EnumCaster.Next((RCSSmoothType)value);
            }
            else if (_type == typeof(AimPoint))
            {
                if (!increment)
                    value = EnumCaster.Previous((AimPoint)value);
                else
                    value = EnumCaster.Next((AimPoint)value);
            }
            else if(_type == typeof(RadarStyle))
            {
                if (!increment)
                    value = EnumCaster.Previous((RadarStyle)value);
                else
                    value = EnumCaster.Next((RadarStyle)value);
            }
            else if(_type == typeof(VisibleCheck))
            {
                if (!increment)
                    value = EnumCaster.Previous((VisibleCheck)value);
                else
                    value = EnumCaster.Next((VisibleCheck)value);
            }
        }

        private int GetIndexOfFont(string param)
        {
            for (int i = 0; i < g_Globals.FontManager.Fonts.Length; i++)
            {
                if (g_Globals.FontManager.Fonts[i] == param)
                {
                    return i;
                }
            }
            return 0;
        }
        private string GetPreviousFont(int idx)
        {
            idx -= 1;
            if (idx < 0)
                return g_Globals.FontManager.Fonts[g_Globals.FontManager.Fonts.Length - 1];
            return g_Globals.FontManager.Fonts[idx];
        }
        private string GetNextFont(int idx)
        {
            idx += 1;
            if (idx >= g_Globals.FontManager.Fonts.Length)
                return g_Globals.FontManager.Fonts[0];
            return g_Globals.FontManager.Fonts[idx];
        }
    }
}
