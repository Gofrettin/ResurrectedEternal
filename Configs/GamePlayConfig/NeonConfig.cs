using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.GamePlayConfig
{
    public class NeonConfig
    {
        public ConfigValueEntry Enable = new ConfigValueEntry
        {
            Header = "Glow Configuration",
            Name = "Enable Neon",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row1"
        };
        public ConfigValueEntry GlowAt = new ConfigValueEntry
        {
            Name = "Target",
            MaxValue = Enum.GetValues(typeof(TargetType)).Length - 1,
            MinValue = 0,
            Value = TargetType.Enemy,
            //ValueType = typeof(TargetType),
            IsGrouped = true,
            GroupId = "row1"
        };

        public ConfigValueEntry NeonOutline = new ConfigValueEntry
        {
            Name = "Outline Thickness",
            MaxValue = 20,
            MinValue = 0.1f,
            Incremental = 0.1f,
            Value = 6f,
            //ValueType = typeof(float),
            ConvarName = "glow_outline_width",
            ConvarRequire = "Enable",
            IsGrouped = true,
            GroupId = "row2"
        };

        public ConfigValueEntry MaxVelocity = new ConfigValueEntry
        {
            Name = "Movement Only",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row2"
        };

        public ConfigValueEntry GlowStyle = new ConfigValueEntry
        {
            Name = "Type",
            MaxValue = Enum.GetValues(typeof(GlowRenderStyle_t)).Length - 1,
            MinValue = 0,
            Value = GlowRenderStyle_t.GLOWRENDERSTYLE_DEFAULT,
            //ValueType = typeof(GlowRenderStyle_t),
            IsGrouped = true,
            GroupId = "row3"
        };

        public ConfigValueEntry GlowStyleWhenVisible = new ConfigValueEntry
        {
            Name = "Type Visible",
            MaxValue = Enum.GetValues(typeof(GlowRenderStyle_t)).Length - 1,
            MinValue = 0,
            Value = GlowRenderStyle_t.GLOWRENDERSTYLE_DEFAULT,
            //ValueType = typeof(GlowRenderStyle_t),
            IsGrouped = true,
            GroupId = "row3"
        };
        public ConfigValueEntry AlphaMax = new ConfigValueEntry
        {
            Name = "Alpha Max",
            MaxValue = 1f,
            MinValue = 0f,
            Incremental = 0.01f,
            Value = 1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "row4"
        };
        public ConfigValueEntry FullBloom = new ConfigValueEntry
        {
            Name = "Full Bloom",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row4"
        };

        public ConfigValueEntry NeonGlowWeapons = new ConfigValueEntry
        {
            Header = "Other",
            Name = "Weapons",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            ConvarName = "mp_weapons_glow_on_ground",
            HiddenFromMenu = false,
            IsGrouped = true,
            GroupId = "row5"
        };
        public ConfigValueEntry NeonGlowGrenades = new ConfigValueEntry
        {
            Name = "Grenades",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row5"
        };
        public ConfigValueEntry NeonGlowProjectiles = new ConfigValueEntry
        {
            Name = "Projectiles",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row6"
        };
        public ConfigValueEntry NeonGlowDefuse = new ConfigValueEntry
        {
            Name = "Defuse Kits",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row6"
        };
        public ConfigValueEntry NeonGlowBomb = new ConfigValueEntry
        {
            Name = "Bomb",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row7"
        };
        public ConfigValueEntry NeonGlowChickens = new ConfigValueEntry
        {
            Name = "Chickens",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row7"
        };

        public ConfigValueEntry EnableCham = new ConfigValueEntry
        {
            Header = "Model Modification Control",
            Name = "Enable Cham",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row8"
        };
        public ConfigValueEntry ChamBrightness = new ConfigValueEntry
        {
            Name = "Cham Brightness",
            MaxValue = 255f,
            MinValue = -255f,
            Value = 1f,
            //ValueType = typeof(float),
            ConvarName = "r_modelAmbientMin",
            ConvarRequire = "EnableCham",
            IsGrouped = true,
            GroupId = "row8"
        };

        public ConfigValueEntry SetViewmodelColor = new ConfigValueEntry
        {
            Name = "Enable Viewmodel Color",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row9"
        };
        public ConfigValueEntry Modelcolors = new ConfigValueEntry
        {
            Name = "Enable Model Colors",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row9"
        };
        public ConfigValueEntry AmbientLightning = new ConfigValueEntry
        {
            Header = "Ambient Lightning",
            Name = "Enabled",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool)
        };
        public ConfigValueEntry AmbientLightingRed = new ConfigValueEntry
        {
            Name = "Red",
            MaxValue = 15f,
            MinValue = -15f,
            Value = 1.0f,
            Incremental = 0.01f,
            //ValueType = typeof(float),
            ConvarName = "mat_ambient_light_r",
            ConvarRequire = "AmbientLightning",
            IsGrouped = true,
            GroupId = "ambient_row1"
        };
        public ConfigValueEntry AmbientLightingGreen = new ConfigValueEntry
        {
            Name = "Green",
            MaxValue = 15f,
            MinValue = -15f,
            Value = 1.0f,
            Incremental = 0.01f,
            //ValueType = typeof(float),
            ConvarName = "mat_ambient_light_g",
            ConvarRequire = "AmbientLightning",
            IsGrouped = true,
            GroupId = "ambient_row1"


        };
        public ConfigValueEntry AmbientLightingBlue = new ConfigValueEntry
        {
            Name = "Blue",
            MaxValue = 15f,
            MinValue = -15f,
            Incremental = 0.01f,
            Value = 1.0f,
            //ValueType = typeof(float),
            ConvarName = "mat_ambient_light_b",
            ConvarRequire = "AmbientLightning",
            IsGrouped = true,
            GroupId = "_ayylmao"
        };
        public ConfigValueEntry FullBright = new ConfigValueEntry
        {
            Name = "Full Bright",
            MaxValue = 100,
            MinValue = 0,
            Incremental = 1,
            Value = 0,
            ConvarName = "mat_fullbright",
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "_ayylmao"
        };
        public ConfigValueEntry CustomAutoExposureMin = new ConfigValueEntry
        {
            Name = "Dark Min",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.5f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "nightmode_row"
        };
        public ConfigValueEntry CustomAutoExposureMax = new ConfigValueEntry
        {
            Name = "Bright Max",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 1.25f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "nightmode_row"
        };
        public ConfigValueEntry CustomBloomScale = new ConfigValueEntry
        {
            Header = "Bloom Configuration",
            Name = "BloomScale",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "bms_row2"
        };
        public ConfigValueEntry CustomBloomScaleMin = new ConfigValueEntry
        {
            Name = "BloomScale Min",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "bms_row2"
        };
        public ConfigValueEntry BloomExponent = new ConfigValueEntry
        {
            Name = "Exponent",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 2.5f,
            ConvarName = "r_bloomtintexponent",
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "bms_row3"
        };
        public ConfigValueEntry BloomSaturation = new ConfigValueEntry
        {
            Name = "Saturation",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 1f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "bms_row3"
        };
        public ConfigValueEntry BloomTintR = new ConfigValueEntry
        {
            Name = "Red",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.3f,
            ConvarName = "r_bloomtintr",
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "rowtint1"
        };
        public ConfigValueEntry BloomTintG = new ConfigValueEntry
        {
            Name = "Green",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.3f,
            ConvarName = "r_bloomtintg",
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "rowtint1"
        };
        public ConfigValueEntry BloomTintB = new ConfigValueEntry
        {
            Name = "Blue",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 0.11f,
            ConvarName = "r_bloomtintb",
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "rowtint1"
        };

        public ConfigValueEntry TonemapPercentTarget = new ConfigValueEntry
        {
            Header = "Tonemap Configuration",
            Name = "Target",
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = 0.01f,
            Value = 80.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "tonemapper_row1"
        };
        public ConfigValueEntry TonemapPercentBrightPixels = new ConfigValueEntry
        {
            Name = "Bright Pixel Percent",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01f,
            Value = 3.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "tonemapper_row1"
        };
        public ConfigValueEntry TonemapMinAvgLum = new ConfigValueEntry
        {
            Name = "Average Lumens",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01,
            Value = 3.0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "tonemapper_row2"
        };
        public ConfigValueEntry TonemapRate = new ConfigValueEntry
        {
            Name = "Rate",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = 0.01,
            Value = 0.2f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "tonemapper_row2"
        };
        public ConfigValueEntry VignetteStartOverride = new ConfigValueEntry
        {
            Header = "Vignette Configuration",
            Name = "Start",
            MaxValue = 2f,
            MinValue = -1f,
            Incremental = .01f,
            Value = 0f,
            //ValueType = typeof(float),
            ConvarName = "mat_local_contrast_vignette_start_override",
            IsGrouped = true,
            GroupId = "vignette_row1"
        };

        public ConfigValueEntry VignetteEndOverride = new ConfigValueEntry
        {
            Name = "End",
            MaxValue = 3,
            MinValue = -1,
            Incremental = .01f,
            Value = 1f,
            //ValueType = typeof(float),
            ConvarName = "mat_local_contrast_vignette_end_override",
            IsGrouped = true,
            GroupId = "vignette_row1"
        };
        public ConfigValueEntry VignetteMidtoneOverride = new ConfigValueEntry
        {
            Name = "Midtone",
            MaxValue = 2f,
            MinValue = -1f,
            Incremental = 0.01f,
            Value = .5f,
            //ValueType = typeof(float),
            ConvarName = "mat_local_contrast_midtone_mask_override",
            PadStyle = PadStyle.Left
        };
        public ConfigValueEntry VignetteContrastScale = new ConfigValueEntry
        {

            Name = "Contrast Scale",
            MaxValue = 50f,
            MinValue = -50f,
            Incremental = 0.01f,
            Value = 0f,
            //ValueType = typeof(float),
            ConvarName = "mat_local_contrast_scale_override",
            IsGrouped = true,
            GroupId = "vignette_row2"
        };


        public ConfigValueEntry VignetteEndScale = new ConfigValueEntry
        {
            Name = "Edge Scale",
            MaxValue = 1000f,
            MinValue = -1000f,
            Incremental = 0.1f,
            Value = 1f,
            //ValueType = typeof(float),
            ConvarName = "mat_local_contrast_edge_scale_override",
            IsGrouped = true,
            GroupId = "vignette_row2"
        };
        public ConfigValueEntry VignetteBlurStrength = new ConfigValueEntry
        {
            Name = "Blur Strength",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = .01f,
            Value = 0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "vignette_row3"
        };
        public ConfigValueEntry FadeToBlackStrength = new ConfigValueEntry
        {
            Name = "Fade 2 Black Strength",
            MaxValue = float.MaxValue,
            MinValue = float.MinValue,
            Incremental = .01f,
            Value = 0f,
            //ValueType = typeof(float),
            IsGrouped = true,
            GroupId = "vignette_row3"
        };
    }
}
