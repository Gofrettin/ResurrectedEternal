using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events.EventArgs
{
    public class PushClipEventArgs
    {
        public string Message;
        public float Duration;
        public SharpDX.Color Color;

        public PushClipEventArgs(string msg, float dur, SharpDX.Color col)
        {
            Message = msg;
            Duration = dur;
            Color = col;
            EventManager.Notify(this);
        }


    }
}
