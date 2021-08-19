using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Memory
{
    struct Brainlag
    {
        public IntPtr[] _lag;
        public static Brainlag Generate()
        {
            Random _r = new Random();
            int num = _r.Next(128, 2000);
            List<IntPtr> ptrs = new List<IntPtr>();
            for (int i = 0; i < 1024; i++)
                ptrs.Add(new IntPtr(i));
            return new Brainlag { _lag = ptrs.ToArray() };
        }
    }
}
