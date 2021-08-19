using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{
    class SkillModNeon : SkillMod
    {
        private DateTime _lastGlowUpdate = DateTime.Now;
        private TimeSpan _Interval = TimeSpan.FromMilliseconds(25);

        private byte m_dwOrignialValue = 0x74;
        private byte m_dwForcedValue = 0xEB;

        public SkillModNeon(Engine engine, Client client) : base(engine, client)
        {
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        public override void Before()
        {
            base.Before();

        }
        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammode
                || ClientModus == Events.Modus.streammodefull)
                return false;
            return true;
        }


        private void WriteByIndex(m_dwGlowObject glowObject, int idx, SharpDX.Color color, bool visible)
        {
            SetGlowStruct(ref glowObject, color, visible);
            Write(glowObject, idx);
        }

        private void Write(m_dwGlowObject glowObject, int idx)
        {

            if (idx > Client.m_dwGlowManager.m_iNumObjects)
                return;
            IntPtr _addr = Client.m_dwGlowManager.m_pGlowArray + (idx * 0x38);

            MemoryLoader.instance.Reader.Write<Vector3>(_addr + 0x08,
                new Vector3(glowObject.R,
                glowObject.G,
                glowObject.B
               ));
            MemoryLoader.instance.Reader.Write<float>(_addr + 0x14, glowObject.A);
            MemoryLoader.instance.Reader.Write<float>(_addr + 0x1C, glowObject.m_flGlowAlphaFunctionOfMaxVelocity);
            MemoryLoader.instance.Reader.Write<float>(_addr + 0x20, glowObject.m_flGlowAlphaMax);
            MemoryLoader.instance.Reader.Write<bool>(_addr + 0x28, glowObject.bRenderWhenOccluded);
            MemoryLoader.instance.Reader.Write<bool>(_addr + 0x29, glowObject.bRenderWhenUnoccluded);
            MemoryLoader.instance.Reader.Write<bool>(_addr + 0x2A, glowObject.bFullBloom);
            MemoryLoader.instance.Reader.Write<byte>(_addr + 0x30, glowObject.m_nRenderStyle);

        }

        private void UnGlow(m_dwGlowObject glowObject, int idx)
        {
            if (glowObject.R == 0f && glowObject.G == 0f && glowObject.B == 0f && glowObject.A == 0f)
                return;

            if (idx > Client.m_dwGlowManager.m_iNumObjects)
                return;
            IntPtr _addr = Client.m_dwGlowManager.m_pGlowArray + (idx * 0x38);

            MemoryLoader.instance.Reader.Write<Vector3>(_addr + 0x08,
                new Vector3(0f,
                0f,
                0f
               ));
            MemoryLoader.instance.Reader.Write<float>(_addr + 0x14, 0f);
        }

        bool ShouldUpdate(m_dwGlowObject s, SharpDX.Color c)
        {
            if (!EngineMath.AlmostEquals(s.R, (c.R / 255f), 0.0001f))
                return true;
            if (!EngineMath.AlmostEquals(s.G, (c.G / 255f), 0.0001f))
                return true;
            if (!EngineMath.AlmostEquals(s.B, (c.B / 255f), 0.0001f))
                return true;
            if (!EngineMath.AlmostEquals(s.A, (c.A / 255f), 0.0001f))
                return true;
            if (s.m_nRenderStyle != Convert.ToByte(Config.NeonConfig.GlowStyle.Value))
                return true;
            if (s.bFullBloom != (bool)Config.NeonConfig.FullBloom.Value)
                return true;
            if (s.m_flGlowAlphaFunctionOfMaxVelocity != Convert.ToSingle(Config.NeonConfig.MaxVelocity.Value))
                return true;
            if (s.m_flGlowAlphaMax != (float)Config.NeonConfig.AlphaMax.Value)
                return true;

            return false;
        }

        void SetGlowStruct(ref m_dwGlowObject s, SharpDX.Color c, bool visible = false)
        {

            s.R = c.R / 255f;
            s.G = c.G / 255f;
            s.B = c.B / 255f;
            s.A = c.A / 255f;
            s.m_nRenderStyle = GetGlowStyle(visible);
            s.bRenderWhenOccluded = true;
            s.bRenderWhenUnoccluded = false;
            s.bFullBloom = (bool)Config.NeonConfig.FullBloom.Value;
            s.m_flGlowAlphaFunctionOfMaxVelocity = Convert.ToSingle(Config.NeonConfig.MaxVelocity.Value);
            s.m_flGlowAlphaMax = (float)Config.NeonConfig.AlphaMax.Value;

        }

        private byte GetGlowStyle(bool visible)
        {
            if ((GlowRenderStyle_t)Config.NeonConfig.GlowStyleWhenVisible.Value == GlowRenderStyle_t.GLOWRENDERSTYLE_DEFAULT)
                return Convert.ToByte(Config.NeonConfig.GlowStyle.Value);
            if (visible)
                return Convert.ToByte(Config.NeonConfig.GlowStyleWhenVisible.Value);
            else
                return Convert.ToByte(Config.NeonConfig.GlowStyle.Value);
        }


        private void UpdateGlows()
        {

            var _glowManager = Client.m_dwGlowManager;
            //m_dwGlowObject[] glowObjects = new m_dwGlowObject[_glowManager.m_iNumObjects];
            var _num = _glowManager.m_iNumObjects;
            var _ptr = _glowManager.m_pGlowArray;

            //var _r = MemoryLoader.instance.Reader.Read<m_dwGlowObject>(_ptr, glowObjects.Length);

            for (int i = 0; i < _num; i++)
            {
                var _next = MemoryLoader.instance.Reader.Read<m_dwGlowObject>(_ptr + (i * 0x38));
                if (_next.dwEntity == IntPtr.Zero) continue;
                var _ent = Client.GetEntityByAddress(_next.dwEntity);
                if (_ent == null || !_ent.IsValid || _ent.m_bDormant || _ent.m_vecOrigin == Vector3.Zero) continue;

                var _color = (Color)Config.OtherConfig.cFontColor.Value;
                bool _vis = false;
                if (_ent.ClientClass == ClientClass.CCSPlayer)
                {
                    var bp = _ent as BasePlayer;
                    _vis = bp.IsVisible;
                    if (!bp.m_bIsActive) continue;

                    switch ((TargetType)Config.NeonConfig.GlowAt.Value)
                    {

                        case TargetType.Enemy:
                            if (bp.IsFriendly(Client.LocalPlayer.Team))
                            {
                                UnGlow(_next, i);
                                continue;
                            }
                                
                            _color = Generators.GetNeonColorBySetting(Config, bp.Team, _vis, bp.m_bGunGameImmunity);
                            break;
                        case TargetType.Friendly:
                            if (!bp.IsFriendly(Client.LocalPlayer.Team))
                            {
                                UnGlow(_next, i);
                                continue;
                            }
                            _color = Generators.GetNeonColorBySetting(Config, bp.Team, _vis, bp.m_bGunGameImmunity);
                            break;
                        case TargetType.All:
                        default:
                            _color = Generators.GetNeonColorBySetting(Config, bp.Team, _vis, bp.m_bGunGameImmunity);
                            break;
                    }


                }
                else if (Generators.IsGrenade(_ent.ClientClass) && (bool)Config.NeonConfig.NeonGlowGrenades.Value)
                {
                   // var _t = _ent as BaseGrenade;
                    _color = (Color)Config.OtherConfig.cGrenadeColor.Value;
                }
                else if (Generators.IsProjectile(_ent.ClientClass) && (bool)Config.NeonConfig.NeonGlowProjectiles.Value)
                {
                   // var _t = _ent as ProjectileEntity;
                    _color = (Color)Config.OtherConfig.cProjectileColor.Value;
                }
                else if (Generators.IsWeapon(_ent.ClientClass) && (bool)Config.NeonConfig.NeonGlowWeapons.Value)
                {
                    //var _t = _ent as BaseCombatWeapon;
                    _color = (Color)Config.OtherConfig.cWeaponColor.Value;
                }
                else if (_ent.ClientClass == ClientClass.CChicken && (bool)Config.NeonConfig.NeonGlowChickens.Value)
                {
                    _color = (Color)Config.OtherConfig.cChickenColor.Value;
                }
                else if (_ent.ClientClass == ClientClass.CEconEntity && (bool)Config.NeonConfig.NeonGlowDefuse.Value)
                {
                    _color = (Color)Config.OtherConfig.cDefuseKitColor.Value;
                }
                else if ((_ent.ClientClass == ClientClass.CC4
                    || _ent.ClientClass == ClientClass.CPlantedC4) && (bool)Config.NeonConfig.NeonGlowBomb.Value)
                {
                    _color = (Color)Config.OtherConfig.cBombColor.Value;
                }
                else
                    continue;

                if (ShouldUpdate(_next, _color))
                {
                    //ILog.AddToLog("[GLOW]Prepare Write", _ent.BaseAddress + " " + _ent.ClientClass.ToString());
                    WriteByIndex(_next, i, _color, _vis);
                }
                    
            }
        }

        public override bool Update()
        {
            //OHNE ANGST BABY
            if (!CanProcess())
                return false;
            if (!Client.UpdateModules || Client.DontGlow || !Engine.IsInGame || Client.LocalPlayer == null || !Client.LocalPlayer.IsValid || Client.LocalPlayer.m_bIsSpectator )
                return false;

            if (DateTime.Now - _lastGlowUpdate < _Interval)
                return false;


            if ((bool)Config.NeonConfig.Enable.Value)
            {
                EnableForce();
                UpdateGlows();
                _lastGlowUpdate = DateTime.Now;

            }
            else
            {
                DisableForce();
            }
            return true;
        }

        private void DisableForce()
        {
            if (MemoryLoader.instance.Reader.Read<byte>(Client.ModuleAddress + g_Globals.Offset.dwForceGlow) == m_dwOrignialValue)
                return;
            MemoryLoader.instance.Reader.Write<byte>(Client.ModuleAddress + g_Globals.Offset.dwForceGlow, m_dwOrignialValue);
        }

        private void EnableForce()
        {
            if (MemoryLoader.instance.Reader.Read<byte>(Client.ModuleAddress + g_Globals.Offset.dwForceGlow) == m_dwForcedValue)
                return;
            MemoryLoader.instance.Reader.Write<byte>(Client.ModuleAddress + g_Globals.Offset.dwForceGlow, m_dwForcedValue);
        }
    }
}
