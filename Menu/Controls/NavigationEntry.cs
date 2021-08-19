using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class NavigationEntry
    {

        //private System.Windows.Forms.Cursor CustomCursor = Generators.LoadCursorFromResource(ResurrectedEternal.Properties.Resources.fuck);
        private Color _activeColor = Color.Cyan;
        private Color _inactiveColor = Color.DeepSkyBlue;
        private Color _onClickColor = Color.Crimson;
        public Color BackgroundColor = new Color(66, 66, 66, 200);
        public Color ForeColor;
        public string Title;
        public RectangleF Rect;
        public Vector2 Position;
        public bool Active = false;

        private bool IsInBounds = false;
        private Container m_dwActiveContainer;
        public Container ActiveContainer
        {
            get { return m_dwActiveContainer; }
           set { m_dwActiveContainer = value; }
        }
        public void OnClick()
        {
            if (Active)
                return;
            ForeColor = _onClickColor;
            Active = true;
        }

        public void OnEnter()
        {
            if (IsInBounds)
                return;
            IsInBounds = true;
            ForeColor = _activeColor;
        }

        public void OnExit()
        {
            if (!IsInBounds)
                return;
            IsInBounds = false;
            ForeColor = _inactiveColor;
        }
        public NavigationEntry(RectangleF navPosition, int row, float width, string title, int fontSize, Container _container)
        {
            ActiveContainer = _container;
            ForeColor = _inactiveColor;
            var _measure = Draw.Drawing.instance.MeasureString(title, fontSize);
            Position = new Vector2(navPosition.X + 2, navPosition.Y + (row * width));
            Rect = new RectangleF(navPosition.X + 2, navPosition.Y + (row * width), _measure.Width, _measure.Height);
            Title = title;
        }

        internal void OnDeselect()
        {
            if (!Active)
                return;
            Active = false;
        }

        public StringContainer GetActiveContainer()
        {
            if (m_dwActiveContainer.StringContainers.Where(x => x.Active).Any())
                return m_dwActiveContainer.StringContainers.Where(x => x.Active).First();
            return null;
        }
        public StringContainer[] GetAllStringContainers()
        {
            return m_dwActiveContainer.StringContainers;
        }
    }
}
