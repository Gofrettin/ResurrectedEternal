using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using ResurrectedEternal.Events;
using System;
using System.Collections.Generic;

namespace AudioMagic.Audio
{
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;
        private bool _useCache = true;

        private bool CanProcess()
        {
            if (StateMachine.ClientModus == Modus.leaguemode
                || StateMachine.ClientModus == Modus.streammode
                || StateMachine.ClientModus == Modus.streammodefull)
                return false;
            return true;
        }
        public AudioPlaybackEngine(bool userCache,int sampleRate = 44100, int channelCount = 2)
        {
            _useCache = userCache;
            var _wveOut = new WaveOutEvent();
            //_wveOut.NumberOfBuffers = 8;
            _wveOut.DesiredLatency = 77;
            outputDevice = _wveOut;
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            //outputDevice.Play();
        }

        private Dictionary<string, CachedSound> _readFiles = new Dictionary<string, CachedSound>();

        public void AddCacheFile(string fileName)
        {
            if (!_useCache)
                return;
            if (!_readFiles.ContainsKey(fileName))
                _readFiles.Add(fileName, new CachedSound(fileName));
        }

        public void PlaySound(string fileName)
        {
            if (!CanProcess())
                return;
            if(_useCache)
            {
                if (!_readFiles.ContainsKey(fileName))
                    _readFiles.Add(fileName, new CachedSound(fileName));
                PlaySound(_readFiles[fileName]);
            }
            else
            {
                var input = new AudioFileReader(fileName);
                AddMixerInput(new AutoDisposeFileReader(input));
            }

            return;

        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        private void AddMixerInput(ISampleProvider input)
        {
            if (outputDevice.PlaybackState == PlaybackState.Stopped)
                outputDevice.Play();
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void StopAmbiente()
        {
            outputDevice.Stop();
            mixer.RemoveAllMixerInputs();
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public float m_dwVolume
        {
            get { return outputDevice.Volume; }
            set { outputDevice.Volume = value; }
        }

        //public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}
