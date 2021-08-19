using ResurrectedEternal.Memory;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects.SunMod
{
    class SunOverlay
    {

        public bool IsValid
        {
            set { _valid = value; }
            get { return _valid; }
        }

        public Vector3 m_vecDir
        {
            get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress + 0x14); }
            set { MemoryLoader.instance.Reader.Write<Vector3>(BaseAddress + 0x14, value); }
        }

        public SunGlowOverlay[] m_glowOverlay;

        private int m_iCurrentSpriteCount = 4;


        //This might be bugged, we have to verify
        public byte m_nSprites
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(BaseAddress + 0x90); }
            set
            {
                //4 sprites active, automatically set.
                //if we reduce to 3 = 4 - 3 = 1, remove 1, remove last.
                //4 - 4 = 0 return;
                //4 - 3 = 1;
                //2 - 4 = -2
                int _difference = m_iCurrentSpriteCount - value;
                bool subtract = true;
                if (_difference == 0)
                    return;
                else if (_difference < 0)
                {
                    subtract = false;
                    _difference = -_difference;
                    //we need two added.
                }

                //whats the current value? m_iCurrentSpritecount
                //how many do we add? _difference
                //m_iCurrentSprite - 1 tile we reach m_iCurrentSpriteCount + _difference - 1;

                //make sure we dont overwrite 0 since it is our only handle to the material, eventually we could store the original material
                var _startIndex = EngineMath.Clamp(m_iCurrentSpriteCount - 1, 1, 3);

                for (int i = _startIndex; i > m_iCurrentSpriteCount + _difference - 1; i--)
                {
                    //do we even have to remove the material
                    if (subtract)
                        m_glowOverlay[i].m_dwMaterial = IntPtr.Zero;
                    else
                        m_glowOverlay[i].m_dwMaterial = m_dwOriginalMaterial;
                }
                //Does this by chance write 0 ? or doe we overwrite 
                MemoryLoader.instance.Reader.Write<byte>(BaseAddress + 0x90, value);
                m_iCurrentSpriteCount = value;
                return;
                ////while (_difference > 0)
                ////{
                ////    _difference--;
                ////    int num = m_iCurrentSpriteCount - _difference;
                ////    if (m_glowOverlay[num - 1].m_dwMaterial == IntPtr.Zero) continue;
                ////    m_glowOverlay[num - 1].m_dwMaterial = IntPtr.Zero;
                ////}
                ////m_iCurrentSpriteCount = value;
                //MemoryLoader.instance.Reader.Write<byte>(BaseAddress + 0x90, value);
            }
        }

        public SunOverlay(IntPtr Address)
        {
            //this gets applied everytime we create a sun.
            BaseAddress = Address;
            m_glowOverlay = new SunGlowOverlay[4];
            m_glowOverlay[0] = new SunGlowOverlay(BaseAddress + 0x30);
            //store original material
            m_dwOriginalMaterial = m_glowOverlay[0].m_dwMaterial;
            if (m_dwOriginalMaterial == IntPtr.Zero)
            {
                _valid = false;
                return;
            }
            //eventually do a nullpointer check for material because it might be that we get a intptr zero here

            for (int i = 1; i < 4; i++)
            {
                //create new overlay with baseaddress.
                var _addr = m_glowOverlay[0].BaseAddress + (0x18 * i);
                m_glowOverlay[i] = new SunGlowOverlay(_addr);
                //somewhere in here the overlay gets overwritten for some reason, we have to recheck the values.
                if (m_glowOverlay[i].m_dwMaterial == m_glowOverlay[0].m_dwMaterial) continue;
                m_glowOverlay[i].m_dwMaterial = m_dwOriginalMaterial;
                //m_nSprites = Convert.ToByte(i);
            }
            m_nSprites = Convert.ToByte(g_Globals.Config.SunConfig.SunOverlayCount.Value);
            m_iCurrentSpriteCount = Convert.ToByte(g_Globals.Config.SunConfig.SunOverlayCount.Value);
        }
        private IntPtr BaseAddress;

        private IntPtr m_dwOriginalMaterial;

        private bool _valid = true;

    }



    public class SunGlowOverlay
    {
        public IntPtr OriginalMaterialPointer = IntPtr.Zero;
        public bool HasMaterial = false;
        public IntPtr m_dwMaterial
        {
            get
            {
                return MemoryLoader.instance.Reader.Read<IntPtr>(BaseAddress + g_Globals.Offset.m_dwMaterial);
            }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_dwMaterial, value); }
        }

        public Vector3 m_colorVec
        {
            get { return MemoryLoader.instance.Reader.Read<Vector3>(BaseAddress); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress, value); }
        }

        public float m_flHorizontalScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flHorizontalScale); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flHorizontalScale, value); }
        }
        public float m_flVerticalScale
        {
            get { return MemoryLoader.instance.Reader.Read<float>(BaseAddress + g_Globals.Offset.m_flVerticalScale); }
            set { MemoryLoader.instance.Reader.Write(BaseAddress + g_Globals.Offset.m_flVerticalScale, value); }
        }
        public SunGlowOverlay(IntPtr baseAddress)
        {
            BaseAddress = baseAddress;
        }
        public IntPtr BaseAddress;

    }
}
