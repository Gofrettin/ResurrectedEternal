using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events.EventArgs
{
    public class RemoveConvarEventArgs
    {
        public string m_pszConvarName;

        public RemoveConvarEventArgs(string name)
        {
            m_pszConvarName = name;
            EventManager.Notify(this);
        }
    }
}
