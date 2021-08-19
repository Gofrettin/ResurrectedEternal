using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class EnvironmentAmbientLight : SpatialEntity
    {

        public SharpDX.Vector3 m_vecColor
        {
            get { return MemoryLoader.instance.Reader.Read<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_vecColor); }
            set { MemoryLoader.instance.Reader.Write<SharpDX.Vector3>(BaseAddress + g_Globals.Offset.m_vecColor, value); }
        }
        public EnvironmentAmbientLight(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
