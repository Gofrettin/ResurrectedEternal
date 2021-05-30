﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRFull.MemoryManager.PatMod
{
    class ModulePattern
    {
        public string ModuleName;
        public SerialPattern[] Patterns;
    }
    class SerialPattern
    {
        public string Name;
        public string Pattern;
        public int Offset;
        public int Extra;
        public bool Relative = true;
        public bool SubtractOnly = false;
    }
}
