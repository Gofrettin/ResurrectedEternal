using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs
{
    public class OtherConfig
    {

        public ConfigValueEntry cTerroristNormalColor = new ConfigValueEntry
        {
            Header = "Bounding Box Color",
            Name = "Normal T",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Yellow,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row1"
        };
        public ConfigValueEntry cTerroristVisibleColor = new ConfigValueEntry
        {
            Name = "Visible T",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Red,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row1"
        };
        public ConfigValueEntry cCTerroristNormalColor = new ConfigValueEntry
        {
            Name = "Normal CT",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.RoyalBlue,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row2"
        };
        public ConfigValueEntry cCTerroristVisibleColor = new ConfigValueEntry
        {
            Name = "Visible CT",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.LimeGreen,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row2"
        };
        public ConfigValueEntry cBoundingBoxOutlineColor = new ConfigValueEntry
        {
            Name = "Outline",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Black,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row3"
        };

        public ConfigValueEntry cImmunityColor = new ConfigValueEntry
        {
            Name = "Immunity",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.White,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "bb_row3"
        };
        public ConfigValueEntry cNeonTerroristNormalColor = new ConfigValueEntry
        {
            Header = "Neon Colors",
            Name = "Normal T",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Yellow,
            IsGrouped = true,
            GroupId = "neon_bb_row1"
            //ValueType = typeof(Color),
        };
        public ConfigValueEntry cNeonTerroristVisibleColor = new ConfigValueEntry
        {
            Name = "Visible T",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Red,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "neon_bb_row1"
        };
        public ConfigValueEntry cNeonCTerroristNormalColor = new ConfigValueEntry
        {
            Name = "Normal CT",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.RoyalBlue,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "neon_bb_row2"
        };
        public ConfigValueEntry cNeonCTerroristVisibleColor = new ConfigValueEntry
        {
            Name = "Visible CT",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.LimeGreen,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "neon_bb_row2"
        };
        //#############################################
        // DROP COLORS START
        //#############################################


        public ConfigValueEntry cWeaponColor = new ConfigValueEntry
        {
            Header = "Drop Colors",
            Name = "Weapon",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Gold,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow1"
        };
        public ConfigValueEntry cDefuseKitColor = new ConfigValueEntry
        {
            Name = "Defuse Kit",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.DeepPink,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow1"
        };
        public ConfigValueEntry cGrenadeColor = new ConfigValueEntry
        {
            Name = "Grenade",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Sienna,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow2"
        };
        public ConfigValueEntry cBombColor = new ConfigValueEntry
        {
            Name = "Bomb",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Crimson,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow2"
        };
        public ConfigValueEntry cProjectileColor = new ConfigValueEntry
        {
            Name = "Projectile",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.DarkMagenta,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow3"
        };
        public ConfigValueEntry cFontColor = new ConfigValueEntry
        {
            Name = "Font",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.GhostWhite,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow3"
        };
        public ConfigValueEntry cChickenColor = new ConfigValueEntry
        {
            Name = "Chicken",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.DarkMagenta,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow4"
        };
        public ConfigValueEntry cViewmodelColor = new ConfigValueEntry
        {
            Name = "Viemodel",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Cyan,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow4"
        };
        public ConfigValueEntry m_cRadarBaseColor = new ConfigValueEntry
        {
            Name = "Radar Base",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Black,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow5"
        };
        public ConfigValueEntry m_cRadarCrossColor = new ConfigValueEntry
        {
            Name = "Radar Cross",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.RoyalBlue,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow5"
        };
        public ConfigValueEntry m_cCrossBase = new ConfigValueEntry
        {
            Name = "Crosshair Base",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Black,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow6"
        };
        public ConfigValueEntry m_cCrossOutline = new ConfigValueEntry
        {
            Name = "Crosshair Outline",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Red,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "droprow6"
        };
        public ConfigValueEntry cHeadCircleColor = new ConfigValueEntry
        {
            Name = "Head Circle Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = Color.Pink,
            //ValueType = typeof(Color),
            PadStyle = PadStyle.Left
        };



        public ConfigValueEntry bEnableFakeLag = new ConfigValueEntry
        {
            Header = "Fake Lag Options",
            Name = "Enabled",
            Value = false,
            IsGrouped = true,
            GroupId = "fl_0"
            
        };
        public ConfigValueEntry bEnableRandomize = new ConfigValueEntry
        {
            Name = "Randomize Lag",
            Value = false,
            IsGrouped = true,
            GroupId = "fl_0"

        };

        public ConfigValueEntry bUseLagKey = new ConfigValueEntry()
        {
            Name = "Use Lag Key",
            Value = false,
            IsGrouped = true,
            GroupId = "fl_444",


        };
        public ConfigValueEntry vkLagey = new ConfigValueEntry()
        {
            Name = "Use Lag Key",
            IsGrouped = true,
            GroupId = "fl_444",
            MaxValue = Enum.GetValues(typeof(VirtualKeys)).Length - 1,
            MinValue = 0,
            Value = VirtualKeys.LeftButton
        };
        public ConfigValueEntry FakeLagMin = new ConfigValueEntry()
        {
            Name = "Fake Lag Min",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 6,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_1"
        };
        public ConfigValueEntry FakeLagMax = new ConfigValueEntry()
        {
            Name = "Fake Lag Max",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 12,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_1"
        };
        public ConfigValueEntry FakeJitterMin = new ConfigValueEntry()
        {
            Name = "Fake Jitter Min",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 50,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_2"
        };
        public ConfigValueEntry FakeJitterMax = new ConfigValueEntry()
        {
            Name = "Fake Jitter Max",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 100,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_2"
        };
        public ConfigValueEntry FakeLossMin = new ConfigValueEntry()
        {
            Name = "Fake Loss Min",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 10,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_3"
        };
        public ConfigValueEntry FakeLossMax = new ConfigValueEntry()
        {
            Name = "Fake Loss Max",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 10,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_3"
        };

        public ConfigValueEntry RandomMin = new ConfigValueEntry()
        {
            Header = "Randomize Milliseconds",
            Name = "Random Min",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 50,
            Incremental = 5,
            IsGrouped = true,
            GroupId = "fl_4"
        };
        public ConfigValueEntry RandomMax = new ConfigValueEntry()
        {
            Name = "Random Max",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 250,
            Incremental = 5,
            IsGrouped = true,
            GroupId = "fl_4"
        };
        public ConfigValueEntry EnableDropPackets = new ConfigValueEntry()
        {
            Header = "Drop Packets",
            Name = "Drop Packets",
            Value = false,
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry DropPacketMin = new ConfigValueEntry()
        {
            Name = "Drop Packets Min",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 10,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_5"
        };
        public ConfigValueEntry DropPacketMax = new ConfigValueEntry()
        {
            Name = "Drop Packets Max",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            //ValueType = typeof(float),
            Value = 50,
            Incremental = 1,
            IsGrouped = true,
            GroupId = "fl_5"
        };
        public ConfigValueEntry PacketIntervalMin = new ConfigValueEntry()
        {
            Name = "Packet Interval",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            //ValueType = typeof(float),
            Value = 125f,
            Incremental = 5f,
            IsGrouped = true,
            GroupId = "fl_6"
        };
        public ConfigValueEntry PacketIntervalMax = new ConfigValueEntry()
        {
            Name = "Packet Interval",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            //ValueType = typeof(float),
            Value = 250f,
            Incremental = 5f,
            IsGrouped = true,
            GroupId = "fl_6"
        };



    }
}
