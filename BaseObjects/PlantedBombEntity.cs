using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Memory;
using RRFull;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class PlantedBombEntity : BaseEntity
    {
        private bool Exploded = false;
        private bool PreExplosion = false;
        private bool IsDefused = false;

        public PlantedBombEntity(IntPtr addr, ClientClass cid) : base(addr, cid)
        {
            PlantTime = DateTime.Now;
            ExplodeTime = DateTime.Now.AddSeconds((long)m_flC4Blow);
            
        }

        public string m_szBombName
        {
            get { return "DA BOMB"; }
        }
        private bool WasTriggeredForPlanted = false;
        //private bool WasTriggeredPreExplosion = false;
        private string m_pszExplosionTime = "";
        public string m_szExplosionTime
        {
            get
            {
                float _time = Henker.Singleton.Engine.GlobalVars.m_flCurTime;
                var _f = m_flC4Blow - _time;
                //+"\n" + _b.m_flDefuseLength + "\n" + _b.m_flDefuseCountDown + "\n" + _b.m_iBombDefuser
                if (m_bBombTicking)
                {
                    if (!WasTriggeredForPlanted)
                    {
                        new BombEntityChangedEventArgs(BombEntityStateChange.Planted);
                        WasTriggeredForPlanted = true;
                    }
                    if (_f > 0)
                    {

                        m_pszExplosionTime = string.Format("{0} seconds remaining\n", _f.ToString("0.0"));
                        if (m_iBombDefuser < 4095)
                        {
                            var _d = m_flDefuseCountDown - _time;
                            var _p = Henker.Singleton.Client.GetPlayerById(m_iBombDefuser);
                            if (_p != null)
                            {
                                m_pszExplosionTime += _p.m_szPlayerName + " is defusing in ";
                            }

                            m_pszExplosionTime += string.Format("{0} seconds", _d.ToString("0.0"));
                        }

                    }
                    else
                    {
                        if(!PreExplosion)
                        {
                            PreExplosion = true;
                            new BombEntityChangedEventArgs(BombEntityStateChange.BeforeExplosion);
                        }
                        m_pszExplosionTime = "UH OH";
                    }
                        

                }
                else
                {
                    if (m_bBombDefused)
                    {
                        if(!IsDefused)
                        {
                            IsDefused = true;
                            new BombEntityChangedEventArgs(BombEntityStateChange.Defused);
                        }
                        m_pszExplosionTime = "DEFUSED!";

                    }

                    else
                    {
                        if (!Exploded)
                        {
                            Exploded = true;
                            new BombEntityChangedEventArgs(BombEntityStateChange.Exploded);
                        }
                        m_pszExplosionTime = "HOLOCAUSTED";
                    }
                }


                return m_pszExplosionTime;
            }
        }
        private DateTime PlantTime;
        private DateTime ExplodeTime;


        //pointer, dereference first -> entity baseadress?
        public bool m_bShouldGlow
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow, value); }
        }
        public bool m_bBombTicking
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bBombTicking); }
        }
        public int m_iBombSite
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_nBombSite); }
        }

        public float m_flC4Blow
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flC4Blow); }
        }
        public float m_flTimerLength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flTimerLength); }
        }
        public float m_flDefuseLength
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flDefuseLength); }
        }

        public float m_flDefuseCountDown
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flDefuseCountDown); }
        }

        public bool m_bBombDefused
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bBombDefused); }
        }

        public int m_iBombDefuser
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_hBombDefuser) & 0xFFF; }
        }


        //public int m_iGlowIndex
        //{
        //    get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + (int)g_Globals.Offset.m_iGlowIndex); }
        //}
    }
}
