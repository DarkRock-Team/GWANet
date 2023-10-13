using System;
namespace GWANet.Main.Exceptions
{
    [Serializable]
    public sealed class InvalidMemoryValueException : InvalidOperationException
    {
        public InvalidMemoryValueException(int ptrValue) 
            : base($"Invalid memory address, value: {ptrValue}")
        {
        }
    }
}
