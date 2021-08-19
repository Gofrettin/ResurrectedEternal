using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Clockwork
{
    class ObjectClock
    {
        private long _deltaTickAverage = 0;
        private long _lastTick = 0;
        private DateTime _timeSinceStart = DateTime.Now;

        public ObjectClock()
        {
            _lastTick = Clock._ticks;
        }

        public long GetDeltaTick()
        {
            _deltaTickAverage = Clock._ticks - _lastTick;
            _lastTick = Clock._ticks;
            return _deltaTickAverage;
        }
    }
}
