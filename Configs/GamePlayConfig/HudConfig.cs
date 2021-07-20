using RRFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.GamePlayConfig
{

    public class HudConfig
    {
        public ConfigValueEntry bEnableRadar = new ConfigValueEntry()
        {
            Header = "Radar",
            Name = "Enable",
            Value = true
            ,IsGrouped = true,
            GroupId = "col_1"
        };

        public ConfigValueEntry fRadarSize = new ConfigValueEntry()
        {
            Name = "Size",
            Value = 100.0f,
            MinValue = 1f,
            MaxValue = float.MaxValue,
            Incremental = 1f,
            IsGrouped = true,
            GroupId = "col_1"

        };
        public ConfigValueEntry xRadarPos = new ConfigValueEntry()
        {
            Name = "Pos X",
            Value = 125.0f,
            MinValue = 1f,
            MaxValue = float.MaxValue,
            Incremental = 1f,
            IsGrouped = true,
            GroupId = "col_2"

        };
        public ConfigValueEntry yRadarPos = new ConfigValueEntry()
        {
            Name = "Pos Y",
            Value = 125.0f,
            MinValue = 1f,
            MaxValue = float.MaxValue,
            Incremental = 1f,
            IsGrouped = true,
            GroupId = "col_2"

        };
        public ConfigValueEntry rsRadarStyle = new ConfigValueEntry()
        {
            //ValueType = typeof(SmoothType),
            
            Name = "Style",
            MinValue = 0,
            MaxValue = Enum.GetValues(typeof(RadarStyle)).Length - 1,
            Value = RadarStyle.Circular,
        };
        public ConfigValueEntry bCrosshair = new ConfigValueEntry()
        {
            Header = "Crosshair Settings",
            Value = true,
            Name = "Crosshair",

        };
        public ConfigValueEntry fCrosshairLength = new ConfigValueEntry()
        {
            
            //ValueType = typeof(SmoothType),
            Value = 2f,
            Name = "Length",
            MinValue = 1f,
            MaxValue = float.MaxValue,
            Incremental = 0.1f,
            IsGrouped = true,
            GroupId = "col_3"
        };
        public ConfigValueEntry fCrosshairWidth = new ConfigValueEntry()
        {
            Header = "Crosshair Settings",
            Value = 2f,
            Name = "Width",
            MinValue = 1f,
            MaxValue = float.MaxValue,
            Incremental = 0.1f,
            IsGrouped = true,
            GroupId = "col_3"
        };

        public ConfigValueEntry bCustomHud = new ConfigValueEntry()
        {
            Header = "Custom Hud",
            Value = false,
            Name = "Enable",
            ConvarName = "cl_draw_only_deathnotices"
        };
        public ConfigValueEntry bDrawFov = new ConfigValueEntry()
        {
            Value = false,
            Name = "Draw Fov",
        };
        public ConfigValueEntry FOVChanger = new ConfigValueEntry
        {
            Header = "Field of View Control",
            Name = "Enabled",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row4"
        };
        public ConfigValueEntry FOVAmount = new ConfigValueEntry
        {
            Name = "Amount",
            MaxValue = 130f,
            MinValue = 1f,
            Value = 130f,
            //ValueType = typeof(float),
            ConvarName = "fov_cs_debug",
            ConvarRequire = "FOVChanger",
            DefaultValue = 0f,
            IsGrouped = true,
            GroupId = "row4"
        };
        public ConfigValueEntry ViewmodelChanger = new ConfigValueEntry
        {
            Header = "Viewmodel Control",
            Name = "Enabled",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row5"
        };

        public ConfigValueEntry ViewmodelFov = new ConfigValueEntry
        {
            Name = "Amount",
            MaxValue = 130f,
            MinValue = 0f,
            Value = 120f,
            //ValueType = typeof(float),
            ConvarName = "viewmodel_fov",
            ConvarRequire = "ViewmodelChanger",
            IsGrouped = true,
            GroupId = "row5"
        };

        public ConfigValueEntry ViewmodelFovOffsetX = new ConfigValueEntry
        {
            Name = "Offset X",
            MaxValue = 40f,
            MinValue = -40f,
            Value = 4f,
            Incremental = .1f,
            //ValueType = typeof(float),
            ConvarName = "viewmodel_offset_x",
            ConvarRequire = "ViewmodelChanger",
            IsGrouped = true,
            GroupId = "row6"
        };
        public ConfigValueEntry ViewmodelFovOffsetY = new ConfigValueEntry
        {
            Name = "Offset Y",
            MaxValue = 40f,
            MinValue = -40f,
            Value = 0f,
            Incremental = .1f,
            //ValueType = typeof(float),
            ConvarName = "viewmodel_offset_y",
            ConvarRequire = "ViewmodelChanger",
            IsGrouped = true,
            GroupId = "row6"
        };
        public ConfigValueEntry ViewmodelFovOffsetZ = new ConfigValueEntry
        {
            Name = "Offset Z",
            MaxValue = 40f,
            MinValue = -40f,
            Value = 4f,
            Incremental = .1f,
            //ValueType = typeof(float),
            ConvarName = "viewmodel_offset_z",
            ConvarRequire = "ViewmodelChanger",
            IsGrouped = true,
            GroupId = "row6"
        };
        public ConfigValueEntry ScreenOffsetX = new ConfigValueEntry
        {
            Header = "Screen Offset",
            Name = "Offset X",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Value = 0f,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "screen_offset"
        };
        public ConfigValueEntry ScreenOffsetY = new ConfigValueEntry
        {
            Header = "Screen Offset",
            Name = "Offset Y",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Value = 0f,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "screen_offset"
        };
        //public ConfigValueEntry fCustomHudSize = new ConfigValueEntry()
        //{

        //}
    }
}
