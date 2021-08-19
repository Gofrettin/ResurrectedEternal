using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects.Cvars
{
    public class ConvarEntity
    {
        public bool IsValid => m_pThis != IntPtr.Zero;

        public void SetValue(string val)
        {
            if (float.TryParse(val, out float _val))
            {
                m_flValue = _val;
                m_nValue = Convert.ToInt32(_val);
                m_pszValue = val;
            }
            else
            {
                m_pszValue = val;
            }
        }

        public void SetValue(float val)
        {
            m_flValue = val;
        }

        public void SetValue(int val)
        {
            m_nValue = val;
        }

        public float m_flValue
        {
            get
            {
                return Generators.XORFLOAT(m_pThis, MemoryLoader.instance.Reader.ReadBytes(m_pThis + 0x2C, 4));
            }
            set
            {
                MemoryLoader.instance.Reader.Write(m_pThis + 0x2C, Generators.XOR((int)m_pThis, value));
            }

        }

        public int m_nValue
        {
            get
            {
                return Generators.XORINT(m_pThis, MemoryLoader.instance.Reader.Read<int>(m_pThis + 0x30));
            }
            set
            {
                MemoryLoader.instance.Reader.Write(m_pThis + 0x30, Generators.XOR((int)m_pThis, value));
            }

        }

        public string m_pszValue
        {
            get
            {
                return MemoryLoader.instance.Reader.ReadString(MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x24), Encoding.UTF8).Trim('\0');
            }
            set
            {
                MemoryLoader.instance.Reader.WriteString(MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x24), value, Encoding.UTF8);
            }
        }


        private string m_pdwpszDefaultValue;
        public string m_pszDefaultValue
        {
            get
            {
                if (string.IsNullOrEmpty(m_pdwpszDefaultValue))
                    m_pdwpszDefaultValue = MemoryLoader.instance.Reader.ReadString(MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x20), Encoding.Default, 128).Trim('\0');
                return m_pdwpszDefaultValue;
            }
        }

        private string m_ppszDescription;
        public string m_pszDescription
        {
            get
            {
                if (string.IsNullOrEmpty(m_ppszDescription))
                    m_ppszDescription = MemoryLoader.instance.Reader.ReadString(MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x10), Encoding.Default).Trim('\0');
                return m_ppszDescription;
            }
        }
        //dafuq?
        public byte m_nSize
        {
            get { return MemoryLoader.instance.Reader.Read<byte>(m_pThis + 0x28); }
        }

        public bool m_bRegistered
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(m_pThis + 0x08); }
        }

        public bool m_bHasMin
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(m_pThis + 0x34); }
        }

        public bool m_bHasMax
        {
            get { return MemoryLoader.instance.Reader.Read<bool>(m_pThis + 0x3C); }
        }


        //This can be either int or float, depends.
        public float m_fMinVal
        {
            get { return MemoryLoader.instance.Reader.Read<float>(m_pThis + 0x38); }
        }

        public float m_fMaxVal
        {
            get { return MemoryLoader.instance.Reader.Read<float>(m_pThis + 0x40); }
        }

        private int m_pnFlags;
        public int m_nFlags
        {
            get
            {
                if (Flags.Count == 0)
                {
                    m_pnFlags = MemoryLoader.instance.Reader.Read<int>(m_pThis + 0x14);
                    GetFlags();
                }
                return m_pnFlags;
            }
        }

        public CONVAR_FLAGS[] m_aFlags
        {
            get { return Flags.ToArray(); }
        }

        public IntPtr m_pNext
        {
            get
            {
                //var _n = MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x1C);
                //if (_n == IntPtr.Zero)
                //    return IntPtr.Zero;

                //return MemoryLoader.instance.Reader.Read<IntPtr>(_n);
                return MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 4);
            }
        }
        private IntPtr m_ppThis;
        public IntPtr m_pThis
        {
            get { return m_ppThis; }
            set { m_ppThis = value; }
        }

        public IntPtr m_pParent
        {
            get
            {
                var _n = MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x1C);
                if (_n == IntPtr.Zero)
                    return IntPtr.Zero;
                return MemoryLoader.instance.Reader.Read<IntPtr>(_n);
            }
        }

        //set value always sets the float first, then the engine converts it to a int and last to a string value.
        //however we need to determine if we actually have to set the string value because of the overhead.
        //also verifiying the value could increase performance overhead a little bit since were comparing against 2/3 values.

        //public IntPtr m_pThis;
        //public bool m_bRegistered;

        private string m_Name;
        public string m_pszName
        {
            get
            {
                if (string.IsNullOrEmpty(m_Name))
                    m_Name = MemoryLoader.instance.Reader.ReadString(MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 0x0C), Encoding.Default, 266).Trim('\0');
                return m_Name;
            }
        }
        //public string m_pszDescription;
        //public int m_nFlags;
        //original - called by Revert/Restore convar
        public IntPtr m_Parent;
        //public string m_pszDefaultValue;
        //public string m_pszValue;
        //public int m_nSize;
        //public float m_flValue;
        //public int m_nValue;
        //public bool m_bHasMin;
        //public bool m_bHasMax;
        //public float m_fMinVal;
        //public float m_fMaxVal;

        public Type TypeOf = typeof(int);

        /// <summary>
        /// for anyone wondering why this is proof on external, we're writing to memory and not touching the convar.
        /// internals have a mysterious way of creating/editing convars which could be proof again if they implement memory editing
        /// and instead of referencing the convar just write to memory directly.
        /// VAC could monitor this but they dont
        /// </summary>
        /// <param name="p"></param>
        public ConvarEntity(IntPtr p)
        {
            m_pThis = p;
        }

        private bool IsInt()
        {
            return int.TryParse(m_pszDefaultValue, out _);
        }

        private bool IsFloat()
        {
            return float.TryParse(m_pszDefaultValue.Contains(",") ? m_pszDefaultValue.Replace('.', ',') : m_pszDefaultValue, out _);
        }

        public IntPtr GetNext()
        {
            return MemoryLoader.instance.Reader.Read<IntPtr>(m_pThis + 4);
        }

        public List<CONVAR_FLAGS> Flags = new List<CONVAR_FLAGS>();
        private void GetFlags()
        {
            foreach (var item in Enum.GetValues(typeof(CONVAR_FLAGS)))
            {
                var _flag = (CONVAR_FLAGS)item;
                if (((CONVAR_FLAGS)m_pnFlags).HasFlag(_flag))
                    Flags.Add(_flag);

            }
        }

        public bool HasFlag(
            CONVAR_FLAGS flag)
        {
            if (Flags.Count == 0)
                GetFlags();
            return Flags.Contains(flag);
        }
        public void ClearCallbacks()
        {
            MemoryLoader.instance.Reader.Write(m_pThis + 0x44 + 0xC, 0);
        }

        internal int GetStringPtr()
        {
            return MemoryLoader.instance.Reader.Read<int>(m_pThis + 0x24);
        }


    }
}
