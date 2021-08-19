using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class Header
    {
        public RectangleF HeaderRect;
        public string MenuHeaderText = "RESURRECTED CHEATS - BECAUSE WE CAN";
        public Vector2 MenuHeaderTextPosition;
        public int MainMenuFontSize = 24;
        public SharpDX.Color HeaderBackgroundColor = new Color(44, 44, 44, 200);

        public RectangleF HeaderTextRect;

        public int HeaderFontSize;

        public Header(RectangleF _screen, int headerFontSize)
        {
            HeaderFontSize = Convert.ToInt32(headerFontSize * MainMenu.MagicFloat);
            HeaderRect = new RectangleF(MainMenu.MenuPosXAbsolute, MainMenu.MenuPosYAbsolute, MainMenu.MenuWidth, 44);
            HeaderTextRect = Draw.Drawing.instance.MeasureString(MenuHeaderText, HeaderFontSize);
           MainMenuFontSize = Convert.ToInt32(24 * MainMenu.MagicFloat);
            //MenuHeaderTextPosition = new Vector2((_screen.Width / 2) - (MainMenu.MenuWidth / 2) + (HeaderTextRect.Width / 2), (_screen.Height / 2) - (MainMenu.MenuHeight / 2) - HeaderTextRect.Height);
            MenuHeaderTextPosition = new Vector2(HeaderRect.X + (HeaderRect.Width / 2) - HeaderTextRect.Width / 2 - 24, HeaderRect.Y + (HeaderRect.Height / 2) - HeaderTextRect.Height / 2);
        }
    }
}
