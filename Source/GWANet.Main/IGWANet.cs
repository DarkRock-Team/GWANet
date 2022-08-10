using System;
using GWANet.Main.Settings;

namespace GWANet.Main
{
    public interface IGWANet : IDisposable
    {
        public void Initialize(InitializationSettings settings = null);
    }
}
