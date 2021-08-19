using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls.Button
{
    public class MenuButton
    {
        public RectangleF Rect;
        public string Text;
        public int FontSize;
        private Vector2 _renderPosition;
        public Vector2 m_vpRenderPosition
        {
            get
            {
                return _renderPosition;
            }
            private set
            {
                _renderPosition = value;
            }
        }

        private bool IsHovering = false;

        private Color TextForeColor = Color.Goldenrod;
        private Color TextHoverColor = Color.Gold;

        private Color RectForeColor = new Color(24, 24, 24, 255);
        private Color RectHoverColor = new Color(44, 44, 44, 255);

        public Color RectColor;

        public Color TextColor;

        private Action OnPress;
        public RectangleF TextRect;

        private int LeftPad = 5;

        //Take parent and use height offset as bottom line, subtract height of element.
        //How to 
        /// <summary>
        /// Creates a new button
        /// </summary>
        /// <param name="_OnPress">The action to perform on mouse down</param>
        /// <param name="_parent">The parent container</param>
        /// <param name="_fontSize"></param>
        /// <param name="text"></param>
        /// <param name="width">the amount of space we have available if were adding multiple buttons to a row</param>
        public MenuButton(Action _OnPress, RectangleF _parent, int _fontSize, string text, float PREVIOUSCONTAINER)
        {
            FontSize = _fontSize;
            Text = text;
            TextRect = Draw.Drawing.instance.MeasureString(Text, FontSize);
            float _heightOffset = _parent.Y + _parent.Height - (TextRect.Height);
            Rect = new RectangleF(_parent.X + _parent.Width + PREVIOUSCONTAINER, _heightOffset, TextRect.Width + 10, TextRect.Height);
            _renderPosition = new Vector2(Rect.X + 5, Rect.Y);
            OnPress = _OnPress;
            RectColor = RectForeColor;
            TextColor = TextForeColor;
        }

        public virtual void OnMouseDown()
        {
            OnPress();
        }
        public virtual void OnMouseEnter()
        {
            if (IsHovering)
                return;
            TextColor = TextHoverColor;
            RectColor = RectHoverColor;
            IsHovering = true;
        }

        public virtual void OnMouseExit()
        {
            if (!IsHovering)
                return;
            TextColor = TextForeColor;
            RectColor = RectForeColor;
            IsHovering = false;
        }
    }
}
