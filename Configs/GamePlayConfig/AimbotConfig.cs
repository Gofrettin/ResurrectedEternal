using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.GamePlayConfig
{
    public class AimbotConfig
    {
        public ConfigValueEntry Enable = new ConfigValueEntry()
        {
            Header = "Aimbot Configuration",
            Name = "Aimbot",
            MaxValue = true,
            MinValue = false,
            //ValueType = typeof(bool),
            Value = true,
            IsGrouped = true,
            GroupId = "col1"
        };
        public ConfigValueEntry Key = new ConfigValueEntry
        {
            Name = "Aim Key",
            MaxValue = Enum.GetValues(typeof(VirtualKeys)).Length - 1,
            MinValue = 0,
            Value = VirtualKeys.LeftButton,
            //ValueType = typeof(VirtualKeys),
            IsGrouped = true,
            GroupId = "col1"
        };

        public ConfigValueEntry Angle = new ConfigValueEntry()
        {
            Name = "Angle",
            MaxValue = 180f,
            MinValue = 1f,
            //ValueType = typeof(float),
            Value = 1.5f,
            Incremental = .5f,
            IsGrouped = true,
            GroupId = "col2"
        };
        public ConfigValueEntry AutoAim = new ConfigValueEntry()
        {
            Name = "Auto Aim",
            MaxValue = true,
            MinValue = false,
            //ValueType = typeof(bool),
            Value = false,
            IsGrouped = true,
            GroupId = "col2"
        };
        public ConfigValueEntry Autoshoot = new ConfigValueEntry()
        {
            Name = "Auto Shoot",
            MaxValue = true,
            MinValue = false,
            //ValueType = typeof(bool),
            Value = false,
            IsGrouped = true,
            GroupId = "col3"
        };
        public ConfigValueEntry NonStick = new ConfigValueEntry()
        {
            Name = "Dont Stick",
            MaxValue = true,
            MinValue = false,
            //ValueType = typeof(bool),
            Value = true,
            IsGrouped = true,
            GroupId = "col4"
        };


        public ConfigValueEntry AimAt = new ConfigValueEntry
        {
            Name = "Target",
            MaxValue = Enum.GetValues(typeof(TargetType)).Length - 1,
            MinValue = 0,
            Value = TargetType.Enemy,
            //ValueType = typeof(TargetType),
            IsGrouped = true,
            GroupId = "col4"
        };

        public ConfigValueEntry Priority = new ConfigValueEntry
        {
            Name = "Priority",
            MaxValue = Enum.GetValues(typeof(AimPriority)).Length - 1,
            MinValue = 0,
            Value = AimPriority.ClosestToCrosshair,
            //ValueType = typeof(AimPriority),
            IsGrouped = true,
            GroupId = "col5"
        };
        public ConfigValueEntry OnGround = new ConfigValueEntry
        {
            Name = "Check On Ground",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col5"
        };
        public ConfigValueEntry ChickenAimbot = new ConfigValueEntry
        {
            Name = "Chicken Aimbot",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col6"
        };
        public ConfigValueEntry Aimpoint = new ConfigValueEntry
        {
            Name = "Aim Point",
            MaxValue = Enum.GetValues(typeof(AimPoint)).Length - 1,
            MinValue = 0,
            Value = AimPoint.Head,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col6"
        };

        public ConfigValueEntry accuracypenalty = new ConfigValueEntry
        {
            Name = "Accuracy Penalty",
            Value = false,
            IsGrouped = true,
            GroupId = "col_wb"
            //ValueType = typeof(bool),
        };
        public ConfigValueEntry Wallbang = new ConfigValueEntry
        {
            Name = "Pseudo Wallbang",
            Value = true,
            IsGrouped = true,
            GroupId = "col_wb"
            //ValueType = typeof(bool),
        };
        public ConfigValueEntry VisibleCheckOption = new ConfigValueEntry()
        {
            Header = "Visible Check",
            Name = "Check Type",
            Value = VisibleCheck.SlowTrace,
            MinValue = 0,
            MaxValue = Enum.GetValues(typeof(VisibleCheck)).Length - 1,
        };
        public ConfigValueEntry Smooth = new ConfigValueEntry()
        {
            Header = "Smooth Options",
            Name = "Smooth",
            MaxValue = 100f,
            MinValue = 0f,
            //ValueType = typeof(float),
            Value = 1f,
            IsGrouped = true,
            GroupId = "col99"
        };
        public ConfigValueEntry SmoothTimeMultiplier = new ConfigValueEntry()
        {
            Name = "Smooth Multiplier",
            MaxValue = 100f,
            MinValue = 1f,
            //ValueType = typeof(float),
            Incremental = .5f,
            Value = 30f,
            IsGrouped = true,
            GroupId = "col99"
        };
        public ConfigValueEntry SmoothOffset = new ConfigValueEntry()
        {
            Name = "Smooth Offset",
            MaxValue = 1000.0f,
            MinValue = 1.0f,
            Incremental = 1f,
            //ValueType = typeof(float),
            Value = 20.0f,
            IsGrouped = true,
            GroupId = "col101"
        };
        public ConfigValueEntry SmoothType = new ConfigValueEntry()
        {
            Name = "Smooth Type",
            MaxValue = Enum.GetValues(typeof(SmoothType)).Length - 1,
            MinValue = 0,
            //ValueType = typeof(SmoothType),
            Value = ResurrectedEternal.SmoothType.SmoothStep,
            IsGrouped = true,
            GroupId = "col101"
        };



        public ConfigValueEntry RCS = new ConfigValueEntry()
        {
            Header = "Recoil Control Options",
            Name = "Enable RCS",
            MaxValue = true,
            MinValue = false,
            //ValueType = typeof(bool),
            Value = true,
            IsGrouped = true,
            GroupId = "col22"
        };

        public ConfigValueEntry EnableRCS = new ConfigValueEntry
        {
            Name = "Standalone RCS",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "col22"
        };


        public ConfigValueEntry Pistol = new ConfigValueEntry
        {
            Name = "Pistol RCS",
            MaxValue = true,
            MinValue = false,
            Value = true,
            IsGrouped = true,
            GroupId = "col33"
        };
        public ConfigValueEntry Sniper = new ConfigValueEntry
        {
            Name = "Sniper RCS",
            MaxValue = true,
            MinValue = false,
            Value = true
            //ValueType = typeof(bool)
                        ,
            IsGrouped = true,
            GroupId = "col33"
        };
        public ConfigValueEntry Compensation = new ConfigValueEntry
        {
            Name = "Compensation",
            MaxValue = 3.0f,
            MinValue = 0.1f,
            Incremental = 0.01f,
            Value = 2f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "col44"
        };
        public ConfigValueEntry Shots = new ConfigValueEntry
        {
            Name = "Min Shots",
            MaxValue = 30,
            MinValue = 0,
            Value = 1,
            IsGrouped = true,
            GroupId = "col44"
        };

        public ConfigValueEntry RCSSmoothType = new ConfigValueEntry
        {
            Header = "RCS Smoothing",
            Name = "Smooth Type",
            MaxValue = Enum.GetValues(typeof(ResurrectedEternal.RCSSmoothType)).Length - 1,
            MinValue = 0,
            //ValueType = typeof(SmoothType),
            Value = ResurrectedEternal.RCSSmoothType.Adaptive,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "rcsSmoothcol2"
        };
        /// <summary>
        /// Defines the max we should increment to
        /// </summary>
        public ConfigValueEntry RCSSmoothFactor = new ConfigValueEntry
        {
            Name = "Factor",
            MaxValue = 3f,
            MinValue = 0.1f,
            Incremental = .01f,
            Value = 1f,
            //ValueType = typeof(bool)

            IsGrouped = true,
            GroupId = "rcsSmoothcol2"
        };
        public ConfigValueEntry RCSSmoothOffset = new ConfigValueEntry
        {
            Name = "Offset",
            MaxValue = 30f,
            MinValue = 1f,
            Value = 18f,
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry trig_Enable = new ConfigValueEntry
        {
            Header = "Trigger Control",
            Name = "Enabled",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row1"
        };

        public ConfigValueEntry trig_KeyEnable = new ConfigValueEntry
        {
            Name = "Use Key",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row1"
        };

        public ConfigValueEntry trig_Key = new ConfigValueEntry
        {
            Name = "Key",
            MaxValue = Enum.GetValues(typeof(VirtualKeys)).Length - 1,
            MinValue = 0,
            Value = VirtualKeys.V,
            //ValueType = typeof(VirtualKeys),
            IsGrouped = true,
            GroupId = "row2"
        };

        //public ConfigValueEntry trig_Delay = new ConfigValueEntry
        //{
        //    Name = "Delay",
        //    MaxValue = 1000.0f,
        //    MinValue = 10.0f,
        //    Value = 50.0f,
        //    //ValueType = typeof(float),
        //    IsGrouped = true,
        //    GroupId = "row2"
        //};
        public ConfigValueEntry trig_Snipers = new ConfigValueEntry
        {
            Name = "Snipers",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row2"
        };
        public ConfigValueEntry trig_Pistols = new ConfigValueEntry
        {
            Name = "Pistols",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row3"
        };
        public ConfigValueEntry trig_Rifle = new ConfigValueEntry
        {
            Name = "Rifles",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row3"
        };
    }
}
