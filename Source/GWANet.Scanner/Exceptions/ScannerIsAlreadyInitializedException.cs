using System;

namespace GWANet.Scanner.Exceptions
{
    [Serializable]
    public sealed class ScannerIsAlreadyInitializedException : InvalidOperationException
    {
        public ScannerIsAlreadyInitializedException(string pid) : base($"Scanner is already initialized for process id: {pid}")
        {

        }
    }
}
