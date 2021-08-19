using AudioMagic.Audio;
using ResurrectedEternal.Configs.ConfigSystem;
using ResurrectedEternal.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.SoundEngine
{
    public static class SoundCache
    {
        private static Dictionary<string, string[]> GenericSoundFiles = new Dictionary<string, string[]>();

        private static Dictionary<int, string[]> KillSounds = new Dictionary<int, string[]>();

        private static Dictionary<string, string[]> AmbientSounds = new Dictionary<string, string[]>();

        private static Dictionary<int, string[]> HeadStreakSounds = new Dictionary<int, string[]>();

        private static string SoundCacheDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\sounds\\";

        private static AudioPlaybackEngine SoundEngine = new AudioPlaybackEngine(false);

        private static AudioPlaybackEngine AmbientEngine = new AudioPlaybackEngine(false);

        private static bool _enabled = false;

        static SoundCache()
        {
            Load();
        }


        private static void Load()
        {
            if (!System.IO.Directory.Exists(SoundCacheDirectory))
                return;

            if (System.IO.File.Exists(g_Globals.SoundConfig))
            {
                _enabled = true;
                try
                {
                    var _des = Serializer.LoadJson<Quakesound>(g_Globals.SoundConfig);
                    foreach (var item in _des.GenericSounds)
                    {
                        string[] _sounds = new string[item.Value.Length];
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            var _item = SoundCacheDirectory + item.Value[i];
                            if (System.IO.File.Exists(_item))
                            {
                                _sounds[i] = _item;
                                SoundEngine.AddCacheFile(_item);
                            }
                            else
                            {
                                Console.WriteLine("Could not find sound file " + _item);
                            }

                        }

                        GenericSoundFiles.Add(item.Key, _sounds);
                    }
                    foreach (var item in _des.KillStreakSounds)
                    {
                        string[] _sounds = new string[item.Files.Length];
                        for (int i = 0; i < item.Files.Length; i++)
                        {
                            var _item = SoundCacheDirectory + item.Files[i];
                            if (System.IO.File.Exists(_item))
                            {
                                _sounds[i] = _item;
                                SoundEngine.AddCacheFile(_item);
                            }
                            else
                            {
                                Console.WriteLine("Could not find sound file " + _item);
                            }
                        }
                        KillSounds.Add(item.NumKillRequired, _sounds);

                    }
                    foreach (var item in _des.AmbientSounds)
                    {
                        string[] _sounds = new string[item.Files.Length];
                        for (int i = 0; i < item.Files.Length; i++)
                        {
                            var _item = SoundCacheDirectory + item.Files[i];
                            if (System.IO.File.Exists(_item))
                            {
                                _sounds[i] = _item;
                            AmbientEngine.AddCacheFile(_item);
                            }
                            else
                            {
                                Console.WriteLine("Could not find sound file " + _item);
                            }
                        }
                        AmbientSounds.Add(item.AmbientName, _sounds);
                    }
                    foreach (var item in _des.HeadStreakSounds)
                    {
                        string[] _sounds = new string[item.Files.Length];
                        for (int i = 0; i < item.Files.Length; i++)
                        {
                            var _item = SoundCacheDirectory + item.Files[i];
                            if (System.IO.File.Exists(_item))
                            {
                                _sounds[i] = _item;
                                SoundEngine.AddCacheFile(_item);
                            }
                            else
                            {
                                Console.WriteLine("Could not find sound file " + _item);
                            }
                        }
                        HeadStreakSounds.Add(item.NumKillRequired, _sounds);
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not load sound configuration file! disabling sounds!");
                    Console.ForegroundColor = ConsoleColor.White;
                    _enabled = false;
                }

                //iterate this shit
                return;
            }

            var _quakesound = new Quakesound();
            _quakesound.Create();
            Serializer.SaveJson(_quakesound, g_Globals.SoundConfig);
            //System.IO.File.WriteAllText(SoundCacheDirectory + "config.json", _content);
            Load();

        }

        private static Random _random = new Random();
        public static void PlaySound(int killIndex)
        {
            if (!_enabled)
                return;
            if (!(bool)g_Globals.Config.ExtraConfig.Sound.Value)
                return;
            if (!KillSounds.ContainsKey(killIndex))
                return;
            var _num = _random.Next(0, KillSounds[killIndex].Length);
            if (KillSounds[killIndex].Length > 0)
                SoundEngine.PlaySound(KillSounds[killIndex][_num]);
            //_soundEngine.Play2D(, false, false, true);
        }

        public static void PlayHeadshot(int hsidx)
        {
            if (!_enabled)
                return;
            if (!(bool)g_Globals.Config.ExtraConfig.Sound.Value)
                return;
            if (!HeadStreakSounds.ContainsKey(hsidx))
                return;
            var _num = _random.Next(0, HeadStreakSounds[hsidx].Length);
            if (HeadStreakSounds[hsidx].Length > 0)
                SoundEngine.PlaySound(HeadStreakSounds[hsidx][_num]);
        }

        public static void PlayRandom(string index)
        {
            if (!_enabled)
                return;
            if (!(bool)g_Globals.Config.ExtraConfig.Sound.Value)
                return;
            if (!GenericSoundFiles.ContainsKey(index))
                return;

            //if (DateTime.Now - _lastSoundPlayed < MaxSpanBetweenSounds)
            //    return;
            //_lastSoundPlayed = DateTime.Now;
            var _num = _random.Next(0, GenericSoundFiles[index].Length);
            if (GenericSoundFiles[index].Length > 0)
                SoundEngine.PlaySound(GenericSoundFiles[index][_num]);
        }

        public static void PlayAmbiente(string index)
        {
            if (!_enabled || !(bool)g_Globals.Config.ExtraConfig.Sound.Value)
                return;
            _clampVolume = (float)g_Globals.Config.ExtraConfig.SoundVolume.Value;
            if (!AmbientSounds.ContainsKey(index))
                return;
            _stopped = false;
            var _num = _random.Next(0, AmbientSounds[index].Length);
            if (AmbientSounds[index].Length > 0)
                AmbientEngine.PlaySound(AmbientSounds[index][_num]);
        }

        private static float _clampVolume = 0f;
        private static bool _stopped = false;
        public static void FadeOutAmbiente()
        {
            if (!_enabled)
                return;

            if (_stopped)
                return;
            _stopped = true;
            AmbientEngine.StopAmbiente();

            //AmbientEngine.StopAmbiente();
            //_clampVolume -= 0.005f;
            //if (_clampVolume < 0)
            //    _clampVolume = 0;
            //m_dwAmbienteVolume = EngineMath.Clamp(_clampVolume, 0, 1);
        }

        //public static float m_dwAmbienteVolume
        //{
        //    get { return AmbientEngine.m_dwVolume; }
        //    set
        //    {
        //        if (!_enabled)
        //            return;
        //        AmbientEngine.m_dwVolume = EngineMath.Clamp(value, 0, 1);
        //    }
        //}

        public static float m_dwVolume
        {
            get { return SoundEngine.m_dwVolume; }
            set
            {
                if (!_enabled)
                    return;
                SoundEngine.m_dwVolume = EngineMath.Clamp(value, 0, 1);
            }
        }
    }
}
