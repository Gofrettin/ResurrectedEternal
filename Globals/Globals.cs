using ResurrectedEternal.Globals;
using RRFull;
using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.GenericObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ResurrectedEternal;

public static class g_Globals
{
    public static string MainEntryAssembly => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\";
    public static string ColorConfig => MainEntryAssembly + "configs\\colors.json";
    public static string ModeConfig => MainEntryAssembly + "configs\\mode.json";
    public static string ConvarConfig => MainEntryAssembly + "configs\\convars.json";
    public static string Offsets => MainEntryAssembly + "configs\\offsets.json";
    public static string Signatures => MainEntryAssembly + "configs\\signatures.json";
    public static string ConfigConfig => MainEntryAssembly + "configs\\config.json";
    public static string SoundConfig => MainEntryAssembly + "configs\\sounds.json";
    public static string NickConfig => MainEntryAssembly + "configs\\nick.json";

    

    static g_Globals()
    {

    }

    public static FontManager FontManager = new FontManager();
    public static Offsets Offset; // = new Offsets();
    public static Dictionary<string, int> NetVars = new Dictionary<string, int>();
    public static ColorManager ColorManager = new ColorManager();
    public static Config Config = new Config();



    public static GlobalVarsBase m_dwGlobalVars => Henker.Singleton.Engine.GlobalVars;

    public static string Nickname;

    public static string MapName => Henker.Singleton.Engine.CurrentMapName;

    public static string[] skyboxes = new string[]
    {
         "Off",
        "cs_baggage_skybox_",
        "cs_tibet", "embassy",
        "italy",
        "jungle",
        "nukeblank",
        "office",
        "sky_cs15_daylight01_hdr",
        "sky_cs15_daylight02_hdr",
        "sky_cs15_daylight03_hdr",
        "sky_cs15_daylight04_hdr",
        "sky_csgo_cloudy01",
        "sky_csgo_night02",
        "sky_csgo_night02b",
        "sky_dust",
        "sky_venice",
        "vertigo",
        "vietnam"
    };
}

