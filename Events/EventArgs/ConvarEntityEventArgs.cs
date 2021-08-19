using Newtonsoft.Json;
using ResurrectedEternal.ClientObjects.Cvars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events.EventArgs
{
    public class ConvarEntityEventArgs
    {
        [JsonIgnore]
        public ConvarEntity ConvarEntity;

        public string m_pszConvarName;
        public string m_pszValue;
        public float m_flValue;
        public int m_nValue;

        public bool isFloat = false;
        public bool isInt = false;

        [JsonConstructor]
        public ConvarEntityEventArgs()
        {

        }

        public ConvarEntityEventArgs(string name, string pszval)
        {

            //ConvarEntity = _convar;
            m_pszConvarName = name;
            m_pszValue = pszval;
            if (pszval.Contains(",") || pszval.Contains("."))
                ToFloat();
            else
                ToInt();


            if (ConvarEntity == null)
                ConvarEntity = ConvarManager.instance.GetConvar(name);
            EventManager.Notify(this);
        }
        public ConvarEntityEventArgs(ConvarEntity _cvar, string newValue)
        {

            ConvarEntity = _cvar;
            m_pszConvarName = _cvar.m_pszName;
            m_pszValue = newValue;
            if (newValue.Contains(",") || newValue.Contains("."))
                ToFloat();
            else
                ToInt();

            EventManager.Notify(this);
        }
        private void ToFloat()
        {
            if (m_pszValue.Contains("f")) m_pszValue = m_pszValue.Replace("f", "");
            if (m_pszValue.Contains(".")) m_pszValue = m_pszValue.Replace(".", ",");
            if (float.TryParse(m_pszValue, out m_flValue))
                isFloat = true;
            return;
        }

        private void ToInt()
        {
            if (int.TryParse(m_pszValue, out m_nValue))
                isInt = true;
            return;
        }
    }
}
