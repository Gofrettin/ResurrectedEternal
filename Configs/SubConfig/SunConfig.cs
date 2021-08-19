using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.SubConfig
{
    [System.Serializable]
    public class SunConfig
    {
        public ConfigValueEntry Sun = new ConfigValueEntry
        {
            Header = "Sun Configuration",
            Name = "Override Sun",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            PadStyle = PadStyle.Left
        };


        public ConfigValueEntry SunOverlayCount = new ConfigValueEntry
        {
            Name = "Overlay Count",
            MaxValue = 4,
            MinValue = 1,
            Incremental = 1,
            Value = 4,
            //ValueType = typeof(int),
            PadStyle = PadStyle.Left
        };
        //public ConfigValueEntry AutomateSun = new ConfigValueEntry
        //{
        //    Name = "Automate Sun",
        //    MaxValue = true,
        //    MinValue = false,
        //    Value = true,
        //    //ValueType = typeof(bool),
        //    PadStyle = PadStyle.Left
        //};
        #region "FIRSTOVERLAY"
        public ConfigValueEntry OverlayOne_primary_color = new ConfigValueEntry
        {
            Header = "First Overlay",
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.GhostWhite,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row1"
        };
        public ConfigValueEntry OverlayOne_secondary_color = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Green,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row1"
        };
        public ConfigValueEntry OverlayOne_primary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row2",
        };
        public ConfigValueEntry OverlayOne_secondary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row2",
        };

        public ConfigValueEntry OverlayOne_primary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row3",
        };

        public ConfigValueEntry OverlayOne_secondary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row3",
        };
        #endregion

        #region "secondary"
        public ConfigValueEntry OverlayTwo_primary_color = new ConfigValueEntry
        {
            Header = "Second Overlay",
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.GhostWhite,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row4"
        };
        public ConfigValueEntry OverlayTwo_secondary_color = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Green,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row4"
        };
        public ConfigValueEntry OverlayTwo_primary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row5",
        };
        public ConfigValueEntry OverlayTwo_secondary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row5",
        };

        public ConfigValueEntry OverlayTwo_primary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row6",
        };

        public ConfigValueEntry OverlayTwo_secondary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row6",
        };
        #endregion

        #region "third"
        public ConfigValueEntry OverlayThree_primary_color = new ConfigValueEntry
        {
            Header = "Third Overlay",
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.GhostWhite,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row7"
        };
        public ConfigValueEntry OverlayThree_secondary_color = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Green,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row7"
        };
        public ConfigValueEntry OverlayThree_primary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row8",
        };
        public ConfigValueEntry OverlayThree_secondary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row8",
        };

        public ConfigValueEntry OverlayThree_primary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row9",
        };

        public ConfigValueEntry OverlayThree_secondary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row9",
        };
        #endregion

        #region "fourth"
        public ConfigValueEntry OverlayFour_primary_color = new ConfigValueEntry
        {
            Header = "Fourth Overlay",
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.GhostWhite,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row10"
        };
        public ConfigValueEntry OverlayFour_secondary_color = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Green,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "row10"
        };
        public ConfigValueEntry OverlayFour_primary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row11",
        };
        public ConfigValueEntry OverlayFour_secondary_Horizontal = new ConfigValueEntry
        {
            Name = "H Stretch",
            MaxValue = 2048f,
            MinValue =1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row11",
        };

        public ConfigValueEntry OverlayFour_primary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row12",
        };

        public ConfigValueEntry OverlayFour_secondary_Vertical = new ConfigValueEntry
        {
            Name = "V Stretch",
            MaxValue = 2048f,
            MinValue = 1f,
            Value = 44f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row12",
        };
        #endregion
        public ConfigValueEntry SunDirX = new ConfigValueEntry
        {
            Header = "Direction Control",
            Name = "Ray X",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = -0.49f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "sunraydir"
        };

        public ConfigValueEntry SunDirY = new ConfigValueEntry
        {
            Name = "Ray Y",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = -0.53f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "sunraydir"
        };

        public ConfigValueEntry SunDirZ = new ConfigValueEntry
        {
            Name = "Ray Z",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = 0.68f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "sunraydir"
        };
        public ConfigValueEntry SecondarySunDirX = new ConfigValueEntry
        {
            Name = "Ray X",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = -0.49f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "secondarysunraydir"
        };

        public ConfigValueEntry SecondarySunDirY = new ConfigValueEntry
        {
            Name = "Ray Y",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = -0.53f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "secondarysunraydir"
        };

        public ConfigValueEntry SecondarySunDirZ = new ConfigValueEntry
        {
            Name = "Ray Z",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = 0.68f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "secondarysunraydir"
        };
        public ConfigValueEntry SkyControl = new ConfigValueEntry
        {
            Header = "Sky Control",
            Name = "Enable",
            Value = true,
            //ValueType = typeof(bool),
        };
        public ConfigValueEntry SkyControlSkyName = new ConfigValueEntry
        {
            Name = "Sky",
            MaxValue = Enum.GetValues(typeof(Skyboxes)).Length - 1,
            MinValue = 0,
            Value = Skyboxes.sky_csgo_night02b,
            //ValueType = typeof(Skyboxes),
        };
        //public ConfigValueEntry SunColorOverlayThree = new ConfigValueEntry
        //{
        //    Header = "Third Overlay",
        //    Name = "Overlay Color",
        //    MaxValue = g_Globals.ColorManager.Count,
        //    MinValue = 0,
        //    Value = Color.GhostWhite,
        //    //ValueType = typeof(Color),

        //};
        //public ConfigValueEntry SunColorOverlayThreeHorizontal = new ConfigValueEntry
        //{
        //    Name = "H Stretch",
        //    MaxValue = 2048f,
        //    MinValue = 0f,
        //    Value = 44f,
        //    //ValueType = typeof(float),
        //    IsGrouped = true,
        //    GroupId = "thirdoverlaystretch",
        //};
        //public ConfigValueEntry SunColorOverlayThreeVertical = new ConfigValueEntry
        //{
        //    Name = "V Stretch",
        //    MaxValue = 2048f,
        //    MinValue = 0f,
        //    Value = 44f,
        //    //ValueType = typeof(float),
        //    IsGrouped = true,
        //    GroupId = "thirdoverlaystretch",
        //};
        //public ConfigValueEntry SunColorOverlayFour = new ConfigValueEntry
        //{
        //    Header = "Fourth Overlay",
        //    Name = "Overlay Color",
        //    MaxValue = g_Globals.ColorManager.Count,
        //    MinValue = 0,
        //    Value = Color.GhostWhite,
        //    //ValueType = typeof(Color),

        //};
        //public ConfigValueEntry SunColorOverlayFourHorizontal = new ConfigValueEntry
        //{
        //    Name = "H Stretch",
        //    MaxValue = 2048f,
        //    MinValue = 0f,
        //    Value = 44f,
        //    //ValueType = typeof(float),
        //    IsGrouped = true,
        //    GroupId = "fourthoverlaystretch",
        //};
        //public ConfigValueEntry SunColorOverlayFourVertical = new ConfigValueEntry
        //{
        //    Name = "V Stretch",
        //    MaxValue = 2048f,
        //    MinValue = 0f,
        //    Value = 44f,
        //    //ValueType = typeof(float),
        //    IsGrouped = true,
        //    GroupId = "fourthoverlaystretch",
        //};

        //public ConfigValueEntry SunR = new ConfigValueEntry
        //{
        //    Name = "Sun Color R",
        //    MaxValue = 255,
        //    MinValue = 0,
        //    Incremental = 1,
        //    Value = 255,
        //    //ValueType = typeof(int)
        //};
        //public ConfigValueEntry SunG = new ConfigValueEntry
        //{
        //    Name = "Sun Color G",
        //    MaxValue = 255,
        //    MinValue = 0,
        //    Incremental = 1,
        //    Value = 255,
        //    //ValueType = typeof(int)
        //};
        //public ConfigValueEntry SunB = new ConfigValueEntry
        //{
        //    Name = "Sun Color B",
        //    MaxValue = 255,
        //    MinValue = 0,
        //    Incremental = 1,
        //    Value = 255,
        //    //ValueType = typeof(int)
        //};
        //public ConfigValueEntry SunA = new ConfigValueEntry
        //{
        //    Name = "Sun Color A",
        //    MaxValue = 255,
        //    MinValue = 0,
        //    Incremental = 1,
        //    Value = 255,
        //    //ValueType = typeof(int)
        //};
        //public ConfigValueEntry SunSize = new ConfigValueEntry
        //{
        //    Name = "Sun Color Size",
        //    MaxValue = 255,
        //    MinValue = 0,
        //    Incremental = 1,
        //    Value = 5,
        //    //ValueType = typeof(int)
        //};
    }
}
