using System;

namespace GWANet.Main
{
    public interface IGWANet : IDisposable
    {
        public void Initialize(string characterName, bool isChangeGameTitle);
    }
}
