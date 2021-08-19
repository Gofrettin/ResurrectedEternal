using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class Navigation : Header
    {
        public RectangleF NavRect;
        public Color NavBackgroundColor = new Color(24, 24, 24, 222);
        public List<NavigationEntry> NavigationEntries = new List<NavigationEntry>();



        public int NavFontSize;
        private int NavWidth => Convert.ToInt32(150 * MainMenu.MagicFloat);
        public Navigation(RectangleF _screen, int navFontSize, int headerFontSize) : base(_screen, headerFontSize)
        {
            NavFontSize = navFontSize;
            NavRect = new RectangleF(HeaderRect.X - 1, HeaderRect.Y + HeaderRect.Height, NavWidth, MainMenu.MenuHeight - HeaderRect.Height + 1);
        }
        public void AddNavEntry(string text, Container _container)
        {
            if(NavigationEntries.Where(x => x.Title == text).Any())
            {
                NavigationEntries.Where(x => x.Title == text).First().ActiveContainer = _container;
                return;
            }
            NavigationEntries.Add(new NavigationEntry(NavRect, NavigationEntries.Count, 25 * MainMenu.MagicFloat, text, NavFontSize, _container));
        }

        public NavigationEntry GetActiveBit()
        {
            if (NavigationEntries.Where(x => x.Active).Any())
                return NavigationEntries.Where(x => x.Active).First();
            return null;
        }
    }
}
