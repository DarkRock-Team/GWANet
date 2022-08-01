using System;

namespace GWANet.Scanner.Exceptions
{
    [Serializable]
    public sealed class InvalidProcessHandleException : InvalidOperationException
    {
        public InvalidProcessHandleException(string processHandleValue, bool hasExited) : base($"Process handle: {processHandleValue} is invalid, hasExited: {hasExited}")
        {
        }
    }
}
