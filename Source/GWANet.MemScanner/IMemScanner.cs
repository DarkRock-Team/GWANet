using GWANet.MemScanner.Definitions;
using GWANet.MemScanner.SignatureScanner;
using System;
using System.Collections.Generic;

namespace GWANet.MemScanner
{
    internal interface IMemScanner : IDisposable
    {
        void Read<T>(UIntPtr memoryAddress, out T value) where T : unmanaged;
    }
}