using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class Sprite : BaseEntity
    {
        //public new Vector3 m_vecOrigin
        //{
        //    //set { m_pvecOrigin = value; }
        //    get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress + (int)g_Globals.Offset.m_vecOrigin); }
        //    set { MemoryLoader.instance.Reader.Write<Vector3>(BaseAddress + g_Globals.Offset.m_vecOrigin, value); }
        //}
        /// <summary>
        /// Does this sprite has an parent?
        /// </summary>
        public int m_hAttachedToEntity
        {
            get { return MemoryLoader.instance.Reader.Read<int>(BaseAddress + g_Globals.Offset.m_hAttachedToEntity) & 0xFFF; }
        }

        /// <summary>
        /// Are we attached to something? not sure why this is an byte
        /// </summary>
        public int m_nAttachment
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + g_Globals.Offset.m_nAttachment); }
        }
        /// <summary>
        /// How quickly does the scale scale with curtime
        /// </summary>
        public float m_flScaleTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flScaleTime); }
        }
        /// <summary>
        /// how big is the sprite?
        /// </summary>
        public float m_flSpriteScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flSpriteScale); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flSpriteScale, value); }
        }
        public float m_flSpriteFramerate
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flSpriteFramerate); }
        }
        public float m_flGlowProxySize
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + (int)g_Globals.Offset.m_flGlowProxySize); }
            set
            {
                MemoryLoader.instance.Reader.Write<float>(BaseAddress + (int)g_Globals.Offset.m_flGlowProxySize, value);
            }
        }
        public float m_flHDRColorScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flHDRColorScale); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flHDRColorScale, value); }
        }
        public float m_flFrame
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flFrame); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flFrame, value); }
        }
        public float m_flBrightnessTime
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flBrightnessTime); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flBrightnessTime, value); }
        }
        /// <summary>
        /// Probably 0 to 255
        /// </summary>
        public byte m_nBrightness
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + (int)g_Globals.Offset.m_nBrightness); }
            set
            {
                MemoryLoader.instance.Reader.Write<byte>(BaseAddress + (int)g_Globals.Offset.m_nBrightness, value);
            }
        }
        public bool m_bWorldSpaceScale
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(BaseAddress + (int)g_Globals.Offset.m_bWorldSpaceScale); }
            set
            {
                MemoryLoader.instance.Reader.Write<bool>(BaseAddress + (int)g_Globals.Offset.m_bWorldSpaceScale, value);
            }
        }
        public Sprite(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }

        //public override void Update(LocalPlayer _localPlayer)
        //{
        //    base.Update(_localPlayer);
        //}
    }
}
