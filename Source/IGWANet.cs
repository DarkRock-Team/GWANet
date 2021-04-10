using System;

namespace GWANet
{
    public interface IGWANet : IDisposable
    {
        public void Initialize(string characterName, bool isChangeGameTitle);
    }
}
