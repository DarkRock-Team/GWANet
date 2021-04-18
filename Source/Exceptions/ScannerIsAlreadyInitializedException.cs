using GWANet.Exceptions;
using System;

namespace GWANet.Source.Exceptions
{
    [Serializable]
    public sealed class ScannerIsAlreadyInitializedException : GWANetException
    {
        public ScannerIsAlreadyInitializedException(string pid) : base($"Scanner is already initialized for process id: {pid}")
        {

        }
    }
}
