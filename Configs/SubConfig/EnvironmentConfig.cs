using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.SubConfig
{
    public class EnvironmentConfig
    {
        public ConfigValueEntry OverrideCascade = new ConfigValueEntry
        {
            Header = "Cascade Light Control",
            Name = "Override Cascade Light",
            MaxValue = true,
            MinValue = false,
            Value = false
            
        };
        public ConfigValueEntry Enable = new ConfigValueEntry
        {
            Name = "Cascade Light",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row1"
        };
        public ConfigValueEntry envEnable = new ConfigValueEntry
        {
            Name = "Environemt Angles",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row1"
        };
        public ConfigValueEntry ColorScale = new ConfigValueEntry
        {
            Name = "Color Scale",
            MaxValue = int.MaxValue,
            MinValue = int.MinValue,
            Incremental = 1,
            Value = 600,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "row2"
        };
        public ConfigValueEntry ShadowDist = new ConfigValueEntry
        {
            Name = "Shadow Distance",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 1f,
            Value = 400f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row2"
        };

        public ConfigValueEntry envShadowDirX = new ConfigValueEntry
        {
            Name = "Env Dir X",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0.47f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "envdircol"
        };
        public ConfigValueEntry envShadowDirY = new ConfigValueEntry
        {
            Name = "Env Dir Y",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0.43f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "envdircol"
        };
        public ConfigValueEntry envShadowDirZ = new ConfigValueEntry
        {
            Name = "Env Dir Z",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = -0.76f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "envdircol"
        };
        public ConfigValueEntry ShadowDirX = new ConfigValueEntry
        {
            Name = "Dir X",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0.47f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "shwircol"
        };
        public ConfigValueEntry ShadowDirY = new ConfigValueEntry
        {
            Name = "Dir Y",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0.43f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "shwircol"
        };
        public ConfigValueEntry ShadowDirZ = new ConfigValueEntry
        {
            Name = "Dir Z",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = -0.76f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "shwircol"
        };
        public ConfigValueEntry Color = new ConfigValueEntry
        {
            Name = "Cascade Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = new SharpDX.Color(254,230,197,0),
            //ValueType = typeof(Color),
            PadStyle = PadStyle.Right
        };

        //WIND
        public ConfigValueEntry WindOverride = new ConfigValueEntry
        {
            Header = "Wind Control",
            Name = "Override",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry MinWind = new ConfigValueEntry
        {
            Name = "Min Wind",
            MaxValue = int.MaxValue,
            MinValue = 0,
            Incremental = 1,
            Value = 2,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "minmaxwind"
        };
        public ConfigValueEntry MaxWind = new ConfigValueEntry
        {
            Name = "Max Wind",
            MaxValue = int.MaxValue,
            MinValue = 0,
            Incremental = 1,
            Value = 4,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "minmaxwind"
        };

        public ConfigValueEntry MinGust = new ConfigValueEntry
        {
            Name = "Min Gust",
            MaxValue = int.MaxValue,
            MinValue = 0,
            Incremental = 1,
            Value = 3,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "minmaxgust"
        };
        public ConfigValueEntry MaxGust = new ConfigValueEntry
        {
            Name = "Max Gust",
            MaxValue = int.MaxValue,
            MinValue = 0,
            Incremental = 1,
            Value = 6,
            //ValueType = typeof(int),
                        IsGrouped = true,
            GroupId = "minmaxgust"
        };
        public ConfigValueEntry MinGustDelay = new ConfigValueEntry
        {
            Name = "Min Gust Delay",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Incremental = 0.1f,
            Value = 10.0f,
            //ValueType = typeof(float),
                        IsGrouped = true,
            GroupId = "minmaxgustdelay"
        };
        public ConfigValueEntry MaxGustDelay = new ConfigValueEntry
        {
            Name = "Max Gust Delay",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Incremental = 0.1f,
            Value = 20.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "minmaxgustdelay"
        };
        public ConfigValueEntry GustDuration = new ConfigValueEntry
        {
            Name = "Gust Duration",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Incremental = 0.1f,
            Value = 5.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "lastcol"
        };
        public ConfigValueEntry InitialWindSpeed = new ConfigValueEntry
        {
            Name = "Wind Speed",
            MaxValue = float.MaxValue,
            MinValue = 0f,
            Incremental = 1f,
            Value = 1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "lastcol"
        };
        //FOG

        public ConfigValueEntry fog_override = new ConfigValueEntry()
        {

            Name = "fog_override",
            Value = true,
            HiddenFromMenu = true,
            ConvarName = "fog_override"
        };

        public ConfigValueEntry FogEnable = new ConfigValueEntry
        {
            Header = "Fog Control",
            Name = "Enable",
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = false,
            PadStyle = PadStyle.Left,
        };

        public ConfigValueEntry FogOverride = new ConfigValueEntry
        {
            Name = "Fog",
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "column1",
            ConvarName = "fog_enable"
        };

        public ConfigValueEntry Blend = new ConfigValueEntry
        {
            Name = "Blend",
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "column1"
        };


        public ConfigValueEntry FogStart = new ConfigValueEntry
        {
            Name = "Start",
            MaxValue = float.MaxValue,
            MinValue = 0,
            Incremental = .5f,
            Value = 5f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "fogstart"
        };
        public ConfigValueEntry FogEnd = new ConfigValueEntry
        {
            Name = "End",
            MaxValue = float.MaxValue,
            MinValue = 1,
            Incremental = .5f,
            Value = 50f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "fogstart",
        };
        public ConfigValueEntry FogDensity = new ConfigValueEntry
        {
            Name = "Density",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0.5f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "density",
        };
        public ConfigValueEntry HDRColor = new ConfigValueEntry
        {
            Name = "HDR Scale",
            MaxValue = 10f,
            MinValue = 0f,
            Incremental = .01f,
            Value = 1.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "density",
        };
        public ConfigValueEntry FogLerpTime = new ConfigValueEntry
        {
            Name = "Duration",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = .5f,
            Value = 0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "column6"
        };

        public ConfigValueEntry ZoomFogScale = new ConfigValueEntry
        {
            Name = "Zoom Scale",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = .5f,
            Value = 1.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "column6"
        };

        public ConfigValueEntry FogDirX = new ConfigValueEntry
        {
            Name = "X",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "fogdir"
        };
        public ConfigValueEntry FogDirY = new ConfigValueEntry
        {
            Name = "Y",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value =  0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "fogdir"
        };
        public ConfigValueEntry FogDirZ = new ConfigValueEntry
        {
            Name = "Z",
            MaxValue = 1f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "fogdir"
        };
        public ConfigValueEntry PrimaryFogColor = new ConfigValueEntry
        {
            Name = "Primary Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = new SharpDX.Color(213,203,172,255),
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "fog_color"
        };
        public ConfigValueEntry SecondaryFogColor = new ConfigValueEntry
        {
            Name = "Secondary Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = new SharpDX.Color(255, 255, 255, 255),
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "fog_color"
        };
        public ConfigValueEntry OverrideSprites = new ConfigValueEntry
        {
            Header = "Sprite Control",
            Name = "Enable",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            PadStyle = PadStyle.Left
        };
        /// <summary>
        /// Access clr_render
        /// </summary>
        public ConfigValueEntry SpriteColor = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = SharpDX.Color.CadetBlue,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "sprite_row1"
        };
        public ConfigValueEntry SpriteBrightness = new ConfigValueEntry
        {
            Name = "Brightness",
            MaxValue = 255,
            MinValue = 0,
            Value = 255,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "sprite_row1",
        };
        public ConfigValueEntry SpriteScale = new ConfigValueEntry
        {
            Name = "Scale",
            MaxValue = 100f,
            MinValue = 0f,
            Value = 1f,
            Incremental = 0.1f,
            IsGrouped = true,
            GroupId = "sprite_row3",
        };
        public ConfigValueEntry GlowProxySize = new ConfigValueEntry
        {
            Name = "Glow Proxy Scale",
            MaxValue = 100f,
            MinValue = 0f,
            Value = 1.5f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "sprite_row3",
        };
        public ConfigValueEntry HDRColorScale = new ConfigValueEntry
        {
            Name = "Color Scale",
            MaxValue = 2f,
            MinValue = 0f,
            Value = 1f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "sprite_row4",
        };

        public ConfigValueEntry OverrideBeams = new ConfigValueEntry
        {
            Header = "Beam Control",
            Name = "Enable",
            MaxValue = true,
            MinValue = false,
            Value = false,
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry BeamWidth = new ConfigValueEntry
        {
            Name = "Width",
            MaxValue = 102.3f,
            MinValue = 0.0f,
            Value = 10f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row1",
        };
        public ConfigValueEntry BeamEndWidth = new ConfigValueEntry
        {
            Name = "End Width",
            MaxValue = 102.3f,
            MinValue = 0.0f,
            Value = 10f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row1",
        };
        public ConfigValueEntry BeamHaloIndex = new ConfigValueEntry
        {
            Name = "Halo Index",
            MaxValue = 32,
            MinValue = 0,
            Value = 16,
            Incremental = 1,
            //ValueType = typeof(int),
            IsGrouped = true,
            GroupId = "beam_row2",
        };
        public ConfigValueEntry BeamHaloScale = new ConfigValueEntry
        {
            Name = "Halo Scale",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Value = 1f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row2",
        };
        public ConfigValueEntry BeamFadeLength = new ConfigValueEntry
        {
            Name = "Fade Length",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Value = 8f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row3",
        };
        public ConfigValueEntry BeamAmplitude = new ConfigValueEntry
        {
            Name = "Amplitude",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Value = 0f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row3",
        };
        public ConfigValueEntry BeamColor = new ConfigValueEntry
        {
            Name = "Color",
            MaxValue = g_Globals.ColorManager.Count,
            MinValue = 0,
            Value = SharpDX.Color.HotPink,
            //ValueType = typeof(Color),
            IsGrouped = true,
            GroupId = "beam_row_last"
        };
        public ConfigValueEntry BeamHDRScale = new ConfigValueEntry
        {
            Name = "Color Scale",
            MaxValue = 100f,
            MinValue = 0f,
            Value = 8f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "beam_row_last"
        };
        public ConfigValueEntry BeamSpeed = new ConfigValueEntry
        {
            Name = "Speed",
            MaxValue = 100f,
            MinValue = 0f,
            Value = 0f,
            Incremental = 0.1f,
            //ValueType = typeof(float),
            PadStyle = PadStyle.Right
        };

    }
}
