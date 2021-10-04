using System;

namespace GWANet.Exceptions.MemScanner
{
    [Serializable]
    public sealed class ScannerIsAlreadyInitializedException : GWANetException
    {
        public ScannerIsAlreadyInitializedException(string pid) : base($"Scanner is already initialized for process id: {pid}")
        {

        }
    }
}
