using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.EnvironmentSkillMods
{
    class SkillModSpriteController : SkillMod
    {
        private DateTime m_dtLastUpdate = DateTime.Now;
        private TimeSpan m_tsInterval = TimeSpan.FromMilliseconds(333);
        public SkillModSpriteController(Engine engine, Client client) : base(engine, client)
        {
        }

        private void UpdateBeams()
        {
            if (!(bool)Config.EnvironmentConfig.OverrideBeams.Value)
                return;

            var _beams = Client.GetBeams();

            if (_beams.Count() == 0)
                return;

            foreach (var item in _beams)
            {
                if (item.IsValid)
                    SetBeam(item);
            }
        }

        private void SetBeam(Beam m_dwBeam)
        {
            if (m_dwBeam.m_fWidth != (float)Config.EnvironmentConfig.BeamWidth.Value)
                m_dwBeam.m_fWidth = (float)Config.EnvironmentConfig.BeamWidth.Value;
            if (m_dwBeam.m_fEndWidth != (float)Config.EnvironmentConfig.BeamEndWidth.Value)
                m_dwBeam.m_fEndWidth = (float)Config.EnvironmentConfig.BeamEndWidth.Value;
            //if (m_dwBeam.m_nHaloIndex != Convert.ToByte(Config.EnvironmentConfig.BeamHaloIndex.Value))
            //    m_dwBeam.m_nHaloIndex = Convert.ToByte(Config.EnvironmentConfig.BeamHaloIndex.Value);
            if (m_dwBeam.m_fHaloScale != (float)Config.EnvironmentConfig.BeamHaloScale.Value)
                m_dwBeam.m_fHaloScale = (float)Config.EnvironmentConfig.BeamHaloScale.Value;
            if (m_dwBeam.m_fFadeLength != (float)Config.EnvironmentConfig.BeamFadeLength.Value)
                m_dwBeam.m_fFadeLength = (float)Config.EnvironmentConfig.BeamFadeLength.Value;
            if (m_dwBeam.m_fAmplitude != (float)Config.EnvironmentConfig.BeamAmplitude.Value)
                m_dwBeam.m_fAmplitude = (float)Config.EnvironmentConfig.BeamAmplitude.Value;
            if (m_dwBeam.m_fSpeed != (float)Config.EnvironmentConfig.BeamSpeed.Value)
                m_dwBeam.m_fSpeed = (float)Config.EnvironmentConfig.BeamSpeed.Value;
            if (m_dwBeam.m_clrRender != (SharpDX.Color)Config.EnvironmentConfig.BeamColor.Value)
                m_dwBeam.m_clrRender = (SharpDX.Color)Config.EnvironmentConfig.BeamColor.Value;
            if (m_dwBeam.m_flBeamHDRColorScale != (float)Config.EnvironmentConfig.BeamHDRScale.Value)
                m_dwBeam.m_flBeamHDRColorScale = (float)Config.EnvironmentConfig.BeamHDRScale.Value;
            //m_dwBeam.m_clrRender = new Color(255, 0, 0, 255);
            //m_dwBeam.m_flBeamHDRColorScale = 15f;
            //m_dwBeam.m_fHaloScale = 7f;
            //m_dwBeam.m_nBeamFlags = Beam_Flags_h.NUM_BEAM_FLAGS;
            //m_dwBeam.m_nBeamType = BeamType.NUM_BEAM_TYPES;
            //m_dwBeam.m_fAmplitude = 200f;
            //m_dwBeam.m_fWidth = 90f;
            //m_dwBeam.m_fEndWidth = 666f;
            //m_dwBeam.m_fFadeLength = 500f;
        }

        private void UpdateSprites()
        {
            if (!(bool)Config.EnvironmentConfig.OverrideSprites.Value)
                return;

            var _sprites = Client.GetSprites();

            if (_sprites.Count() == 0)
                return;

            foreach (var sprite in _sprites)
            {
                if (sprite.IsValid)
                    SetSprite(sprite);
            }
        }

        private void SetSprite(Sprite m_dwSprite)
        {
            if (m_dwSprite.m_flSpriteScale != (float)Config.EnvironmentConfig.SpriteScale.Value)
                m_dwSprite.m_flSpriteScale = (float)Config.EnvironmentConfig.SpriteScale.Value;
            if (m_dwSprite.m_flGlowProxySize != (float)Config.EnvironmentConfig.GlowProxySize.Value)
                m_dwSprite.m_flGlowProxySize = (float)Config.EnvironmentConfig.GlowProxySize.Value;
            if (m_dwSprite.m_nBrightness != Convert.ToByte(Config.EnvironmentConfig.SpriteBrightness.Value))
                m_dwSprite.m_nBrightness = Convert.ToByte(Config.EnvironmentConfig.SpriteBrightness.Value);
            if (m_dwSprite.m_clrRender != (SharpDX.Color)Config.EnvironmentConfig.SpriteColor.Value)
                m_dwSprite.m_clrRender = (SharpDX.Color)Config.EnvironmentConfig.SpriteColor.Value;
            if (m_dwSprite.m_flHDRColorScale != (float)Config.EnvironmentConfig.HDRColorScale.Value)
                m_dwSprite.m_flHDRColorScale = (float)Config.EnvironmentConfig.HDRColorScale.Value;
        }

        public override void AfterUpdate()
        {
            if (!Client.UpdateModules || Client.LocalPlayer == null || !Engine.IsInGame || !Client.UpdateModules)
                return;

            if (DateTime.Now - m_dtLastUpdate < m_tsInterval)
                return;
            //var _beams = Client.GetBeams();

            //var _beamEnds = Client.GetBeamEnds();

            //foreach (var item in _beamEnds)
            //{
            //    item.m_clrRender = new Color(9, 0, 255, 255);
            //    item.m_flLightScale = 71f;
            //    item.m_Radius = 90f;
            //}

            //foreach (var item in _beams)
            //{
            //    item.m_clrRender = new Color(255, 0, 0, 255);
            //    item.m_flBeamHDRColorScale = 15f;
            //    item.m_fHaloScale = 7f;
            //    //item.m_nBeamFlags = Beam_Flags_h.NUM_BEAM_FLAGS;
            //    //item.m_nBeamType = BeamType.NUM_BEAM_TYPES;
            //    //item.m_fAmplitude = 200f;
            //    item.m_fWidth = 90f;
            //    item.m_fEndWidth = 666f;
            //    item.m_fFadeLength = 500f;
            //}
            UpdateSprites();
            UpdateBeams();
            m_dtLastUpdate = DateTime.Now;
            base.AfterUpdate();
        }

        public override void Before()
        {
            base.Before();
        }

        public override void End()
        {
            base.End();
        }

        public override void Start()
        {
            base.Start();
        }

        public override bool Update()
        {
            return base.Update();
        }
    }
}
