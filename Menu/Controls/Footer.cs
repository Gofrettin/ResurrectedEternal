using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class Footer
    {
        public RectangleF Rect;
        public Vector2 Position;
        public string Text;
        public Footer(string text, RectangleF _mainContainer)
        {
            Text = text;
            Rect = Draw.Drawing.instance.MeasureString(text, 11);
            Position = new Vector2(_mainContainer.X + 5, _mainContainer.Height - Rect.Height);
        }
    }
}
