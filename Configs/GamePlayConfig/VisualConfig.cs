using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.GamePlayConfig
{
    public class VisualConfig
    {
        public ConfigValueEntry Enable = new ConfigValueEntry
        {
            Header = "Visual Configuration",
            Name = "Visuals",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),

        };
        public ConfigValueEntry DrawBox = new ConfigValueEntry
        {
            Name = "Box",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col0"
        };
        public ConfigValueEntry Type = new ConfigValueEntry
        {
            Name = "Type",
            MaxValue = Enum.GetValues(typeof(TargetType)).Length - 1,
            MinValue = 0,
            Value = TargetType.Enemy,
            //ValueType = typeof(TargetType),
            IsGrouped = true,
            GroupId = "col0"
        };
        public ConfigValueEntry DrawName = new ConfigValueEntry
        {
            Name = "Name",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col1"
        };

        public ConfigValueEntry DrawWeapon = new ConfigValueEntry
        {
            Name = "Weapon",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col1"
        };
        public ConfigValueEntry DrawAllWeapon = new ConfigValueEntry
        {
            Name = "All Weapons",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col2"
        };
        public ConfigValueEntry DrawDistance = new ConfigValueEntry
        {
            Name = "Distance",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col2"
        };

        public ConfigValueEntry DrawDefuser = new ConfigValueEntry
        {
            Name = "Has Defuser",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col3"
        };

        public ConfigValueEntry DrawDefusing = new ConfigValueEntry
        {
            Name = "Is Defusing",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col3"
        };

        public ConfigValueEntry DrawSneaking = new ConfigValueEntry
        {
            Name = "Sneaking",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col4"
        };
        public ConfigValueEntry DrawScoped = new ConfigValueEntry
        {
            Name = "Scoped",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col4"
        };
        public ConfigValueEntry DrawHealth = new ConfigValueEntry
        {
            Name = "Health",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col5"
        };
        public ConfigValueEntry DrawArmor = new ConfigValueEntry
        {
            Name = "Armor",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col5"
        };
        public ConfigValueEntry DrawHead = new ConfigValueEntry
        {
            Name = "Head",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col6"
        };

        public ConfigValueEntry HeadSize = new ConfigValueEntry
        {
            Name = "Size",
            MaxValue = 20f,
            MinValue = 1f,
            Value = 6f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "col6"
        };


        public ConfigValueEntry DrawDroppedWeapons = new ConfigValueEntry
        {
            Header = "Drop Visuals",
            Name = "Weapons",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col7"
        };
        public ConfigValueEntry DroppedGrenades = new ConfigValueEntry
        {
            Name = "Grenades",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col7"
        };
        public ConfigValueEntry DrawGrenades = new ConfigValueEntry
        {
            Name = "Throwables",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col8"
        };

        public ConfigValueEntry DrawBomb = new ConfigValueEntry
        {
            Name = "Bomb",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col8"
        };
        public ConfigValueEntry GrenadeTrajectory = new ConfigValueEntry
        {
            Name = "Grenade Trajectory",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            ConvarName = "cl_grenadepreview",
            IsGrouped = true,
            GroupId = "grdtjctry2"
        };
        public ConfigValueEntry ShowRecoil = new ConfigValueEntry
        {
            Name = "Crosshair Recoil",
            Value = false,
            IsGrouped = true,
            GroupId = "grdtjctry2"
        };
        public ConfigValueEntry WeaponSpread = new ConfigValueEntry
        {
            Name = "Weapon Spread",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            ConvarName = "weapon_debug_spread_show",
            IsGrouped = true,
            GroupId = "thelastdrawgrp"
        };
        public ConfigValueEntry DrawEverything = new ConfigValueEntry
        {
            Name = "Everything",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "thelastdrawgrp"
        };

        public ConfigValueEntry RenderDistance = new ConfigValueEntry
        {
            Header = "Extra Visuals",
            Name = "Entity Render Distance",
            MaxValue = 10000f,
            MinValue = 100f,
            Value = 1500f,
            Incremental = 100f,
            //ValueType = typeof(float),
        };
        public ConfigValueEntry DrawChicken = new ConfigValueEntry
        {
            Name = "Chicks",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "gridchick"
        };
        public ConfigValueEntry ShowSpectators = new ConfigValueEntry
        {
            Name = "Spectators",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "gridchick"
        };
        public ConfigValueEntry ShowImmunity = new ConfigValueEntry
        {
            Name = "Immunity",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "maxmaxim"
        };
        public ConfigValueEntry FlashWarning = new ConfigValueEntry
        {
            Name = "Flash Warning",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "maxmaxim"
        };
        public ConfigValueEntry skltsn = new ConfigValueEntry
        {
            Name = "Skeletons",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "enable_skeleton_draw",
            IsGrouped = true,
            GroupId = "newshit1"
        };
        //public ConfigValueEntry showinferno = new ConfigValueEntry
        //{
        //    Name = "Show Infernos",
        //    MaxValue = 1,
        //    MinValue = 0,
        //    Value = 1,
        //    //ValueType = typeof(bool),
        //    IsGrouped = true,
        //    GroupId = "newshit1",
        //    ConvarName = "inferno_debug"
        //};
        public ConfigValueEntry renderboxes = new ConfigValueEntry
        {
            Name = "Renderboxes",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "newshit1",
            ConvarName = "r_drawrenderboxes"
        };
        public ConfigValueEntry mat_normals = new ConfigValueEntry
        {
            Name = "Normals",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "mat_normals",
            IsGrouped = true,
            GroupId = "newshit2"
        };
        public ConfigValueEntry modelstats = new ConfigValueEntry
        {
            Name = "Overlay",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "r_drawmodelstatsoverlay",
            IsGrouped = true,
            GroupId = "newshit2"
        };
        public ConfigValueEntry NamePos = new ConfigValueEntry
        {
            Header = "Position Control",
            Name = "Name",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Top,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos1"
        };
        public ConfigValueEntry WeaponPos = new ConfigValueEntry
        {
            Name = "Weapon",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Right,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos1"
        };
        public ConfigValueEntry DistancePos = new ConfigValueEntry
        {
            Name = "Distance",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Bottom,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos2"
        };
        public ConfigValueEntry DefuserPos = new ConfigValueEntry
        {
            Name = "Defuser",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Left,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos2"
        };
        public ConfigValueEntry DefusingPos = new ConfigValueEntry
        {
            Name = "Defusing",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Left,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos3"
        };
        public ConfigValueEntry SneakingPos = new ConfigValueEntry
        {
            Name = "Sneak",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Left,
            //ValueType = typeof(DrawPosition),
            IsGrouped = true,
            GroupId = "pos3"
        };
        public ConfigValueEntry ScopedPos = new ConfigValueEntry
        {
            Name = "Is Scoped",
            MaxValue = Enum.GetValues(typeof(DrawPosition)).Length - 1,
            MinValue = 0,
            Value = DrawPosition.Left,
            //ValueType = typeof(DrawPosition),
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry LineThickness = new ConfigValueEntry
        {
            Header = "Extra",
            Name = "Line Thickness",
            MaxValue = 20f,
            MinValue = 0.1f,
            Value = 1.5f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry Font = new ConfigValueEntry
        {
            Name = "Font",
            MaxValue = g_Globals.FontManager.Fonts.Length - 1,
            MinValue = 0,
            Value = "Calibri",
            Incremental = 0.1f,
            //ValueType = typeof(string[]),
            IsGrouped = true,
            GroupId = "fontcol",
        };
        public ConfigValueEntry FontStyle = new ConfigValueEntry
        {
            Name = "Font Style",
            MaxValue = Enum.GetValues(typeof(SharpDX.DirectWrite.FontStyle)).Length - 1,
            MinValue = 0,
            Value = SharpDX.DirectWrite.FontStyle.Normal,
            //ValueType = typeof(SharpDX.DirectWrite.FontStyle),
            IsGrouped = true,
            GroupId = "fontcol",
        };
        public ConfigValueEntry FontWeight = new ConfigValueEntry
        {
            Name = "Font Weight",
            MaxValue = Enum.GetValues(typeof(SharpDX.DirectWrite.FontWeight)).Length - 1,
            MinValue = 0,
            Value = SharpDX.DirectWrite.FontWeight.Bold,
            //ValueType = typeof(SharpDX.DirectWrite.FontWeight),
            IsGrouped = true,
            GroupId ="lastcol"
        };
        public ConfigValueEntry FontSize = new ConfigValueEntry
        {
            Name = "Font Size",
            MaxValue = 30,
            MinValue = 1,
            Value = 14,
            Incremental = 1,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "lastcol",
        };
        public ConfigValueEntry WaterMark = new ConfigValueEntry
        {
            Name = "Watermark",
            MaxValue = true,
            MinValue = false,
            Value = true
        };
        //ESP
        //public bool bESP = true;
        //public bool bDrawBox = true;
        //public TargetType ESPType = TargetType.All;
        //public bool bDrawName = true;
        //public bool bDrawWeapon = true;
        //public bool bDrawHP = true;
        //public bool bDrawArmor = true;
        //public bool bDrawHead = true;

        //public bool bDrawDistance = true;
        //public bool bDrawImmunity = true;
        //public bool bGlow = true;
        //public int iGlowTicks = 50;


        //public ConfigValueEntry SkeletonDraw = new ConfigValueEntry
        //{
        //    Name = "Enable Draw Skeleton",
        //    MaxValue = true,
        //    MinValue = false,
        //    Value = true,
        //    //ValueType = typeof(bool),
        //    ConvarName = "enable_skeleton_draw"
        //};
        //public ConfigValueEntry SkeletonDrawSecondary = new ConfigValueEntry
        //{
        //    Name = "Enable Draw Skeleton",
        //    MaxValue = true,
        //    MinValue = false,
        //    Value = true,
        //    //ValueType = typeof(bool),
        //    ConvarName = "enable_skeleton_draw"
        //};
        //public ConfigValueEntry JiggleBone = new ConfigValueEntry
        //{
        //    Name = "Enable Jiggle Bone",
        //    MaxValue = true,
        //    MinValue = false,
        //    Value = true,
        //    //ValueType = typeof(bool),
        //    ConvarName = "cl_jiggle_bone_debug"
        //};
    }
}
