using ResurrectedEternal.Menu.Controls.StringContainers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public class Container
    {
        public int ContainerNum;
        public float Width = 270f;
        public float Height => StringContainers.Length * SingleLineHeight;
        public float SingleLineHeight => 22 * MainMenu.MagicFloat;
        public StringContainer[] StringContainers;
        //public Color NormalColor = Color.Goldenrod;
        //public Color HighlightColor = Color.Cyan;

        public RectangleF ContainerPos;


        public ConfigValueEntry[] Entries;
        private int FontSize;

        public Container(ConfigValueEntry[] cfgs, int containerNum, RectangleF _navRect, float headerHeight, int fontSize)
        {
            FontSize = fontSize;
            ContainerPos = new RectangleF(_navRect.X + _navRect.Width, _navRect.Y, MainMenu.MenuWidth - _navRect.Width, MainMenu.MenuHeight - headerHeight);
            ContainerNum = containerNum;
            StringContainers = new StringContainer[cfgs.Length];
            Entries = cfgs;
            int Column = 0;
            int Row = 0;
            List<StringContainer> _containers = new List<StringContainer>();
            for (int i = 0; i < Entries.Length; i++)
            {
                Row++;
                if(!string.IsNullOrEmpty(Entries[i].Header))
                {
                    //if we have a header, well add a new stringheadercontainer before the actual container and increase the row count by 1
                    _containers.Add(new StringHeaderContainer(ContainerPos, Entries[i], Row, Column, SingleLineHeight, Width, FontSize, ContainerPos.Width));
                    Row += 2;
                }

                if(Entries[i].IsGrouped)
                {
                    //select all entries where groupid matches.
                    var _grouped = Entries.Where(x => x.GroupId == Entries[i].GroupId).ToArray();
                    //increment counter to skip next, so its important they are ordered or we mess up
                    i += _grouped.Length - 1;
                    int _pos = 0;
                    foreach (var item in _grouped)
                    {
                        
                        _containers.Add(new StringEntryContainer(ContainerPos, item, Row, Column, SingleLineHeight, Width, FontSize, ContainerPos.Width, _pos, _grouped.Length));
                        _pos++;
                    }
                    continue;
                }
                _containers.Add(new StringEntryContainer(ContainerPos, Entries[i], Row, Column, SingleLineHeight, Width, FontSize, ContainerPos.Width, Entries[i].PadStyle));
            }
            StringContainers = _containers.ToArray();
        }
    }
}
