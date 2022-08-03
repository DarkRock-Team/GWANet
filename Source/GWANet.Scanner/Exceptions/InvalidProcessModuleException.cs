using System;

namespace GWANet.Scanner.Exceptions
{
    [Serializable]
    public sealed class InvalidProcessModuleException : InvalidOperationException
    {
        public InvalidProcessModuleException(int processId, long moduleMemorySize) : base(
            $"Module of the process with PID {processId.ToString()} is invalid," +
            $" module memory page size: {moduleMemorySize}")
        {

        }
    }
}