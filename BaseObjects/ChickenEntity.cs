using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    public class ChickenEntity : BaseEntity
    {
        public Vector3 Head => m_vecOrigin + (Vector3.UnitZ * 8);
        public Vector3 Center => m_vecOrigin + (Vector3.UnitZ * 5);
        public bool Visible = false;

        public bool m_bShouldGlow
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow); }
            set { MemoryLoader.instance.Reader.Write<bool>(BaseAddress + g_Globals.Offset.m_bShouldGlow, value); }
        }

        public ChickenEntity(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }

        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    base.Update(_localPlayer);
        //}
    }
}
