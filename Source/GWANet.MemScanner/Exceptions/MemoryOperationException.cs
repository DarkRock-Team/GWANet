using System;
using System.Runtime.Serialization;

namespace GWANet.MemScanner.Exceptions
{
    [Serializable]
    internal sealed class MemoryOperationException : InvalidOperationException
    {
        public MemoryOperationException(string message) : base(message)
        {
        }
        protected MemoryOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
