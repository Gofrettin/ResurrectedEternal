using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.BSPParse;
using ResurrectedEternal.Events.EventArgs;
using ResurrectedEternal.Memory;
using System.Collections.Generic;

namespace ResurrectedEternal.ClientObjects
{
    class MapManager
    {
        public bool VisibleCheckAvailable => Maps.ContainsKey(_currentMap) && Maps[_currentMap] != null;

        public bool m_bForceVisibleCheck
        {
            get
            {
                if (!VisibleCheckAvailable)
                    return false;
                if (!m_dwMap.FromDirectory)
                    return false;
                return true;
            }
        }

        public BSPFile m_dwMap => Maps[_currentMap];
        private Dictionary<string, BSPFile> Maps = new Dictionary<string, BSPFile>();

        private string _currentMap = "";

        private MemoryLoader MemoryLoader;

        private Client Client;

        //public event Action<string> OnMapChanged;

        public MapManager(Client _c)
        {
            Client = _c;
            MemoryLoader = MemoryLoader.instance;
        }

        private bool _isBusyLoading = false;
        public void Update()
        {

            if (string.IsNullOrEmpty(g_Globals.MapName))
                return;

            var _nextMap = g_Globals.MapName;

            if (_currentMap != _nextMap)
            {
                new MapChangedEventArgs(_currentMap, _nextMap);
                _currentMap = _nextMap;
            }

            if (Maps.ContainsKey(_currentMap) || _isBusyLoading)
                return;

            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                _isBusyLoading = true;
                Maps.Add(_currentMap, Generators.GenerateBSP(MemoryLoader.m_dwpszProcessDirectory, _currentMap));
                _isBusyLoading = false;

            });

        }

        public bool VisibleByMask(BaseEntity _entity)
        {
            return (_entity.m_iSpottedByMask & 1 << Client.m_iLocalPlayerIndex - 1) != 0;
        }

        public bool VisibleCheck(Vector3 from, Vector3 tp)
        {
            return m_dwMap.IsVisible(from, tp);
        }
    }
}
