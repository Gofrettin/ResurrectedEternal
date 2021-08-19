using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class MenuBase : Navigation
    {
        public RectangleF MenuRect;
        public Color MenuBackgroundColor = new Color(24, 24, 24, 222);

        public MenuBase(RectangleF _screen, int navFontSize, int headerFontSize) : base(_screen, navFontSize, headerFontSize)
        {
            //MenuRect = new RectangleF((_screen.Width / 2) - (MainMenu.MenuWidth / 2), (_screen.Height / 2) - (MainMenu.MenuHeight / 2), MainMenu.MenuWidth, MainMenu.MenuHeight);
            MenuRect = new RectangleF(MainMenu.MenuPosXAbsolute, MainMenu.MenuPosYAbsolute, MainMenu.MenuWidth, MainMenu.MenuHeight);
        }

    }
}
