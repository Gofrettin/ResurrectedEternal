using Newtonsoft.Json;
using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.ClientObjects.Cvars;
using ResurrectedEternal.GenericObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.EnvironmentSkillMods
{
    class SkillModEnvironmentControl : SkillMod
    {
        private DateTime m_dtLastUpdate = DateTime.Now;
        private TimeSpan m_tsInterval = TimeSpan.FromMilliseconds(250);
        public SkillModEnvironmentControl(Engine engine, Client client) : base(engine, client)
        {
        }
        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammode
                || ClientModus == Events.Modus.streammodefull)
                return false;
            return true;
        }
        public override void AfterUpdate()
        {
            if (!CanProcess())
                return;

            if (!Client.UpdateModules || Client.DontGlow || Client.LocalPlayer == null || !Engine.IsInGame || !Client.UpdateModules)
                return;

            if (DateTime.Now - m_dtLastUpdate < m_tsInterval)
                return;
            SunControl();
            FogControl();
            WindControl();
            LightControl();
            TonemapControl();
            PostProcessControl();
            SkyControl();
            m_dtLastUpdate = DateTime.Now;

            base.AfterUpdate();
        }



        private void MakeHappyThings()
        {

        }

        private ConvarEntity DrawSkyConvar;
        private ConvarEntity SkyNameConvar;

        private Skyboxes _skybox = Skyboxes.cs_baggage_skybox_;
        private string Skyname = "";

        private void SkyControl()
        {
            if (!(bool)Config.SunConfig.SkyControl.Value)
                return;
            if (DrawSkyConvar == null)
                DrawSkyConvar = ConvarManager.instance.GetConvar("r_3dsky");
            if (SkyNameConvar == null)
                SkyNameConvar = ConvarManager.instance.GetConvar("sv_skyname");

            if (DrawSkyConvar.m_nValue == 1)
                DrawSkyConvar.m_nValue = 0;

            if (_skybox != (Skyboxes)Config.SunConfig.SkyControlSkyName.Value)
            {
                Skyname = ((Skyboxes)Config.SunConfig.SkyControlSkyName.Value).ToString();
                _skybox = (Skyboxes)Config.SunConfig.SkyControlSkyName.Value;
            }

            if (SkyNameConvar.m_pszValue != Skyname)
                SkyNameConvar.m_pszValue = Skyname;


        }
        private Sun m_dwSun;

        private bool _defaultSun = false;

        private void SaveJson(string name, object o)
        {
            System.IO.File.WriteAllText(name + ".json", JsonConvert.SerializeObject(o, Formatting.Indented));
        }

        private void SunControl()
        {
            if (!(bool)Config.SunConfig.Sun.Value)
                return;
            var _result = Client.GetEntityByClass(ClientClass.CSun);
            if (_result == null)
                return;
            m_dwSun = _result as Sun;
            if (!m_dwSun.IsValid)
                return;

            //if (!_defaultSun)
            //{
            //    SaveJson("sun", m_dwSun);
            //    m_vSunPrimaryCurrentDirect = m_dwSun.m_dwSunOverlayPrimary.m_vecDir;
            //    m_vSunSecondaryCurrentDirect = m_dwSun.m_dwSunOverlaySecondary.m_vecDir;
            //    _defaultSun = true;
            //}


            if (!m_dwSun.HasValidPointers)
                return;


            //m_dwSunOverlaySecondary = new SunOverlay(BaseAddress + g_Globals.Offset.m_dwSecondaryGlowOverlay);)

            for (int i = 0; i < (int)Config.SunConfig.SunOverlayCount.Value; i++)
            {
                if (m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i] == null) continue;

                SharpDX.Vector3 _clr_primary = SharpDX.Vector3.Zero;
                SharpDX.Vector3 _clr_secondary = SharpDX.Vector3.Zero;
                float primary_horizontal = 0f;
                float primary_vertical = 0f;

                float secondary_vertical = 0f;
                float secondary_horizontal = 0f;

                switch (i)
                {
                    case 0:
                        _clr_primary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayOne_primary_color.Value);
                        _clr_secondary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayOne_secondary_color.Value);
                        primary_horizontal = (float)Config.SunConfig.OverlayOne_primary_Horizontal.Value;
                        secondary_horizontal = (float)Config.SunConfig.OverlayOne_secondary_Horizontal.Value;
                        primary_vertical = (float)Config.SunConfig.OverlayOne_primary_Vertical.Value;
                        secondary_vertical = (float)Config.SunConfig.OverlayOne_secondary_Vertical.Value;
                        break;
                    case 1:
                        _clr_primary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayTwo_primary_color.Value);
                        _clr_secondary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayTwo_secondary_color.Value);
                        primary_horizontal = (float)Config.SunConfig.OverlayTwo_primary_Horizontal.Value;
                        secondary_horizontal = (float)Config.SunConfig.OverlayTwo_secondary_Horizontal.Value;
                        primary_vertical = (float)Config.SunConfig.OverlayTwo_primary_Vertical.Value;
                        secondary_vertical = (float)Config.SunConfig.OverlayTwo_secondary_Vertical.Value;
                        break;
                    case 2:
                        _clr_primary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayThree_primary_color.Value);
                        _clr_secondary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayThree_secondary_color.Value);
                        primary_horizontal = (float)Config.SunConfig.OverlayThree_primary_Horizontal.Value;
                        secondary_horizontal = (float)Config.SunConfig.OverlayThree_secondary_Horizontal.Value;
                        primary_vertical = (float)Config.SunConfig.OverlayThree_primary_Vertical.Value;
                        secondary_vertical = (float)Config.SunConfig.OverlayThree_secondary_Vertical.Value;
                        break;
                    case 3:
                        _clr_primary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayFour_primary_color.Value);
                        _clr_secondary = m_flColorFromRGB((SharpDX.Color)Config.SunConfig.OverlayFour_secondary_color.Value);
                        primary_horizontal = (float)Config.SunConfig.OverlayFour_primary_Horizontal.Value;
                        secondary_horizontal = (float)Config.SunConfig.OverlayFour_secondary_Horizontal.Value;
                        primary_vertical = (float)Config.SunConfig.OverlayFour_primary_Vertical.Value;
                        secondary_vertical = (float)Config.SunConfig.OverlayFour_secondary_Vertical.Value;
                        break;
                    default:
                        break;
                }


                if (m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_colorVec != _clr_primary)
                    m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_colorVec = _clr_primary;
                if (m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_flHorizontalScale != primary_horizontal)
                    m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_flHorizontalScale = primary_horizontal;
                if (m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_flVerticalScale != primary_vertical)
                    m_dwSun.m_dwSunOverlayPrimary.m_glowOverlay[i].m_flVerticalScale = primary_vertical;


                if (m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_colorVec != _clr_secondary)
                    m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_colorVec = _clr_secondary;
                if (m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_flHorizontalScale != secondary_horizontal)
                    m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_flHorizontalScale = secondary_horizontal;
                if (m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_flVerticalScale != secondary_vertical)
                    m_dwSun.m_dwSunOverlaySecondary.m_glowOverlay[i].m_flVerticalScale = secondary_vertical;

            }

            var _count = Convert.ToByte(Config.SunConfig.SunOverlayCount.Value);
            if (m_dwSun.m_dwSunOverlayPrimary.m_vecDir != m_vSunPrimaryCurrentDirect)
                m_dwSun.m_dwSunOverlayPrimary.m_vecDir = m_vSunPrimaryCurrentDirect;
            if (m_dwSun.m_dwSunOverlaySecondary.m_vecDir != m_vSunSecondaryCurrentDirect)
                m_dwSun.m_dwSunOverlaySecondary.m_vecDir = m_vSunSecondaryCurrentDirect;
            if (m_dwSun.m_dwSunOverlayPrimary.m_nSprites != _count)
                m_dwSun.m_dwSunOverlayPrimary.m_nSprites = _count;
            if (m_dwSun.m_dwSunOverlaySecondary.m_nSprites != _count)
                m_dwSun.m_dwSunOverlaySecondary.m_nSprites = _count;
        }

        private FogController m_dwFog;
        private bool m_dwfogdefa = false;
        private void FogControl()
        {
            if (!(bool)Config.EnvironmentConfig.FogOverride.Value)
                return;

            var _result = Client.GetEntityByClass(ClientClass.CFogController);
            if (_result == null)
                return;
            m_dwFog = _result as FogController;

            if (!m_dwFog.IsValid)
                return;


            //if (!m_dwfogdefa)
            //{
            //    SaveJson("fog", m_dwFog);
            //    Config.TIDAL.Fog.Value = m_dwFog.m_bFogEnable;
            //    Config.TIDAL.Blend.Value = m_dwFog.m_bFogblend;
            //    m_vFogDirPrimary = m_dwFog.m_vFogdirPrimary;
            //    Config.TIDAL.PrimaryFogColor.Value = m_dwFog.m_cFogcolorPrimary;
            //    Config.TIDAL.SecondaryFogColor.Value = m_dwFog.m_cFogcolorSecondary;
            //    Config.TIDAL.FogStart.Value = m_dwFog.m_fFogstart;
            //    Config.TIDAL.FogEnd.Value = m_dwFog.m_fFogend;
            //    Config.TIDAL.FogDensity.Value = m_dwFog.m_fFogmaxdensity;
            //    Config.TIDAL.HDRColor.Value = m_dwFog.m_fFogHDRColorScale;
            //    Config.TIDAL.ZoomFogScale.Value = m_dwFog.m_fFogZoomFogScale;
            //    m_dwfogdefa = true;
            //}
            if (m_dwFog.m_bFogEnable != (bool)Config.EnvironmentConfig.FogEnable.Value)
                m_dwFog.m_bFogEnable = (bool)Config.EnvironmentConfig.FogEnable.Value;
            if (m_dwFog.m_bFogblend != (bool)Config.EnvironmentConfig.Blend.Value)
                m_dwFog.m_bFogblend = (bool)Config.EnvironmentConfig.Blend.Value;
            if (m_dwFog.m_vFogdirPrimary != m_vFogDirPrimary)
                m_dwFog.m_vFogdirPrimary = m_vFogDirPrimary;
            if (m_dwFog.m_cFogcolorPrimary != (SharpDX.Color)Config.EnvironmentConfig.PrimaryFogColor.Value)
                m_dwFog.m_cFogcolorPrimary = (SharpDX.Color)Config.EnvironmentConfig.PrimaryFogColor.Value;
            if (m_dwFog.m_cFogcolorSecondary != (SharpDX.Color)Config.EnvironmentConfig.SecondaryFogColor.Value)
                m_dwFog.m_cFogcolorSecondary = (SharpDX.Color)Config.EnvironmentConfig.SecondaryFogColor.Value;
            if (m_dwFog.m_fFogstart != (float)Config.EnvironmentConfig.FogStart.Value)
                m_dwFog.m_fFogstart = (float)Config.EnvironmentConfig.FogStart.Value;
            if (m_dwFog.m_fFogend != (float)Config.EnvironmentConfig.FogEnd.Value)
                m_dwFog.m_fFogend = (float)Config.EnvironmentConfig.FogEnd.Value;
            //if (m_dwFog.m_fFogfarz != (float)Config.TIDAL.FogFarZ.Value)
            //    m_dwFog.m_fFogfarz = (float)Config.TIDAL.FogFarZ.Value;
            if (m_dwFog.m_fFogmaxdensity != (float)Config.EnvironmentConfig.FogDensity.Value)
                m_dwFog.m_fFogmaxdensity = (float)Config.EnvironmentConfig.FogDensity.Value;
            if (m_dwFog.m_fFogHDRColorScale != (float)Config.EnvironmentConfig.HDRColor.Value)
                m_dwFog.m_fFogHDRColorScale = (float)Config.EnvironmentConfig.HDRColor.Value;
            if (m_dwFog.m_fFogZoomFogScale != (float)Config.EnvironmentConfig.ZoomFogScale.Value)
                m_dwFog.m_fFogZoomFogScale = (float)Config.EnvironmentConfig.ZoomFogScale.Value;
        }

        private CascadeLight m_dwLight;
        private void LightControl()
        {
            if (!(bool)Config.EnvironmentConfig.OverrideCascade.Value)
                return;
            var _result = Client.GetEntityByClass(ClientClass.CCascadeLight);
            if (_result == null)
                return;
            m_dwLight = _result as CascadeLight;

            if (!m_dwLight.IsValid)
                return;

            //Might want to serialize if map doesnt exist
            //if (!_HasDefaultValues)
            //{
            //    SaveJson("cascade", m_dwLight);
            //    Config.TIDAL.Enable.Value = m_dwLight.m_bEnabled;
            //    Config.TIDAL.envEnable.Value = m_dwLight.m_bUseLightEnvAngles;
            //    Config.TIDAL.ColorScale.Value = m_dwLight.m_LightColorScale;
            //    Config.TIDAL.ShadowDist.Value = m_dwLight.m_flMaxShadowDist;
            //    var _clr = m_dwLight.m_LightColor;
            //    g_Globals.ColorManager.AddColor(_clr, "Cascade Color");
            //    Config.TIDAL.Color.Value = _clr;
            //    var _envDir = Generators.ToFloat(m_dwLight.m_envLightShadowDirection);
            //    var _Dir = Generators.ToFloat(m_dwLight.m_shadowDirection);
            //    Config.TIDAL.ShadowDirX.Value = _Dir[0];
            //    Config.TIDAL.ShadowDirY.Value = _Dir[1];
            //    Config.TIDAL.ShadowDirZ.Value = _Dir[2];
            //    Config.TIDAL.envShadowDirX.Value = _envDir[0];
            //    Config.TIDAL.envShadowDirY.Value = _envDir[1];
            //    Config.TIDAL.envShadowDirZ.Value = _envDir[2];
            //    _HasDefaultValues = true;

            //}
            if (m_dwLight.m_bEnabled != (bool)Config.EnvironmentConfig.Enable.Value)
                m_dwLight.m_bEnabled = (bool)Config.EnvironmentConfig.Enable.Value;
            if (m_dwLight.m_bUseLightEnvAngles != (bool)Config.EnvironmentConfig.envEnable.Value)
                m_dwLight.m_bUseLightEnvAngles = (bool)Config.EnvironmentConfig.envEnable.Value;
            if (m_dwLight.m_LightColorScale != (int)Config.EnvironmentConfig.ColorScale.Value)
                m_dwLight.m_LightColorScale = (int)Config.EnvironmentConfig.ColorScale.Value;
            if (m_dwLight.m_flMaxShadowDist != (float)Config.EnvironmentConfig.ShadowDist.Value)
                m_dwLight.m_flMaxShadowDist = (float)Config.EnvironmentConfig.ShadowDist.Value;
            if (m_dwLight.m_shadowDirection != m_shadowDirVector)
                m_dwLight.m_shadowDirection = m_shadowDirVector;
            if (m_dwLight.m_envLightShadowDirection != m_envShadowDirEnvVector)
                m_dwLight.m_envLightShadowDirection = m_envShadowDirEnvVector;
            if (m_dwLight.m_LightColor != (SharpDX.Color)Config.EnvironmentConfig.Color.Value)
                m_dwLight.m_LightColor = (SharpDX.Color)Config.EnvironmentConfig.Color.Value;
        }

        private Wind m_dwWind;
        private bool _m_dwWindDefault = false;
        private void WindControl()
        {
            if (!(bool)Config.EnvironmentConfig.WindOverride.Value)
                return;
            var _result = Client.GetEntityByClass(ClientClass.CEnvWind);
            if (_result == null)
                return;
            m_dwWind = _result as Wind;
            if (!m_dwWind.IsValid)
                return;

            //if (!_m_dwWindDefault)
            //{
            //    SaveJson("wind", m_dwWind);
            //    Config.TIDAL.MinWind.Value = m_dwWind.m_iMinWind;
            //    Config.TIDAL.MaxWind.Value = m_dwWind.m_iMaxWind;
            //    Config.TIDAL.MaxGust.Value = m_dwWind.m_iMaxGust;
            //    Config.TIDAL.MinGust.Value = m_dwWind.m_iMinGust;
            //    Config.TIDAL.InitialWindSpeed.Value = m_dwWind.m_flInitialWindSpeed;
            //    Config.TIDAL.MinGustDelay.Value = m_dwWind.m_flMinGustDelay;
            //    Config.TIDAL.MaxGustDelay.Value = m_dwWind.m_flMaxGustDelay;
            //    Config.TIDAL.GustDuration.Value = m_dwWind.m_flGustDuration;
            //    _m_dwWindDefault = true;
            //}

            if (m_dwWind.m_iMinWind != (int)Config.EnvironmentConfig.MinWind.Value)
                m_dwWind.m_iMinWind = (int)Config.EnvironmentConfig.MinWind.Value;
            if (m_dwWind.m_iMaxWind != (int)Config.EnvironmentConfig.MaxWind.Value)
                m_dwWind.m_iMaxWind = (int)Config.EnvironmentConfig.MaxWind.Value;
            if (m_dwWind.m_iMaxGust != (int)Config.EnvironmentConfig.MaxGust.Value)
                m_dwWind.m_iMaxGust = (int)Config.EnvironmentConfig.MaxGust.Value;
            if (m_dwWind.m_iMinGust != (int)Config.EnvironmentConfig.MinGust.Value)
                m_dwWind.m_iMinGust = (int)Config.EnvironmentConfig.MinGust.Value;
            if (m_dwWind.m_flInitialWindSpeed != (float)Config.EnvironmentConfig.InitialWindSpeed.Value)
                m_dwWind.m_flInitialWindSpeed = (float)Config.EnvironmentConfig.InitialWindSpeed.Value;
            if (m_dwWind.m_flMinGustDelay != (float)Config.EnvironmentConfig.MinGustDelay.Value)
                m_dwWind.m_flMinGustDelay = (float)Config.EnvironmentConfig.MinGustDelay.Value;
            if (m_dwWind.m_flMaxGustDelay != (float)Config.EnvironmentConfig.MaxGustDelay.Value)
                m_dwWind.m_flMaxGustDelay = (float)Config.EnvironmentConfig.MaxGustDelay.Value;
            if (m_dwWind.m_flGustDuration != (float)Config.EnvironmentConfig.GustDuration.Value)
                m_dwWind.m_flGustDuration = (float)Config.EnvironmentConfig.GustDuration.Value;
        }

        private TonemapController m_dwTonemapper;
        private void TonemapControl()
        {
            var _result = Client.GetEntityByClass(ClientClass.CEnvTonemapController);
            if (_result == null)
                return;
            m_dwTonemapper = _result as TonemapController;

            if (!m_dwTonemapper.IsValid)
                return;


            //if (!m_dwTonemapper.m_bUseCustomAutoExposureMin)
            //    m_dwTonemapper.m_bUseCustomAutoExposureMin = true;
            //if (!m_dwTonemapper.m_bUseCustomAutoExposureMax)
            //    m_dwTonemapper.m_bUseCustomAutoExposureMax = true;
            if (m_dwTonemapper.m_flCustomAutoExposureMin != (float)Config.NeonConfig.CustomAutoExposureMin.Value)
                m_dwTonemapper.m_flCustomAutoExposureMin = (float)Config.NeonConfig.CustomAutoExposureMin.Value;
            if (m_dwTonemapper.m_flCustomAutoExposureMax != (float)Config.NeonConfig.CustomAutoExposureMax.Value)
                m_dwTonemapper.m_flCustomAutoExposureMax = (float)Config.NeonConfig.CustomAutoExposureMax.Value;
            if (m_dwTonemapper.m_flCustomBloomScale != (float)Config.NeonConfig.CustomBloomScale.Value)
                m_dwTonemapper.m_flCustomBloomScale = (float)Config.NeonConfig.CustomBloomScale.Value;
            if (m_dwTonemapper.m_flCustomBloomScaleMinimum != (float)Config.NeonConfig.CustomBloomScaleMin.Value)
                m_dwTonemapper.m_flCustomBloomScaleMinimum = (float)Config.NeonConfig.CustomBloomScaleMin.Value;
            //if (m_dwTonemapper.m_flBloomExponent != (float)Config.MovieConfig.BloomExponent.Value)
            //    m_dwTonemapper.m_flBloomExponent = (float)Config.MovieConfig.BloomExponent.Value;
            if (m_dwTonemapper.m_flBloomSaturation != (float)Config.NeonConfig.BloomSaturation.Value)
                m_dwTonemapper.m_flBloomSaturation = (float)Config.NeonConfig.BloomSaturation.Value;
            if (m_dwTonemapper.m_flTonemapPercentTarget != (float)Config.NeonConfig.TonemapPercentTarget.Value)
                m_dwTonemapper.m_flTonemapPercentTarget = (float)Config.NeonConfig.TonemapPercentTarget.Value;
            if (m_dwTonemapper.m_flTonemapPercentBrightPixels != (float)Config.NeonConfig.TonemapPercentBrightPixels.Value)
                m_dwTonemapper.m_flTonemapPercentBrightPixels = (float)Config.NeonConfig.TonemapPercentBrightPixels.Value;
            if (m_dwTonemapper.m_flTonemapMinAvgLum != (float)Config.NeonConfig.TonemapMinAvgLum.Value)
                m_dwTonemapper.m_flTonemapMinAvgLum = (float)Config.NeonConfig.TonemapMinAvgLum.Value;
            if (m_dwTonemapper.m_flTonemapRate != (float)Config.NeonConfig.TonemapRate.Value)
                m_dwTonemapper.m_flTonemapRate = (float)Config.NeonConfig.TonemapRate.Value;
        }

        private PostProcessController m_dwppController;
        private void PostProcessControl()
        {
            var _result = Client.GetEntityByClass(ClientClass.CPostProcessController);
            if (_result == null)
                return;
            m_dwppController = _result as PostProcessController;

            if (!m_dwppController.IsValid)

                return;


            if (m_dwppController.m_flVignetteBlurStrength != (float)Config.NeonConfig.VignetteBlurStrength.Value)
                m_dwppController.m_flVignetteBlurStrength = (float)Config.NeonConfig.VignetteBlurStrength.Value;
            if (m_dwppController.m_flFadetoblackStrength != (float)Config.NeonConfig.FadeToBlackStrength.Value)
                m_dwppController.m_flFadetoblackStrength = (float)Config.NeonConfig.FadeToBlackStrength.Value;
        }

        public override bool Update()
        {
            return base.Update();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Before()
        {
            base.Before();
        }


        public override void End()
        {
            base.End();
        }

        private SharpDX.Vector3 m_flColorFromRGB(SharpDX.Color _c)
        {
            var _red = EngineMath.Clamp(_c.R / 255f, 0f, 1f);
            var _gree = EngineMath.Clamp(_c.G / 255f, 0f, 1f);
            var _blue = EngineMath.Clamp(_c.B / 255f, 0f, 1f);
            return new SharpDX.Vector3(_red, _gree, _blue);
        }

        private SharpDX.Vector3 m_vSunPrimaryCurrentDirect
        {
            get
            {
                var _v = new SharpDX.Vector3(
                    (float)Config.SunConfig.SunDirX.Value,
                     (float)Config.SunConfig.SunDirY.Value,
                    (float)Config.SunConfig.SunDirZ.Value
              );
                //_v.Normalize();
                return _v;
            }
            set
            {
                Config.SunConfig.SunDirX.Value = value.X;
                Config.SunConfig.SunDirY.Value = value.Y;
                Config.SunConfig.SunDirZ.Value = value.Z;
            }
        }
        private SharpDX.Vector3 m_vSunSecondaryCurrentDirect
        {
            get
            {
                var _v = new SharpDX.Vector3(
                    (float)Config.SunConfig.SecondarySunDirX.Value,
                     (float)Config.SunConfig.SecondarySunDirY.Value,
                    (float)Config.SunConfig.SecondarySunDirZ.Value
              );
                //_v.Normalize();
                return _v;
            }
            set
            {
                Config.SunConfig.SecondarySunDirX.Value = value.X;
                Config.SunConfig.SecondarySunDirY.Value = value.Y;
                Config.SunConfig.SecondarySunDirZ.Value = value.Z;
            }
        }
        private SharpDX.Vector3 m_vFogDirPrimary
        {
            get
            {
                return new SharpDX.Vector3(
                    (float)Config.EnvironmentConfig.FogDirX.Value,
                    (float)Config.EnvironmentConfig.FogDirY.Value,
                    (float)Config.EnvironmentConfig.FogDirZ.Value
              );
            }
            set

            {
                Config.EnvironmentConfig.FogDirX.Value = value.X;
                Config.EnvironmentConfig.FogDirY.Value = value.Y;
                Config.EnvironmentConfig.FogDirZ.Value = value.Z;
            }
        }

        private SharpDX.Vector3 m_shadowDirVector
        {
            get
            {
                return new SharpDX.Vector3(
              (float)Config.EnvironmentConfig.ShadowDirX.Value,
              (float)Config.EnvironmentConfig.ShadowDirY.Value,
              (float)Config.EnvironmentConfig.ShadowDirZ.Value);
            }
            set
            {
                Config.EnvironmentConfig.ShadowDirX.Value = value.X;
                Config.EnvironmentConfig.ShadowDirY.Value = value.Y;
                Config.EnvironmentConfig.ShadowDirZ.Value = value.Z;
            }
        }
        private SharpDX.Vector3 m_envShadowDirEnvVector
        {
            get
            {
                return new SharpDX.Vector3(
              (float)Config.EnvironmentConfig.envShadowDirX.Value,
              (float)Config.EnvironmentConfig.envShadowDirY.Value,
              (float)Config.EnvironmentConfig.envShadowDirZ.Value);
            }
            set
            {
                Config.EnvironmentConfig.envShadowDirX.Value = value.X;
                Config.EnvironmentConfig.envShadowDirY.Value = value.Y;
                Config.EnvironmentConfig.envShadowDirZ.Value = value.Z;
            }
        }
    }
}
