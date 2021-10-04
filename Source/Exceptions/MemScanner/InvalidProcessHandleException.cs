using System;

namespace GWANet.Exceptions.MemScanner
{
    [Serializable]
    public sealed class InvalidProcessHandleException : GWANetException
    {
        public InvalidProcessHandleException(string processHandleValue) : base($"Process handle is invalid, value: {processHandleValue}")
        {

        }
    }
}
