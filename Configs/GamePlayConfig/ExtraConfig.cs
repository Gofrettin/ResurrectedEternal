using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.GamePlayConfig
{
    public class ExtraConfig
    {


        //public ConfigValueEntry RandomStickers = new ConfigValueEntry
        //{
        //    Name = "Random Stickers",
        //    MaxValue = 1f,
        //    MinValue = 0f,
        //    Value = 1f,
        //    Incremental = 1f,
        //    //ValueType = typeof(float),
        //    ConvarName = "stickers_debug_randomize"
        //};
        public ConfigValueEntry Bunnyhop = new ConfigValueEntry
        {
            Header = "Other",
            Name = "Bunnyhop",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row7"
        };
        public ConfigValueEntry FlashAlpha = new ConfigValueEntry
        {
            Name = "Flash Alpha",
            MaxValue = 255f,
            MinValue = 0f,
            Value = 66.5f,
            //ValueType = typeof(bool),
            IsGrouped = true,
            GroupId = "row7"
        };
        public ConfigValueEntry chtsbps = new ConfigValueEntry
        {
            Name = "Cheats Bypass",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "sv_cheats",
            IsGrouped = true,
            GroupId = "_magical"
        };
        public ConfigValueEntry radarshowall = new ConfigValueEntry
        {
            Name = "Radar",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "mp_radar_showall",
            IsGrouped = true,
            GroupId = "_magical"
        };
        public ConfigValueEntry MinecraftMode = new ConfigValueEntry
        {
            Name = "Minecraft Mode",
            MaxValue = 3,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_showlowresimage",
            IsGrouped = true,
            GroupId = "moreshit1"
        };
        public ConfigValueEntry mat_drawgray = new ConfigValueEntry
        {
            Name = "Graymode",
            MaxValue = 1,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_drawgray",
            IsGrouped = true,
            GroupId = "moreshit1"
        };
        public ConfigValueEntry mat_showmiplevels = new ConfigValueEntry
        {
            Name = "LSD Mode",
            MaxValue = 3,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_showmiplevels",
            IsGrouped = true,
            GroupId = "moreshit2"
        };

        public ConfigValueEntry mat_measurefillrate = new ConfigValueEntry
        {
            Name = "Matrix V1",
            MaxValue = 1,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_measurefillrate",
            IsGrouped = true,
            GroupId = "moreshit2"
        };
        public ConfigValueEntry fillrate = new ConfigValueEntry
        {
            Name = "Matrix V2",
            MaxValue = 1,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_fillrate",
            IsGrouped = true,
            GroupId = "moreshit3"

        };
        public ConfigValueEntry slowpathwireframe = new ConfigValueEntry
        {
            Name = "Matrix V3",
            MaxValue = 1,
            MinValue = 0,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "r_slowpathwireframe",
            IsGrouped = true,
            GroupId = "moreshit3"
        };
        public ConfigValueEntry staticprops = new ConfigValueEntry
        {
            Name = "Props",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawstaticprops",
            IsGrouped = true,
            GroupId = "staticpropscol"
        };
        public ConfigValueEntry osch = new ConfigValueEntry
        {
            Name = "OSCH Mode",
            MaxValue = 2,
            MinValue = 0,
            Value = 1,
            //ValueType = typeof(int),
            ConvarName = "r_drawothermodels",
            IsGrouped = true,
            GroupId = "staticpropscol"
        };
        public ConfigValueEntry walls = new ConfigValueEntry
        {
            Name = "Walls",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawdisp",
            IsGrouped = true,
            GroupId = "staticpropscol2"
        };
        public ConfigValueEntry detailprops = new ConfigValueEntry
        {
            Name = "Details",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawdetailprops",
            IsGrouped = true,
            GroupId = "staticpropscol2"
        };
        public ConfigValueEntry details2 = new ConfigValueEntry
        {
            Name = "Details 2",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawfuncdetail",
            IsGrouped = true,
            GroupId = "staticpropscol3"
        };
        public ConfigValueEntry world = new ConfigValueEntry
        {
            Name = "World",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawworld",
            IsGrouped = true,
            GroupId = "staticpropscol3"
        };
        public ConfigValueEntry drawbrushmodels = new ConfigValueEntry
        {
            Name = "Brushes",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawbrushmodels",
            IsGrouped = true,
            GroupId = "staticpropscol4"
        };
        public ConfigValueEntry beamsdraw = new ConfigValueEntry
        {
            Name = "Beams",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawbeams",
            IsGrouped = true,
            GroupId = "staticpropscol4"
        };
        public ConfigValueEntry mtwrfm = new ConfigValueEntry
        {
            Name = "Wireframe",
            MaxValue = 4,
            MinValue = -1,
            Value = 0,
            //ValueType = typeof(int),
            ConvarName = "mat_wireframe",
            IsGrouped = true,
            GroupId = "staticpropscol5"
        };
        public ConfigValueEntry mtwrfmdcl = new ConfigValueEntry
        {
            Name = "Wireframe Decal",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "r_modelwireframedecal",
            IsGrouped = true,
            GroupId = "staticpropscol5"
        };
        public ConfigValueEntry mtwrfmdclsm = new ConfigValueEntry
        {
            Name = "Collisions",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "vcollide_wireframe",
            IsGrouped = true,
            GroupId = "staticpropscol6"
        };
        public ConfigValueEntry occlusion = new ConfigValueEntry
        {
            Name = "Occlusion",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "r_visocclusion",
            IsGrouped = true,
            GroupId = "staticpropscol6"
        };
        public ConfigValueEntry drawentities = new ConfigValueEntry
        {
            Name = "Entities",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawentities",
            IsGrouped = true,
            GroupId = "staticpropscol7"
        };

        public ConfigValueEntry trnslcntworld = new ConfigValueEntry
        {
            Name = "Trans World",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawtranslucentworld",
            IsGrouped = true,
            GroupId = "staticpropscol7"
        };
        public ConfigValueEntry trnslcentworldrndr = new ConfigValueEntry
        {
            Name = "Trans Renderables",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawtranslucentrenderables",
            IsGrouped = true,
            GroupId = "staticpropscol8"
        };
        public ConfigValueEntry opqworld = new ConfigValueEntry
        {
            Name = "Opaque World",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawopaqueworld",
            IsGrouped = true,
            GroupId = "staticpropscol8"
        };
        public ConfigValueEntry opqworldrndr = new ConfigValueEntry
        {
            Name = "Opaque Renderables",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_drawopaquerenderables",
            IsGrouped = true,
            GroupId = "staticpropscol9"
        };

        public ConfigValueEntry revrs = new ConfigValueEntry
        {
            Name = "Reverse",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "mat_reversedepth",
            IsGrouped = true,
            GroupId = "staticpropscol9"
        };
        public ConfigValueEntry sv_allow_thirdperson = new ConfigValueEntry
        {
            Name = "Allow Thirdperson",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(int),
            ConvarName = "sv_allow_thirdperson",
            IsGrouped = true,
            GroupId = "lastbutnotleast"
        };
        public ConfigValueEntry ragdollgravity = new ConfigValueEntry
        {
            Name = "Ragdoll Gravity",
            MaxValue = 1000f,
            MinValue = -1000f,
            Value = 100f,
            //ValueType = typeof(int),
            ConvarName = "cl_ragdoll_gravity",
           
            IsGrouped = true,
            GroupId = "lastbutnotleast"
        };
        public ConfigValueEntry magiccolors = new ConfigValueEntry
        {
            Name = "Rainbow",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(int),
            ConvarName = "r_colorstaticprops",
            IsGrouped =true,
            GroupId = "memerow1"
        }; 
        public ConfigValueEntry autostrafe = new ConfigValueEntry
        {
            Name = "Autostrafe",
            MaxValue = true,
            MinValue = false,
            Value = true,
            IsGrouped = true,
            GroupId = "memerow1"
        };
        public ConfigValueEntry enableSkins = new ConfigValueEntry
        {
            Header = "Knife Control",
            Name = "Enable",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row8"
        };
        public ConfigValueEntry Knifemodel = new ConfigValueEntry
        {
            Name = "Knifemodel",
            MaxValue = Enum.GetValues(typeof(KNIFEINDEX)).Length - 1,
            MinValue = 0,
            Value = KNIFEINDEX.KNIFE_KARAMBIT,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row8"
        };
        public ConfigValueEntry Sound = new ConfigValueEntry
        {
            Header = "Sound Control",
            Name = "Enable",
            Value = true,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row9"
        };
        public ConfigValueEntry SoundVolume = new ConfigValueEntry
        {
            Name = "Volume",
            Value = .15f,
            MaxValue = 1f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row9"
        };

        public ConfigValueEntry SoundHitmaker = new ConfigValueEntry
        {
            Name = "Hitmarker",
            Value = true,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row10"
        };
        public ConfigValueEntry SoundQuake = new ConfigValueEntry
        {
            Name = "Quakesounds",
            Value = true,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row10"
        };

        public ConfigValueEntry EnableModelScale = new ConfigValueEntry
        {
            Header = "Model Scaling",
            Name = "Enable",
            Value = true,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row11"
        };
        public ConfigValueEntry PlayerScale = new ConfigValueEntry
        {
            Name = "Player Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row11"
        };
        public ConfigValueEntry BomScale = new ConfigValueEntry
        {
            Name = "Bomb Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row12"
        };
        public ConfigValueEntry WeaponScale = new ConfigValueEntry
        {
            Name = "Weapon Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row12"
        };
        public ConfigValueEntry ProjectileScale = new ConfigValueEntry
        {
            Name = "Projectile Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row13"
        };
        public ConfigValueEntry GrenadeScale = new ConfigValueEntry
        {
            Name = "Grenade Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row13"
        };
        public ConfigValueEntry DefuseScale = new ConfigValueEntry
        {
            Name = "Defuse Kit Scale",
            Value = 1f,
            MaxValue = 100f,
            MinValue = 0f,
            Incremental = .01f,
            //ValueType = typeof(KNIFEINDEX),
            IsGrouped = true,
            GroupId = "row14"
        };
        public ConfigValueEntry DisableKillCam = new ConfigValueEntry
        {
            Name = "Kill Cam Disable Enforcer",
            MaxValue = true,
            MinValue = false,
            Value = false,
            //ValueType = typeof(bool),
            ConvarName = "spec_replay_autostart",
            HiddenFromMenu = true
        };
        public ConfigValueEntry Freeze = new ConfigValueEntry
        {
            Name = "Freezu Cam Disable Enforcer",
            MaxValue = true,
            MinValue = false,
            Value = true,
            //ValueType = typeof(bool),
            ConvarName = "cl_disablefreezecam",
            HiddenFromMenu = true
        };
    }
}
