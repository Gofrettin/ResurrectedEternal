using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects
{
    class NetworkStringTable : ClientObject
    {
        public event Action<int> OnWalkFinished;
        public event Action OnWalkStarted;
        public bool IsValid = false;

        public NetworkStringTable(IntPtr moduleAddress, uint offset) : base(moduleAddress, offset)
        {

        }

        public void ReValidate()
        {
            Init();
        }

        private Dictionary<string, int> _models = new Dictionary<string, int>();
        private void Init()
        {
            IsValid = true;
            _models.Clear();
            OnWalkStarted?.Invoke();
            var _entry = MemoryLoader.instance.Reader.Read<IntPtr>(Pointer + 0x40);
            var _first = MemoryLoader.instance.Reader.Read<IntPtr>(_entry + 0xC);

            if (_first == IntPtr.Zero)
            {
                IsValid = false;
                return;
            }


            for (int i = 0; i < 1024; i++)
            {
                var _nameAddress = MemoryLoader.instance.Reader.Read<IntPtr>(_first + 0xC + (i * 0x34)); //52 = 13 bytes // + 12 // + 0x40 0x104
                var name = MemoryLoader.instance.Reader.ReadString(_nameAddress, Encoding.ASCII);
                //Console.WriteLine(name);
                if (string.IsNullOrEmpty(name) || !name.StartsWith("model") || _models.ContainsKey(name)) continue;
                _models.Add(name, i);
            }
            //foreach (var item in _models)
            //{
            //    System.IO.File.AppendAllText("models.txt", item.Key + " - " + item.Value + "\n");
            //}
            OnWalkFinished?.Invoke(_models.Count);
        }

        public uint GetModelByIndex(string model)
        {
            if (_models.ContainsKey(model))
                return Convert.ToUInt16(_models[model]);
            else
                return 0;
        }


        //public 
    }
}
