using System;

namespace GWANet.MemScanner.Exceptions
{
    [Serializable]
    public sealed class InvalidProcessModuleException : InvalidOperationException
    {
        public InvalidProcessModuleException(int processId) : base($"Module of the process with PID {processId.ToString()} is null")
        {

        }
    }
}