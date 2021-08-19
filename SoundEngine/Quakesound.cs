using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.SoundEngine
{
    public class SoundFile
    {
        public int NumKillRequired;

        public string[] Files;
    }

    public class AmbientSoundFile
    {
        public string AmbientName;
        public string[] Files;
    }

    //public class KillStreak : SoundFile
    //{

    //}

    //public class ComboKillSound : SoundFile
    //{
    //    public int NumKillHeadshotRequired;
    //    public TimeSpan MinComboTime;
    //}

    public class Quakesound
    {
        public Dictionary<string, string[]> GenericSounds;

        //public string[] HitmarkerSounds;

        //public string[] HeadshotSounds;

        //public string[] RoundStartSounds;
        //public string[] RoundLossSounds;
        //public string[] RoundWinSounds;

        //public string[] ConnectedSounds;
        //public string[] DisconnectedSounds;

        //public string[] FirstBloodSounds;

        //public string[] DeathSounds;

        //public string[] TaserKillSounds;

        //public string[] LastManStandingSounds;

        public SoundFile[] KillStreakSounds;
        public SoundFile[] HeadStreakSounds;
        public AmbientSoundFile[] AmbientSounds;
        //public ComboKillSound[] ComboKillSounds;

        public void Create()
        {
            GenericSounds = new Dictionary<string, string[]>();

            GenericSounds.Add("hitmarker", new string[]
            {
                "hitmarker.mp3"
            });

            GenericSounds.Add("headshot", new string[]
            {
                "headshot.mp3",
                "headshot2.mp3",
                "headshot3.mp3",
                "headshot_fem.mp3",
            });
            GenericSounds.Add("roundstart", new string[]
            {
                "prepare.mp3",
                "prepare2.mp3",
                "prepare3.mp3",
                "prepare4.mp3",
                "prepare_fem.mp3",
                "ahshit.mp3",
                "go.mp3"
            });
            GenericSounds.Add("roundend_win", new string[]
            {
                "flawless.mp3",
                "perfect.mp3",
                "easy.mp3",

            });
            //only add small short juicy sounds to this since we have music already in the endsounds
            GenericSounds.Add("roundend_loss", new string[]
            {
                "ffs.mp3",
                                "baka.mp3",
                                "fuck.mp3"
            });
            //GenericSounds.Add("onwin_lastmanstanding", new string[]
            //{
            //    "johncena.mp3"
            //});
            GenericSounds.Add("roundend_ctwin", new string[]
            {
                "ctwinnar2.mp3",
                "ctwinnar3.mp3",
                "ctwinnar4.mp3",
                "murica.mp3"
            });
            GenericSounds.Add("roundend_twin", new string[]
{
                "twinnar.mp3",
                "twinnar2.mp3",
                "twinnar3.mp3",
                "akbar2.mp3"
});
            GenericSounds.Add("disconnected", new string[]
            {
                "bye.mp3",
                "maytheforce.mp3",
                "nope.mp3",
                "nosir.mp3",
                "discordleave.mp3",

            });
            GenericSounds.Add("connected", new string[]
            {
                "hi.mp3",
                "hellothere.mp3",
                "motherphucker.mp3",
                "hellokid.mp3",
                "mmmonkey.mp3",
                "arara.mp3",
                "yareyare.mp3",
                "jeff.mp3"
            });

            GenericSounds.Add("firstblood", new string[]
            {
                "firstblood.mp3",
                "firstblood2.mp3",
                "firstblood3.mp3",
                "firstblood_fem.mp3"
            });

            GenericSounds.Add("taser", new string[]
            {
                "suicide.mp3",
                "suicide2.mp3",
                "suicide3.mp3",
                "suicide4.mp3",
            });

            GenericSounds.Add("lastman", new string[]
            {
                "manstanding.mp3",
                "oneandonly.mp3",
                "getoverhere.mp3"
            });

            GenericSounds.Add("onplayerdeath", new string[]
            {
                "death.mp3",
                "death2.mp3",
                "hagay.mp3",
                "ough.mp3",
                "slap.mp3",
                "illu.mp3",
                "directedby.mp3",


            });

            GenericSounds.Add("onbombplanted", new string[]
            {
                "whygay.mp3",
                "wow.mp3",
                "plsgodno.mp3"
            });
            GenericSounds.Add("onbombdefused", new string[]
            {
                "clapping.mp3",
                "deeznuts.mp3",
                "nope.mp3",
                "notevenclose.mp3"
            });
            GenericSounds.Add("onbombabouttoexplode", new string[]
                {
                    "badfeeling.mp3",
                    "akbar.mp3",
                    "nonono.mp3",
                    "hereweaah.mp3",
                    "wbrb.mp3"
                });
            GenericSounds.Add("onjoinserver", new string[]
            {
                "joinserver.mp3",
                "ahshit.mp3"
            });
            GenericSounds.Add("onmatchended", new string[]
            {
                "whowantstobeamillionaire.mp3",
                "sadshit.mp3",
                "ph.mp3"
            });

            AmbientSounds = new AmbientSoundFile[]
            {
                new AmbientSoundFile
                {
                    AmbientName = "suspense",
                    Files = new string[]
                    {
                        "ambiente.mp3",
                        //"ambiente1.mp3",
                        "ambiente2.mp3",
                        "ambiente3.mp3",
                        "ambiente4.mp3",
                        "ambiente5.mp3",
                        "ambiente6.mp3",
                        "ambiente7.mp3",
                        "ambiente8.mp3",
                        "ambiente9.mp3",
                        "ambiente10.mp3",
                        "ambiente11.mp3",
                        "ambiente12.mp3",
                        "ambiente13.mp3",
                        "ambiente14.mp3",
                        "ambiente15.mp3",
                        "ambiente16.mp3",
                        "ambiente17.mp3",
                    }
                }
            };

            HeadStreakSounds = new SoundFile[]
            {
                new SoundFile
                {
                    Files = new string[]
                    {
                        "impressive.mp3"
                    },
                    NumKillRequired = 4,
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "combowhore.mp3"
                    },
                    NumKillRequired = 6,
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "hattrick.mp3"
                    },
                    NumKillRequired = 8,
                }
            };

            KillStreakSounds = new SoundFile[]
            {
                new SoundFile
                {
                    Files  = new string[]
                    {
                        "firstblood.mp3",
                        "firstblood_fem.mp3"
                    },
                    NumKillRequired = 1
                },

                new SoundFile
                {
                    Files = new string[]
                    {
                        "dominating.mp3",
                        "dominating_fem.mp3"
                    },
                    NumKillRequired = 3
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "rampage.mp3",
                        "rampage_fem.mp3"
                    },
                    NumKillRequired = 5
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "killingspree.mp3",
                        "killingspree_fem.mp3"
                    },
                    NumKillRequired = 7
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "monsterkill.mp3",
                        "monsterkill_fem.mp3"
                    },
                    NumKillRequired = 9
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "holyshit.mp3",
                        "holyshit_fem.mp3"
                    },
                    NumKillRequired = 11
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "godlike.mp3",
                        "godlike_fem.mp3"
                    },
                    NumKillRequired = 13
                },
                new SoundFile
                {
                    Files = new string[]
                    {
                        "wickedsick.mp3",
                        "wickedsick_fem.mp3"
                    },
                    NumKillRequired = 15
                },
            };
        }

    }
}
