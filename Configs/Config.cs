using Newtonsoft.Json;
using ResurrectedEternal.Configs.GamePlayConfig;
using ResurrectedEternal.Configs;
using ResurrectedEternal.Configs.GamePlayConfig;
using ResurrectedEternal.Configs.SubConfig;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace ResurrectedEternal
{
    public enum ConfigType
    {
        Generic,
        Colors,
        Convars,
        Mode
    }
    public class ConfigValueEntry
    {
        [JsonIgnore]
        public string AccessorName;
        [JsonIgnore]
        public string Header;
        [JsonIgnore]
        public string Name;
        //public Type ValueType;
        [JsonIgnore]
        public object MaxValue;
        [JsonIgnore]
        public object MinValue;

        public object Value;

        [JsonIgnore]
        public object Incremental = 1.0f;
        [JsonIgnore]
        public string ConvarName;
        [JsonIgnore]
        public string ConvarRequire;
        [JsonIgnore]
        public bool HiddenFromMenu = false;
        [JsonIgnore]
        public object DefaultValue;
        [JsonIgnore]
        public bool IsGrouped = false;
        [JsonIgnore]
        public string GroupId;
        [JsonIgnore]
        public PadStyle PadStyle = PadStyle.Center;
    }

    public enum RadarStyle
    {
        Circular,
        Box
    }
    public enum AimbotStyle
    {
        Pixel,
        Angle,
        HitBox
    }

    public enum TargetType
    {
        Enemy,
        Friendly,
        All
    }

    public enum RCSSmoothType
    {
        None,
        Adaptive,
        Incremental
    }

    public enum SmoothType
    {
        SmoothStep,
        Lerp
    }

    public enum AimPriority
    {
        ClosestToCrosshair,
        DistanceToSelf,

    }
    public enum DrawPosition
    {
        Top,
        Left,
        Right,
        Bottom
    }

    public class Config
    {

        public AimbotConfig AimbotConfig = new AimbotConfig();
        public VisualConfig VisualConfig = new VisualConfig();
        public NeonConfig NeonConfig = new NeonConfig();

        //public AmbientConfig MovieConfig = new AmbientConfig();
        public EnvironmentConfig EnvironmentConfig = new EnvironmentConfig();
        public SunConfig SunConfig = new SunConfig();
        public ExtraConfig ExtraConfig = new ExtraConfig(); 
        public OtherConfig OtherConfig = new OtherConfig();
        public HudConfig HudConfig = new HudConfig();
    }
}