using Newtonsoft.Json;
using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.BaseObjects.Fire;
using ResurrectedEternal.BSPParse;
using ResurrectedEternal.GenericObjects;
using ResurrectedEternal.MemoryManager.PatMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace ResurrectedEternal
{
    public static class Generators
    {
        public static string EndPadString(string value, int pad = 30)
        {
            int diff = 0;
            if (value.Length < pad)
                diff = pad - value.Length;
            for (int i = 0; i < diff; i++)
                value += " ";

            return value;

        }
        public static string StartPadString(string value, int pad = 30)
        {
            string _pad = "";
            for (int i = 0; i < pad; i++)
                _pad += " ";

            return _pad + value;


        }
        public static bool IsProjectile(ClientClass _class)
        {
            switch (_class)
            {
                case ClientClass.CBaseCSGrenadeProjectile:
                case ClientClass.CBreachChargeProjectile:
                case ClientClass.CBumpMineProjectile:
                case ClientClass.CDecoyProjectile:
                case ClientClass.CMolotovProjectile:
                case ClientClass.CSensorGrenadeProjectile:
                case ClientClass.CSmokeGrenadeProjectile:
                case ClientClass.CSnowballProjectile:
                case ClientClass.CTEClientProjectile:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsGrenade(ClientClass _class)
        {
            switch (_class)
            {
                case ClientClass.CBaseCSGrenade:
                case ClientClass.CBaseGrenade:
                case ClientClass.CDecoyGrenade:
                case ClientClass.CFlashbang:
                case ClientClass.CIncendiaryGrenade:
                case ClientClass.CHEGrenade:
                case ClientClass.CMolotovGrenade:
                case ClientClass.CSmokeGrenade:
                case ClientClass.CSensorGrenade:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsWeapon(ClientClass _class)
        {
            switch (_class)
            {
                case ClientClass.CAK47:
                case ClientClass.CWeaponAug:
                case ClientClass.CWeaponAWP:
                case ClientClass.CWeaponBaseItem:
                case ClientClass.CWeaponBizon:
                case ClientClass.CWeaponCSBase:
                case ClientClass.CWeaponCSBaseGun:
                case ClientClass.CWeaponCycler:
                case ClientClass.CDEagle:
                case ClientClass.CWeaponElite:
                case ClientClass.CWeaponFamas:
                case ClientClass.CWeaponFiveSeven:
                case ClientClass.CWeaponG3SG1:
                case ClientClass.CWeaponGalil:
                case ClientClass.CWeaponGalilAR:
                case ClientClass.CWeaponGlock:
                case ClientClass.CWeaponHKP2000:
                case ClientClass.CWeaponM249:
                case ClientClass.CWeaponM3:
                case ClientClass.CWeaponM4A1:
                case ClientClass.CWeaponMAC10:
                case ClientClass.CWeaponMag7:
                case ClientClass.CWeaponMP5Navy:
                case ClientClass.CWeaponMP7:
                case ClientClass.CWeaponMP9:
                case ClientClass.CWeaponNegev:
                case ClientClass.CWeaponNOVA:
                case ClientClass.CWeaponP228:
                case ClientClass.CWeaponP250:
                case ClientClass.CWeaponP90:
                case ClientClass.CWeaponSawedoff:
                case ClientClass.CSCAR17:
                case ClientClass.CWeaponSCAR20:
                case ClientClass.CWeaponScout:
                case ClientClass.CWeaponSG550:
                case ClientClass.CWeaponSG552:
                case ClientClass.CWeaponSG556:
                case ClientClass.CWeaponShield:
                case ClientClass.CWeaponSSG08:
                case ClientClass.CWeaponTaser:
                case ClientClass.CWeaponTec9:
                case ClientClass.CWeaponTMP:
                case ClientClass.CWeaponUMP45:
                case ClientClass.CWeaponUSP:
                case ClientClass.CWeaponXM1014:
                case ClientClass.CKnife:
                case ClientClass.CKnifeGG:
                    return true;
                default:
                    return false;
            }
        }

        internal static string ExtractMapName(string filename)
        {
            //"maps\\de_Dust2.bsp"
            return filename.Replace("maps\\", "").Replace(".bsp", "");
        }

        public static string FormatNiceResult(e_RoundEndReason _reasong)
        {
            switch (_reasong)
            {
                case e_RoundEndReason.Invalid_Round_End_Reason:
                    return "Invalid Round End Reason";
                case e_RoundEndReason.RoundEndReason_StillInProgress:
                    return "Round Started";
                case e_RoundEndReason.Target_Bombed:
                    return "The Bomb Exploded!";
                case e_RoundEndReason.VIP_Escaped:
                    return "The VIP has escaped!";
                case e_RoundEndReason.VIP_Assassinated:
                    return "The VIP has been Assassinated!";
                case e_RoundEndReason.Terrorists_Escaped:
                    return "The Terrorist has escapted!";
                case e_RoundEndReason.CTs_PreventEscape:
                    return "CT's prevented the escaped!";
                case e_RoundEndReason.Escaping_Terrorists_Neutralized:
                    return "what";
                case e_RoundEndReason.Bomb_Defused:
                    return "The Bomb has been defused!";
                case e_RoundEndReason.CTs_Win:
                    return "CT's won!";
                case e_RoundEndReason.Terrorists_Win:
                    return "T's won!";
                case e_RoundEndReason.Round_Draw:
                    return "That's a draw!";
                case e_RoundEndReason.All_Hostages_Rescued:
                case e_RoundEndReason.Target_Saved:
                case e_RoundEndReason.Hostages_Not_Rescued:
                case e_RoundEndReason.Terrorists_Not_Escaped:
                case e_RoundEndReason.VIP_Not_Escaped:
                case e_RoundEndReason.Game_Commencing:
                case e_RoundEndReason.Terrorists_Surrender:
                case e_RoundEndReason.CTs_Surrender:
                case e_RoundEndReason.Terrorists_Planted:
                case e_RoundEndReason.CTs_ReachedHostage:
                case e_RoundEndReason.RoundEndReason_Count:
                default:
                    return _reasong.ToString();
            }
        }
        public static Dictionary<string, object> GetStaticPropertyBag(Type t)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            var map = new Dictionary<string, object>();
            foreach (var prop in t.GetFields(flags))
            {
                map[prop.Name] = prop.GetValue(null);
            }
            return map;
        }
        public static Random random = new Random();

        public static string GetRandomString(int length = 32, string palette = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            char[] chars = new char[length];
            random = new Random(random.Next(int.MinValue, int.MaxValue));
            for (int i = 0; i < length; i++)
                chars[i] = palette[random.Next(0, palette.Length)];
            return new string(chars);
        }
        public static SharpDX.Color GetColorBySetting(Config config, PlayerTeam friendly, bool visible, bool immunity)
        {
            if (immunity)
                return (SharpDX.Color)config.OtherConfig.cImmunityColor.Value;

            switch (friendly)
            {

                case PlayerTeam.Terrorist:
                    return visible ? (SharpDX.Color)config.OtherConfig.cTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cTerroristNormalColor.Value;
                case PlayerTeam.CounterTerrorist:
                    return visible ? (SharpDX.Color)config.OtherConfig.cCTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cCTerroristNormalColor.Value;
                case PlayerTeam.Neutral:
                default:
                    return SharpDX.Color.White;
            }
        }
        public static SharpDX.Color GetNeonColorBySetting(Config config, PlayerTeam friendly, bool visible, bool immunity)
        {
            if (immunity)
                return (SharpDX.Color)config.OtherConfig.cImmunityColor.Value;

            switch (friendly)
            {

                case PlayerTeam.Terrorist:
                    return visible ? (SharpDX.Color)config.OtherConfig.cNeonTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cNeonTerroristNormalColor.Value;
                case PlayerTeam.CounterTerrorist:
                    return visible ? (SharpDX.Color)config.OtherConfig.cNeonCTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cNeonCTerroristNormalColor.Value;
                case PlayerTeam.Neutral:
                default:
                    return SharpDX.Color.White;
            }
        }
        public static SharpDX.Color GetColorBySetting(Config config, BasePlayer _player)
        {
            if (_player.m_bGunGameImmunity)
                return (SharpDX.Color)config.OtherConfig.cImmunityColor.Value;

            switch (_player.Team)
            {

                case PlayerTeam.Terrorist:
                    return _player.IsVisible ? (SharpDX.Color)config.OtherConfig.cTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cTerroristNormalColor.Value;
                case PlayerTeam.CounterTerrorist:
                    return _player.IsVisible ? (SharpDX.Color)config.OtherConfig.cCTerroristVisibleColor.Value : (SharpDX.Color)config.OtherConfig.cCTerroristNormalColor.Value;
                case PlayerTeam.Neutral:
                default:
                    return SharpDX.Color.White;
            }
        }

        public static string UNXOR(uint thisPtr, string val)
        {
            var _val = BitConverter.ToInt32(Encoding.UTF8.GetBytes(val), 0) ^ thisPtr;
            return Encoding.UTF8.GetString(BitConverter.GetBytes(_val));
        }

        public static uint XOR(uint thisPtr, float val)
        {

            return BitConverter.ToUInt32(BitConverter.GetBytes(val), 0) ^ thisPtr;
        }
        public static int XOR(int thisPtr, float val)
        {

            return BitConverter.ToInt32(BitConverter.GetBytes(val), 0) ^ thisPtr;
        }
        public static uint XOR(uint thisPtr, int val)
        {

            return BitConverter.ToUInt32(BitConverter.GetBytes(val), 0) ^ thisPtr;
        }
        public static int XOR(int thisPtr, int val)
        {

            return BitConverter.ToInt32(BitConverter.GetBytes(val), 0) ^ thisPtr;
        }

        public static int XORINT(IntPtr _ptr, int xor_value)
        {
            return xor_value ^= (int)_ptr;
        }

        public static float XORFLOAT(IntPtr _ptr, byte[] result)
        {
            //byte[] xored = MemoryLoader.instance.Reader.ReadBytes(pThis + 0x2C, 4);

            byte[] key = BitConverter.GetBytes((int)_ptr);

            for (int i = 0; i < 4; ++i)
                result[i] ^= key[i];

            return BitConverter.ToSingle(result, 0);
        }

        internal static PlayerTeam GetWinningTeam(e_RoundEndReason roundState)
        {
            switch (roundState)
            {
                case e_RoundEndReason.VIP_Assassinated:
                case e_RoundEndReason.VIP_Not_Escaped:
                case e_RoundEndReason.CTs_Surrender:
                case e_RoundEndReason.Terrorists_Planted:
                case e_RoundEndReason.Hostages_Not_Rescued:
                case e_RoundEndReason.Terrorists_Win:
                case e_RoundEndReason.Terrorists_Escaped:
                case e_RoundEndReason.Target_Bombed:
                    return PlayerTeam.Terrorist;
                case e_RoundEndReason.VIP_Escaped:
                case e_RoundEndReason.CTs_PreventEscape:
                case e_RoundEndReason.Escaping_Terrorists_Neutralized:
                case e_RoundEndReason.Bomb_Defused:
                case e_RoundEndReason.CTs_Win:
                case e_RoundEndReason.All_Hostages_Rescued:
                case e_RoundEndReason.Target_Saved:
                case e_RoundEndReason.Terrorists_Not_Escaped:
                case e_RoundEndReason.Terrorists_Surrender:
                case e_RoundEndReason.CTs_ReachedHostage:
                    return PlayerTeam.CounterTerrorist;
                case e_RoundEndReason.Round_Draw:
                case e_RoundEndReason.RoundEndReason_StillInProgress:
                case e_RoundEndReason.Invalid_Round_End_Reason:
                case e_RoundEndReason.Game_Commencing:
                case e_RoundEndReason.RoundEndReason_Count:
                default:
                    return PlayerTeam.Neutral;
            }
        }

        public static byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                var length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source,
                    CompressionMode.Decompress))
                {
                    var result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static byte[] Compress(byte[] input)
        {
            using (var result = new MemoryStream())
            {
                var lengthBytes = BitConverter.GetBytes(input.Length);
                result.Write(lengthBytes, 0, 4);

                using (var compressionStream = new GZipStream(result,
                    CompressionMode.Compress))
                {
                    compressionStream.Write(input, 0, input.Length);
                    compressionStream.Flush();

                }
                return result.ToArray();
            }
        }
        public static BSPFile GenerateBSP(string path, string mapDir)
        {
            bool available = false;
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(mapDir))
                return null;
            //save our previous states on first generation of bsp

            //maps/de_dust2.bsp
            var _map = "";
            if (File.Exists(g_Globals.MainEntryAssembly + mapDir))
            {
                //we have the addon so we want to use raytrace
                available = true;
                //SetVisibleCheck(VisibleCheck.RayTrace);
                _map = g_Globals.MainEntryAssembly + mapDir;
            }
            else
            {


                //we dont have the addon
                _map = path + mapDir;
                //check if we have to restore our visible check to the previous one

            }
            //make it return the map path so we can simply grab it from the file and dont cause unnecessary out of memory exceptions
            using (var stream = File.OpenRead(_map))
            {
                return new BSPFile(stream, available);
            }
        }
        public static bool Exclude(ClientClass _class)
        {
            switch (_class)
            {
                case ClientClass.CCSPlayer:
                    return true;
                case ClientClass.CAK47:
                case ClientClass.CWeaponAug:
                case ClientClass.CWeaponAWP:
                case ClientClass.CWeaponBaseItem:
                case ClientClass.CWeaponBizon:
                case ClientClass.CWeaponCSBase:
                case ClientClass.CWeaponCSBaseGun:
                case ClientClass.CWeaponCycler:
                case ClientClass.CDEagle:
                case ClientClass.CWeaponElite:
                case ClientClass.CWeaponFamas:
                case ClientClass.CWeaponFiveSeven:
                case ClientClass.CWeaponG3SG1:
                case ClientClass.CWeaponGalil:
                case ClientClass.CWeaponGalilAR:
                case ClientClass.CWeaponGlock:
                case ClientClass.CWeaponHKP2000:
                case ClientClass.CWeaponM249:
                case ClientClass.CWeaponM3:
                case ClientClass.CWeaponM4A1:
                case ClientClass.CWeaponMAC10:
                case ClientClass.CWeaponMag7:
                case ClientClass.CWeaponMP5Navy:
                case ClientClass.CWeaponMP7:
                case ClientClass.CWeaponMP9:
                case ClientClass.CWeaponNegev:
                case ClientClass.CWeaponNOVA:
                case ClientClass.CWeaponP228:
                case ClientClass.CWeaponP250:
                case ClientClass.CWeaponP90:
                case ClientClass.CWeaponSawedoff:
                case ClientClass.CSCAR17:
                case ClientClass.CWeaponSCAR20:
                case ClientClass.CWeaponScout:
                case ClientClass.CWeaponSG550:
                case ClientClass.CWeaponSG552:
                case ClientClass.CWeaponSG556:
                case ClientClass.CWeaponShield:
                case ClientClass.CWeaponSSG08:
                case ClientClass.CWeaponTaser:
                case ClientClass.CWeaponTec9:
                case ClientClass.CWeaponTMP:
                case ClientClass.CWeaponUMP45:
                case ClientClass.CWeaponUSP:
                case ClientClass.CWeaponXM1014:
                    return true;
                default:
                    return false;
            }
        }
        public static string ParseChars(char[] input)
        {
            return new string(input).Trim('\0');
        }

        public static bool IsFlashbang(string modelName)
        {
            return modelName == "models/Weapons/w_eq_flashbang_dropped.mdl";
        }

        public static string GetGrenadeName(string modelName)
        {
            switch (modelName)
            {
                case "models/Weapons/w_eq_smokegrenade_thrown.mdl":
                    return "SMOKE";
                case "models/Weapons/w_eq_fraggrenade_dropped.mdl":
                    return "HE";
                case "models/Weapons/w_eq_flashbang_dropped.mdl":
                    return "FLASH";
                case "models/Weapons/w_eq_decoy_dropped.mdl":
                    return "DECOY";
                case "models/Weapons/w_eq_molotov_dropped.mdl":
                    return "MOLLY";
                case "models/Weapons/w_eq_incendiarygrenade_dropped.mdl":
                    return "INCENDIARY";
                default:
                    return "UNKNOWN";
            }
        }
        public static string GetClassIdName(ClientClass _class)
        {
            switch (_class)
            {

                case ClientClass.CCSPlayer:
                    return "a plyer";
                case ClientClass.CAK47:
                    return "russian gun";
                case ClientClass.CWeaponAug:
                    return "AUG";
                case ClientClass.CWeaponAWP:
                    return "BIG BOY WEAPON";
                case ClientClass.CWeaponBaseItem:
                    return "this unknown";
                case ClientClass.CWeaponBizon:
                    return "BIZON";
                case ClientClass.CWeaponCSBase:
                    return "this a wpcsbase";
                case ClientClass.CWeaponCSBaseGun:
                    return "dis a wpcsbasegun";
                case ClientClass.CWeaponCycler:
                    return "what is a weapon cycler";
                case ClientClass.CDEagle:
                    return "DEAGLE";
                case ClientClass.CWeaponElite:
                    return "COWBOY GUNS";
                case ClientClass.CWeaponFamas:
                    return "BRRT";
                case ClientClass.CWeaponFiveSeven:
                    return "pak pak";
                case ClientClass.CWeaponG3SG1:
                    return "G3SG1";
                case ClientClass.CWeaponGalil:
                case ClientClass.CWeaponGalilAR:
                    return "GALIL";
                case ClientClass.CWeaponGlock:
                    return "GANGSTER PISTOL";
                case ClientClass.CWeaponHKP2000:
                    return "HKP2000";
                case ClientClass.CWeaponM249:
                    return "scream gun";
                case ClientClass.CWeaponM3:
                    return "shotty";
                case ClientClass.CWeaponM4A1:
                    return "german gun";
                case ClientClass.CWeaponMAC10:
                    return "uzi";
                case ClientClass.CWeaponMag7:
                    return "MAG7";
                case ClientClass.CWeaponMP5Navy:
                    return "MP5";
                case ClientClass.CWeaponMP7:
                    return "MP7";
                case ClientClass.CWeaponMP9:
                    return "MP9";
                case ClientClass.CWeaponNegev:
                    return "hell gun";
                case ClientClass.CWeaponNOVA:
                    return "NOVA";
                case ClientClass.CWeaponP228:
                    return "P228";
                case ClientClass.CWeaponP250:
                    return "P250";
                case ClientClass.CWeaponP90:
                    return "P90";
                case ClientClass.CWeaponSawedoff:
                    return "zombie apocalypse weapon";
                case ClientClass.CSCAR17:
                    return "SCAR17";
                case ClientClass.CWeaponSCAR20:
                    return "SCAR20";
                case ClientClass.CWeaponScout:
                    return "CHEATER WEAPON";
                case ClientClass.CWeaponSG550:
                    return "SG550";
                case ClientClass.CWeaponSG552:
                    return "SG552";
                case ClientClass.CWeaponSG556:
                    return "SG556";
                case ClientClass.CWeaponShield:
                    return "a shield";
                case ClientClass.CWeaponSSG08:
                    return "SSG08";
                case ClientClass.CWeaponTaser:
                    return "bzzt";
                case ClientClass.CWeaponTec9:
                    return "TEC9";
                case ClientClass.CWeaponTMP:
                    return "TMP";
                case ClientClass.CWeaponUMP45:
                    return "UMP45";
                case ClientClass.CWeaponUSP:
                    return "USP";
                case ClientClass.CWeaponXM1014:
                    return "BOT WEAPON";
                case ClientClass.CC4:
                    return "SPRENGSTOFF";
                case ClientClass.CPlantedC4:
                    return "GEPFLANZTER SPRENGSTOFF";
                case ClientClass.CBaseCSGrenade:
                    return "WHAT";
                case ClientClass.CBaseGrenade:
                    return "???";
                case ClientClass.CDecoyGrenade:
                    return "DECOY";
                case ClientClass.CFlashbang:
                    return "FLASHBANG";
                case ClientClass.CIncendiaryGrenade:
                    return "INCENDIARY";
                case ClientClass.CHEGrenade:
                    return "HE";
                case ClientClass.CMolotovGrenade:
                    return "MOLOTOV";
                case ClientClass.CSmokeGrenade:
                    return "SMOKE";
                case ClientClass.CSensorGrenade:
                    return "SENSOR";
                case ClientClass.CBaseCSGrenadeProjectile:
                    return "FLASH";
                case ClientClass.CBreachChargeProjectile:
                    return "BREACH CHARGE??";
                case ClientClass.CBumpMineProjectile:
                    return "WHAT IS THIS EVEN";
                case ClientClass.CDecoyProjectile:
                    return "DECOY";
                case ClientClass.CMolotovProjectile:
                    return "MOLTOV";
                case ClientClass.CSensorGrenadeProjectile:
                    return "SENSOR";
                case ClientClass.CSmokeGrenadeProjectile:
                    return "SMOKE";
                case ClientClass.CSnowballProjectile:
                    return "SNOBOLL";
                case ClientClass.CTEClientProjectile:
                    return "WHAT";
                default:
                    return _class.ToString();
            }
        }

        public static string ModelStringByItemDefinitionIndex(KNIFEINDEX id)
        {
            switch (id)
            {
                case KNIFEINDEX.KNIFE_BAYONET:
                    return "models/weapons/v_knife_bayonet.mdl";

                case KNIFEINDEX.KNIFE_FLIP:
                    return "models/weapons/v_knife_flip.mdl";
                case KNIFEINDEX.KNIFE_GUT:
                    return "models/weapons/v_knife_gut.mdl";
                case KNIFEINDEX.KNIFE_KARAMBIT:
                    return "models/weapons/v_knife_karam.mdl";
                case KNIFEINDEX.KNIFE_M9_BAYONET:
                    return "models/weapons/v_knife_m9_bay.mdl";
                case KNIFEINDEX.KNIFE_TACTICAL:
                    return "models/weapons/v_knife_tactical.mdl";
                case KNIFEINDEX.KNIFE_FALCHION:
                    return "models/weapons/v_knife_falchion_advanced.mdl";
                case KNIFEINDEX.KNIFE_SURVIVAL_BOWIE:
                    return "models/weapons/v_knife_survival_bowie.mdl";
                case KNIFEINDEX.KNIFE_BUTTERFLY:
                    return "models/weapons/v_knife_butterfly.mdl";
                case KNIFEINDEX.KNIFE_PUSH:
                    return "models/weapons/v_knife_push.mdl";
                case KNIFEINDEX.KNIFE_SURVIVAL:
                    return "models/weapons/v_knife_canis.mdl";
                case KNIFEINDEX.KNIFE_URSUS:
                    return "models/weapons/v_knife_ursus.mdl";
                case KNIFEINDEX.KNIFE_GYPSY_JACKKNIFE:
                    return "models/weapons/v_knife_gypsy_jackknife.mdl";
                case KNIFEINDEX.KNIFE_NOMAD:
                    return "models/weapons/v_knife_outdoor.mdl";
                case KNIFEINDEX.KNIFE_STILETTO:
                    return "models/weapons/v_knife_stiletto.mdl";
                case KNIFEINDEX.KNIFE_WIDOWMAKER:
                    return "models/weapons/v_knife_widowmaker.mdl";
                case KNIFEINDEX.KNIFE_SKELETON:
                    return "models/weapons/v_knife_skeleton.mdl";
                default:
                    return "models/weapons/v_knife_bayonet.mdl";
            }
        }

        public static WeaponClass GetWeaponType(ItemDefinitionIndex ID)
        {
            switch (ID)
            {
                case ItemDefinitionIndex.DEAGLE:
                case ItemDefinitionIndex.DUALBERETTAS:
                case ItemDefinitionIndex.FIVESEVEN:
                case ItemDefinitionIndex.GLOCK18:
                case ItemDefinitionIndex.TEC9:
                case ItemDefinitionIndex.USPS:
                case ItemDefinitionIndex.P2000:
                case ItemDefinitionIndex.P250:
                case ItemDefinitionIndex.CZ75AUTO:
                case ItemDefinitionIndex.R8Revolver:
                    return WeaponClass.PISTOL;

                // Rifles
                case ItemDefinitionIndex.AK47:
                case ItemDefinitionIndex.AUG:
                case ItemDefinitionIndex.FAMAS:
                case ItemDefinitionIndex.GALILAR:
                case ItemDefinitionIndex.M4A4:
                case ItemDefinitionIndex.M4A1S:
                case ItemDefinitionIndex.SG553:
                case ItemDefinitionIndex.BROKENM4A1S:
                    return WeaponClass.RIFLE;

                // Snipers
                case ItemDefinitionIndex.AWP:
                case ItemDefinitionIndex.G3SG1:
                case ItemDefinitionIndex.SCAR20:
                case ItemDefinitionIndex.SSG08:
                    return WeaponClass.SNIPER;
                // Heavy
                case ItemDefinitionIndex.M249:
                case ItemDefinitionIndex.XM1014:
                case ItemDefinitionIndex.MAG7:
                case ItemDefinitionIndex.NEGEV:
                case ItemDefinitionIndex.SAWEDOFF:
                case ItemDefinitionIndex.NOVA:
                    return WeaponClass.HEAVY;

                // SMGs
                case ItemDefinitionIndex.MAC10:
                case ItemDefinitionIndex.P90:
                case ItemDefinitionIndex.UMP45:
                case ItemDefinitionIndex.PPBIZON:
                case ItemDefinitionIndex.MP7:
                case ItemDefinitionIndex.MP9:
                case ItemDefinitionIndex.MP5SD:
                    return WeaponClass.SMG;

                case ItemDefinitionIndex.KNIFE:
                case ItemDefinitionIndex.KNIFE_T:
                case ItemDefinitionIndex.KNIFE_BAYONET:
                case ItemDefinitionIndex.KNIFE_FLIP:
                case ItemDefinitionIndex.KNIFE_GUT:
                case ItemDefinitionIndex.KNIFE_KARAMBIT:
                case ItemDefinitionIndex.KNIFE_M9_BAYONET:
                case ItemDefinitionIndex.KNIFE_TACTICAL:
                case ItemDefinitionIndex.KNIFE_FALCHION:
                case ItemDefinitionIndex.KNIFE_SURVIVAL_BOWIE:
                case ItemDefinitionIndex.KNIFE_BUTTERFLY:
                case ItemDefinitionIndex.KNIFE_PUSH:
                case ItemDefinitionIndex.KNIFE_SKELETON:
                case ItemDefinitionIndex.KNIFE_STILETTO:
                case ItemDefinitionIndex.KNIFE_SURVIVAL:
                case ItemDefinitionIndex.KNIFE_URSUS:
                case ItemDefinitionIndex.KNIFE_WIDOWMAKER:
                    return WeaponClass.KNIFE;
                default:
                    return WeaponClass.OTHER;
            }
        }

        public static BaseEntity CreateFromClassId(IntPtr _entityAdress, ClientClass _classId)
        {
            //Console.WriteLine(_classId.ToString());
            if (_entityAdress == IntPtr.Zero)
                return null;
            switch (_classId)
            {
                case ClientClass.CCSPlayer:
                    return new BasePlayer(_entityAdress, _classId);
                case ClientClass.CAK47:
                case ClientClass.CWeaponAug:
                case ClientClass.CWeaponAWP:
                case ClientClass.CWeaponBaseItem:
                case ClientClass.CWeaponBizon:
                case ClientClass.CWeaponCSBase:
                case ClientClass.CWeaponCSBaseGun:
                case ClientClass.CWeaponCycler:
                case ClientClass.CDEagle:
                case ClientClass.CWeaponElite:
                case ClientClass.CWeaponFamas:
                case ClientClass.CWeaponFiveSeven:
                case ClientClass.CWeaponG3SG1:
                case ClientClass.CWeaponGalil:
                case ClientClass.CWeaponGalilAR:
                case ClientClass.CWeaponGlock:
                case ClientClass.CWeaponHKP2000:
                case ClientClass.CWeaponM249:
                case ClientClass.CWeaponM3:
                case ClientClass.CWeaponM4A1:
                case ClientClass.CWeaponMAC10:
                case ClientClass.CWeaponMag7:
                case ClientClass.CWeaponMP5Navy:
                case ClientClass.CWeaponMP7:
                case ClientClass.CWeaponMP9:
                case ClientClass.CWeaponNegev:
                case ClientClass.CWeaponNOVA:
                case ClientClass.CWeaponP228:
                case ClientClass.CWeaponP250:
                case ClientClass.CWeaponP90:
                case ClientClass.CWeaponSawedoff:
                case ClientClass.CSCAR17:
                case ClientClass.CWeaponSCAR20:
                case ClientClass.CWeaponScout:
                case ClientClass.CWeaponSG550:
                case ClientClass.CWeaponSG552:
                case ClientClass.CWeaponSG556:
                case ClientClass.CWeaponShield:
                case ClientClass.CWeaponSSG08:
                case ClientClass.CWeaponTaser:
                case ClientClass.CWeaponTec9:
                case ClientClass.CWeaponTMP:
                case ClientClass.CWeaponUMP45:
                case ClientClass.CWeaponUSP:
                case ClientClass.CWeaponXM1014:
                case ClientClass.CKnife:
                case ClientClass.CKnifeGG:
                    return new BaseCombatWeapon(_entityAdress, _classId);
                case ClientClass.ParticleSmokeGrenade:
                    return new ParticleSmokeGrenade(_entityAdress, _classId);
                case ClientClass.CC4:
                    return new BombEntity(_entityAdress, _classId);
                case ClientClass.CPlantedC4:
                    return new PlantedBombEntity(_entityAdress, _classId);
                case ClientClass.CBaseCSGrenade:
                case ClientClass.CBaseGrenade:
                case ClientClass.CDecoyGrenade:
                case ClientClass.CFlashbang:
                case ClientClass.CIncendiaryGrenade:
                case ClientClass.CHEGrenade:
                case ClientClass.CMolotovGrenade:
                case ClientClass.CSmokeGrenade:
                case ClientClass.CSensorGrenade:
                    return new BaseGrenade(_entityAdress, _classId);
                case ClientClass.CBaseCSGrenadeProjectile:
                case ClientClass.CBreachChargeProjectile:
                case ClientClass.CBumpMineProjectile:
                case ClientClass.CDecoyProjectile:
                case ClientClass.CMolotovProjectile:
                case ClientClass.CSensorGrenadeProjectile:
                case ClientClass.CSmokeGrenadeProjectile:
                case ClientClass.CSnowballProjectile:
                case ClientClass.CTEClientProjectile:
                    return new ProjectileEntity(_entityAdress, _classId);
                case ClientClass.CChicken:
                    return new ChickenEntity(_entityAdress, _classId);
                case ClientClass.CEnvTonemapController:
                    return new TonemapController(_entityAdress, _classId);
                case ClientClass.CEnvDOFController:
                    return new EnvDOFController(_entityAdress, _classId);
                case ClientClass.CEconEntity:
                    return new EconEntity(_entityAdress, _classId);
                case ClientClass.CFogController:
                    return new FogController(_entityAdress, _classId);
                case ClientClass.CSun:
                    var _sun = new Sun(_entityAdress, _classId);
                    //if (!_sun.m_dwSunOverlayPrimary.IsValid || !_sun.m_dwSunOverlaySecondary.IsValid)
                    //    return null;

                    return _sun;
                case ClientClass.CCascadeLight:
                    return new CascadeLight(_entityAdress, _classId);
                case ClientClass.CColorCorrection:
                    return new ColorCorrection(_entityAdress, _classId);
                case ClientClass.CEnvWind:
                    return new Wind(_entityAdress, _classId);
                case ClientClass.CPredictedViewModel:
                    return new PredictedViewModel(_entityAdress, _classId);
                //case vmtClassId.ra
                case ClientClass.CShadowControl:
                    return new ShadowControl(_entityAdress, _classId);
                case ClientClass.CPostProcessController:
                    return new PostProcessController(_entityAdress, _classId);
                case ClientClass.CSprite:
                    return new Sprite(_entityAdress, _classId);
                case ClientClass.CInferno:
                    return new Inferno(_entityAdress, _classId);
                case ClientClass.CCSRagdoll:
                case ClientClass.CRagdollManager:
                case ClientClass.CRagdollProp:
                case ClientClass.CRagdollPropAttached:
                case ClientClass.CRopeKeyframe:
                case ClientClass.CFuncBrush:
                case ClientClass.CBaseEntity:
                case ClientClass.CBaseButton:
                case ClientClass.CDynamicProp:
                case ClientClass.CAI_BaseNPC:
                    return null;
                case ClientClass.CBeam:
                    return new Beam(_entityAdress, _classId);
                case ClientClass.CSpotlightEnd:
                    return new BeamEnd(_entityAdress, _classId);
                case ClientClass.CCSPlayerResource:
                    return null;

                default:
                    return new BaseEntity(_entityAdress, _classId);
            }
        }

        public static bool ValidClientClass(ClientClass _c)
        {
            switch (_c)
            {
                case ClientClass.CCSPlayer:
                case ClientClass.CAK47:
                case ClientClass.CWeaponAug:
                case ClientClass.CWeaponAWP:
                case ClientClass.CWeaponBaseItem:
                case ClientClass.CWeaponBizon:
                case ClientClass.CWeaponCSBase:
                case ClientClass.CWeaponCSBaseGun:
                case ClientClass.CWeaponCycler:
                case ClientClass.CDEagle:
                case ClientClass.CWeaponElite:
                case ClientClass.CWeaponFamas:
                case ClientClass.CWeaponFiveSeven:
                case ClientClass.CWeaponG3SG1:
                case ClientClass.CWeaponGalil:
                case ClientClass.CWeaponGalilAR:
                case ClientClass.CWeaponGlock:
                case ClientClass.CWeaponHKP2000:
                case ClientClass.CWeaponM249:
                case ClientClass.CWeaponM3:
                case ClientClass.CWeaponM4A1:
                case ClientClass.CWeaponMAC10:
                case ClientClass.CWeaponMag7:
                case ClientClass.CWeaponMP5Navy:
                case ClientClass.CWeaponMP7:
                case ClientClass.CWeaponMP9:
                case ClientClass.CWeaponNegev:
                case ClientClass.CWeaponNOVA:
                case ClientClass.CWeaponP228:
                case ClientClass.CWeaponP250:
                case ClientClass.CWeaponP90:
                case ClientClass.CWeaponSawedoff:
                case ClientClass.CSCAR17:
                case ClientClass.CWeaponSCAR20:
                case ClientClass.CWeaponScout:
                case ClientClass.CWeaponSG550:
                case ClientClass.CWeaponSG552:
                case ClientClass.CWeaponSG556:
                case ClientClass.CWeaponShield:
                case ClientClass.CWeaponSSG08:
                case ClientClass.CWeaponTaser:
                case ClientClass.CWeaponTec9:
                case ClientClass.CWeaponTMP:
                case ClientClass.CWeaponUMP45:
                case ClientClass.CWeaponUSP:
                case ClientClass.CWeaponXM1014:
                case ClientClass.CKnife:
                case ClientClass.CKnifeGG:
                case ClientClass.ParticleSmokeGrenade:
                case ClientClass.CC4:
                case ClientClass.CPlantedC4:
                case ClientClass.CBaseCSGrenade:
                case ClientClass.CBaseGrenade:
                case ClientClass.CDecoyGrenade:
                case ClientClass.CFlashbang:
                case ClientClass.CIncendiaryGrenade:
                case ClientClass.CHEGrenade:
                case ClientClass.CMolotovGrenade:
                case ClientClass.CSmokeGrenade:
                case ClientClass.CSensorGrenade:
                case ClientClass.CBaseCSGrenadeProjectile:
                case ClientClass.CBreachChargeProjectile:
                case ClientClass.CBumpMineProjectile:
                case ClientClass.CDecoyProjectile:
                case ClientClass.CMolotovProjectile:
                case ClientClass.CSensorGrenadeProjectile:
                case ClientClass.CSmokeGrenadeProjectile:
                case ClientClass.CSnowballProjectile:
                case ClientClass.CTEClientProjectile:
                case ClientClass.CChicken:
                case ClientClass.CEnvTonemapController:
                case ClientClass.CEnvDOFController:
                case ClientClass.CEconEntity:
                case ClientClass.CFogController:
                case ClientClass.CSun:
                case ClientClass.CCascadeLight:
                case ClientClass.CColorCorrection:
                case ClientClass.CEnvWind:
                case ClientClass.CPredictedViewModel:
                case ClientClass.CShadowControl:
                case ClientClass.CPostProcessController:
                case ClientClass.CSprite:
                case ClientClass.CInferno:
                case ClientClass.CBeam:
                case ClientClass.CSpotlightEnd:
                    return true;
                case ClientClass.CCSPlayerResource:
                case ClientClass.CCSRagdoll:
                case ClientClass.CRagdollManager:
                case ClientClass.CRagdollProp:
                case ClientClass.CRagdollPropAttached:
                case ClientClass.CRopeKeyframe:
                case ClientClass.CFuncBrush:
                case ClientClass.CBaseEntity:
                case ClientClass.CBaseButton:
                case ClientClass.CDynamicProp:
                case ClientClass.CAI_BaseNPC:

                default:
                    return false;
            }
        }

        /// <summary>
        /// Offsets must be the same name as the variable or otherwise reflection wont find it.
        /// </summary>
        /// <returns></returns>
        internal static ModulePattern[] CreateModulePattern()
        {
            return new ModulePattern[]
            {
               new ModulePattern
                {
                ModuleName = "client.dll",
                Patterns = new SerialPattern[]
                {
                    new SerialPattern
                    {
                        Name = "dwViewMatrix",
                        Pattern = "0F 10 05 ? ? ? ? 8D 85 ? ? ? ? B9",
                        Offset = new int[] { 3 },
                        Extra = 176

                    },
                    new SerialPattern
                    {
                        Name = "dwEntityList",
                        Pattern = "BB ? ? ? ? 83 FF 01 0F 8C ? ? ? ? 3B F8",
                        Offset = new int[] {0x1 },
                        Extra = 0
                    },
                    new SerialPattern
                    {
                        Name = "dwGameRulesProxy",
                        Pattern = "A1 ? ? ? ? 85 C0 0F 84 ? ? ? ? 80 B8 ? ? ? ? ? 74 7A",
                        Offset = new int[] {1 },
                        Extra = 0
                    },
                    new SerialPattern
                    {
                        Name = "dwGlowObjectManager",
                        Pattern = "A1 ? ? ? ? A8 01 75 4B",
                        Offset = new int[] {0x1 },
                        Extra = 4
                    },
                    new SerialPattern
                    {
                        Name = "dwRadarBase",
                        Pattern = "A1 ? ? ? ? 8B 0C B0 8B 01 FF 50 ? 46 3B 35 ? ? ? ? 7C EA 8B 0D",
                        Offset = new int[] {0x1 },
                        Extra = 0
                    },
                    new SerialPattern
                    {
                        Name = "dwForceJump",
                        Pattern = "8B 0D ? ? ? ? 8B D6 8B C1 83 CA 02",
                        Offset = new int[] {0x2 },
                        Extra = 0
                    },
                    new SerialPattern
                    {
                        Name = "dwForceAttack",
                        Pattern = "89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04",
                        Offset = new int[] {0x2 },
                        Extra = 0
                    },
                    new SerialPattern
                    {
                        Name ="dwForceGlow",
                        Pattern = "74 07 8B CB E8 ? ? ? ? 83 C7 10",
                        Extra = 0,
                        Offset = new int[] { 0 },
                        Relative = true,
                        SubtractOnly = true

                    },
                    new SerialPattern
                    {
                        Name = "m_dwGetAllClasses",
                        Pattern = "A1 ? ? ? ? C3 CC CC CC CC CC CC CC CC CC CC A1 ? ? ? ? B9",
                        Relative = true,
                        Offset = new int[] { 1, 0 }
                    }
                }
               },
            new ModulePattern
            {
                ModuleName = "engine.dll",
                Patterns = new SerialPattern[]
        {
            new SerialPattern
                    {
                        Name = "dwClientState",
                        Pattern = "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0",
                        Offset = new int[] {1 },
                        Extra = 0

                    },
                    new SerialPattern
                    {
                        Name = "dwModelPrecacheTable",
                        Pattern = "8B 8E ? ? ? ? 8B D0 85 C9",
                        Offset = new int[] {2 },
                        Extra = 0,
                        Relative = false

                    },
                    new SerialPattern
                    {
                        Name = "dwGlobalVars",
                        Pattern = "68 ? ? ? ? 68 ? ? ? ? FF 50 08 85 C0",
                        Offset = new int[] {1 },
                        Extra = 0

                    },
                    new SerialPattern
                    {
                        Name = "dwPlayerInfo",
                        Pattern = "8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01",
                        Offset = new int[] {2 },
                        Extra = 0,
                        Relative = false
                    },
                     new SerialPattern
                    {
                        Name = "dwClientState_ViewAngles",
                        Pattern = "F3 0F 11 86 ? ? ? ? F3 0F 10 44 24 ? F3 0F 11 86",
                        Offset = new int[] {4 },
                        Extra = 0,
                        Relative = false
                    },
                                          new SerialPattern
                    {
                        Name = "m_dwLocalPlayerIndex",
                        Pattern = "8B 80 ? ? ? ? 40 C3",
                        Offset = new int[] {2 },
                        Extra = 0,
                        Relative = false
                    },
                                          new SerialPattern
                                          {
                                              Name = "dwClientState_MaxPlayer",
                                              Pattern = "A1 ? ? ? ? 8B 80 ? ? ? ? C3 CC CC CC CC 55 8B EC 8A 45 08",
                                              Offset = new int[] {2 },
                                              Relative = false
                                          },
                                          new SerialPattern
                                          {
                                              Name = "dwClientState_MapDirectory",
                                              Pattern = "B8 ? ? ? ? C3 05 ? ? ? ? C3",
                                              Offset = new int[] {7 },
                                              Relative = false
                                          },
                                          new SerialPattern
                                          {
                                              Name = "dwClientState_Map",
                                              Pattern = "05 ? ? ? ? C3 CC CC CC CC CC CC CC A1",
                                              Offset = new int[] {1 },
                                              Relative = false
                                          }
                }
            },
            new ModulePattern
            {
                ModuleName = "vstdlib.dll",
                Patterns = new SerialPattern[]
                {
                    new SerialPattern
                    {
                        Name = "m_engineCvar",
                        Pattern = "8B 0D ? ? ? ? C7 05",
                        Offset = new int[] {2 },
                        Extra = 0

                    },
                    new SerialPattern
                    {
                        Name = "m_dwConvarTable",
                        Pattern = "8B 3C 85",
                        Offset = new int[] {3 },
                        Extra = 0
                    }
                }
            }
            };

        }


        public static SharpDX.Color ColorByHealth(int h)
        {
            if (h > 50)
                return SharpDX.Color.ForestGreen;
            if (h <= 50 && h > 25)
                return SharpDX.Color.Yellow;
            return SharpDX.Color.Red;
        }
        public static SharpDX.Vector3 ToVector(float a, float b, float c)
        {
            return new SharpDX.Vector3(a, b, c);
        }
        public static float[] ToFloat(SharpDX.Vector3 _vector)
        {
            return new float[] { _vector.X, _vector.Y, _vector.Z };
        }
    }
}
