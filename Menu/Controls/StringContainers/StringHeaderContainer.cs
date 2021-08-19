using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls.StringContainers
{
    class StringHeaderContainer : StringContainer
    {
        private Vector2 m_vpRenderPosition;
        private RectangleF ContainerStart;
        private Color _activeColor = Color.Cyan;
        private Color _inactiveColor = Color.Goldenrod;
        private Color _onClickColor = Color.Crimson;
        private RectangleF TitleFontRect;

        private int Row, Column;
        private float Width, Height;
        private float ContainerWidth;

        private int FontOffset = 4;

        public StringHeaderContainer(RectangleF _ContainerStart, ConfigValueEntry configEntry, int row, int column, float height, float width, int fontSize, float containerWidth)
            : base(_ContainerStart, configEntry, row, column, height, width, fontSize, containerWidth)
        {
            StringContainerType = StringContainerType.Header;
            ContainerWidth = containerWidth;
            StringContainerFontSize = fontSize + FontOffset;
            ForeColor = _inactiveColor;
            ContainerStart = _ContainerStart;
            Row = row;
            Column = column;
            Height = height;
            Width = width;
            ConfigValue = configEntry;
            TitleFontRect = Draw.Drawing.instance.MeasureString(ConfigValue.Name, StringContainerFontSize);
            float x = ContainerStart.X + (Column * Width) + 5;
            float y = ContainerStart.Y + (Row * Height) + StringContainerFontSize / 4;
            
            Rect = new RectangleF(x, y, containerWidth - StringContainerFontSize * 1.5f, TitleFontRect.Height);
            m_vpRenderPosition = new Vector2(Rect.X, Rect.Y);
            ForeColor = Color.Crimson;
        }
        public override Vector2 m_vRenderPosition
        {
            get
            {
                return m_vpRenderPosition;
            }
        }
        //public override Vector2 m_vValueRenderPosition
        //{
        //    get
        //    {
        //        return Vector2.Zero;
        //        //m_vpRenderPosition = new Vector2(ContainerStart.X + (Column * Width), ContainerStart.Y + (Row * Height) + Rect.Center.Y);
        //        ValueFontRect = Draw.Drawing.instance.MeasureString(MenuConfigCaster.CorrectValue(ConfigValue), StringContainerFontSize);
        //        m_vpvRenderPosition = new Vector2(ContainerStart.X + (Column * Width) + Rect.Width - ValueFontRect.Width, ContainerStart.Y + (Row * Height) + StringContainerFontSize);
        //        return m_vpvRenderPosition;
        //    }
        //}

        public override void Decrement()
        {
            base.Decrement();
        }

        public override void Increment()
        {
            base.Increment();
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
