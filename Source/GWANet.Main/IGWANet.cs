using System;
using GWANet.Main.Settings;

namespace GWANet.Main
{
    public interface IGWANet : IDisposable
    {
        public bool Initialize(InitializationSettings settings = null);
    }
}
