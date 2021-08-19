using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResurrectedEternal.Clockwork
{
    public static class Clock
    {
        private static Thread _clockwork;
        public static long _ticks { private set; get; }
        static DateTime time1 = DateTime.Now;
        static DateTime time2 = DateTime.Now;

        private static void OnClockTick()
        {
            while (true)
            {
                time2 = DateTime.Now;
                DeltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
                _ticks++;
                Thread.Sleep(1);
                time1 = time2;
            }
        }
        public static float DeltaTime { get; private set; }

        public static void Initialize()
        {
            _clockwork = new Thread(OnClockTick);
            _clockwork.Start();
        }
        private static readonly System.Diagnostics.Stopwatch _sw = System.Diagnostics.Stopwatch.StartNew();

        

        public static TimeSpan GetTime()
        {
            return _sw.Elapsed;
        }

    }
}
