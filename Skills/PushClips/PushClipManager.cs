using ResurrectedEternal.Events;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Objects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.PushClips
{
    public class PushClip
    {
        public string Message;
        public float Duration;
        private Color _c;
        public Color Color
        {
            get
            {
                
                return _c;
                var percentage = (float)(DateTime.Now.Ticks / ExpireTime.Ticks);
                return new Color(_c.R, _c.G, _c.B, EngineMath.Clamp(_c.A * (byte)percentage, 0, 255));
            }
            private set { _c = value; }
        }
        private DateTime ExpireTime;

        public PushClip(string m, float d, Color c)
        {
            Message = m;
            Duration = d;
            Color = c;
            ExpireTime = DateTime.Now.AddSeconds(Duration);
        }


        public bool Elapsed()
        {
            if (DateTime.Now < ExpireTime)
                return false;
            return true;
        }

    }

    class PushClipManager
    {
        private GenericQueue<PushClip> PushClipQueue = new GenericQueue<PushClip>();

        public PushClipManager()
        {
            EventManager.OnPushClipAdded += EventManager_OnPushClipAdded;
        }

        private void EventManager_OnPushClipAdded(PushClipEventArgs obj)
        {
            PushClipQueue.m_pAdd = new PushClip(obj.Message, obj.Duration, obj.Color);
        }

        private List<PushClip> PushClips = new List<PushClip>();

        public void AddClip(string m, float d, Color c)
        {
            PushClips.Add(new PushClip(m, d, c));
        }

        public void AddClip(PushClip pc)
        {
            PushClips.Add(pc);
        }

        private void Scan()
        {
            if (!PushClipQueue.m_bAvailable)
                return;
            var _next = PushClipQueue.m_pNext;
            while (_next != null)
            {
                PushClips.Add(_next);
                _next = PushClipQueue.m_pNext;
            }

        }

        public PushClip[] GetClips()
        {
            Scan();
            PushClips.RemoveAll(x => x.Elapsed());
            return PushClips.ToArray();
        }

        public string Cut(string message, int length)
        {
            var chunks = ChunksUpto(message, length);
            var str = string.Join("\n", chunks);
            return str.Remove(str.Length - 1, 1);
        }

        private IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }
    }
}
