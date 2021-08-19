using ResurrectedEternal.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ClientObjects
{
    class Engine : ClientObject
    {
        private IntPtr GetPointer()
        {
            return MemoryLoader.instance.Reader.Read<IntPtr>(ModuleAddress + (int)g_Globals.Offset.dwClientState);
        }

        private SignonState GetSignOnState()
        {
            var _ptr = GetPointer();
            if (_ptr == IntPtr.Zero)
                return SignonState.None;
            return (SignonState)MemoryLoader.instance.Reader.Read<int>(_ptr + (int)g_Globals.Offset.GameState);
        }

        public SignonState SignonState => (SignonState)MemoryLoader.instance.Reader.Read<int>(Pointer + (int)g_Globals.Offset.GameState);
        public IntPtr GlobalVarsPointer => MemoryLoader.instance.Reader.Read<IntPtr>(ModuleAddress + (int)g_Globals.Offset.dwGlobalVars);

        //add globalvars as single entities?
        public GlobalVarsBase GlobalVars => MemoryLoader.instance.Reader.Read<GlobalVarsBase>(ModuleAddress + (int)g_Globals.Offset.dwGlobalVars);

        public string CurrentMapName => MemoryLoader.instance.Reader.ReadString(Pointer + (int)g_Globals.Offset.dwClientState_MapDirectory, Encoding.UTF8, 128);

        public IntPtr getPtr => (Pointer + (int)g_Globals.Offset.dwClientState_MapDirectory);

        public Engine(IntPtr moduleAddress, uint offset) : base(moduleAddress, offset)
        {



        }

        public int MaxPlayers => MemoryLoader.instance.Reader.Read<int>(Pointer + (int)g_Globals.Offset.dwClientState_MaxPlayer);


        public bool IsInGame => SignonState == SignonState.Full;
        public bool InMenu => SignonState == SignonState.None;

        public bool TeamSelect => SignonState == SignonState.PreSpawn;

        public bool Connecting => SignonState == SignonState.Connected;


        //public void CreateNeonCham(float brightness)
        //{
        //    var thisPtr = MemoryLoader.instance.Reader.Read<uint>(MemoryLoader.instance.Modules["engine.dll"] + (int)g_Globals.Offset.m_model_ambient_min - 0x2C);


        //    MemoryLoader.instance.Reader.Write(MemoryLoader.instance.Modules["engine.dll"] + (int)g_Globals.Offset.m_model_ambient_min, Generators.XOR(thisPtr, brightness));
        //    //BitConverter.GetBytes(thisPtr ^ XOR(brightness / 255f)));
        //}

        //public float m_flLastTick
        //{
        //    get { return GlobalVars.m_flIntervalPerTick}
        //}

        //private DateTime _lastForce = DateTime.Now.AddMinutes(-1);
        //private TimeSpan _next = TimeSpan.FromSeconds(5);

        //public void ForceUpdate()
        //{
        //    if (DateTime.Now - _lastForce < _next)
        //        return;

        //    _lastForce = DateTime.Now;

        //    MemoryLoader.instance.Reader.Write(Pointer + 0x174, -1);
        //}




    }

}
