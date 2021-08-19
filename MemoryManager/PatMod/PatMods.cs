using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.MemoryManager.PatMod
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
        public int Extra;
        public int[] Offset;
        public bool Relative = true;
        public bool SubtractOnly = false;
    }
}
