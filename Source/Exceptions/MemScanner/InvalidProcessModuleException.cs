using System;

namespace GWANet.Exceptions.MemScanner
{
    [Serializable]
    public sealed class InvalidProcessModuleException : GWANetException
    {
        public InvalidProcessModuleException(int processId) : base($"Module of the process with PID {processId.ToString()} is null")
        {

        }
    }
}