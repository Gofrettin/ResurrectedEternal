using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class ProjectileEntity : BaseEntity
    {
        public bool IsVisible = false;
        public string Name
        {
            get { return MemoryLoader.instance.Reader.ReadString(BaseAddress + g_Globals.Offset.m_iName, Encoding.UTF8, 260); }
        }

        public ProjectileEntity(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }

        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    base.Update(_localPlayer);
        //}
    }
}
