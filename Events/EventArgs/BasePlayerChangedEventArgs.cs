using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Events.EventArgs
{
    public enum BasePlayerStateChange
    {
        Connected,
        Disconnected
    }
    public class BasePlayerChangedEventArgs
    {
        public int Id;
        public string Name;
        public BasePlayerStateChange NewState;

        public BasePlayerChangedEventArgs(int _id, string _name, BasePlayerStateChange _state)
        {
            Id = _id;
            Name = _name;
            NewState = _state;
            EventManager.Notify(this);
        }
    }
}
