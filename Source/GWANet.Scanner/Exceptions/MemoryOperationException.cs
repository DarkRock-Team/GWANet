using System;
using System.Runtime.Serialization;

namespace GWANet.Scanner.Exceptions
{
    [Serializable]
    internal sealed class MemoryOperationException : InvalidOperationException
    {
        public MemoryOperationException(string message) : base(message)
        {
        }

        internal MemoryOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
