using System;
using System.Diagnostics;
using GWANet.Domain;

namespace GWANet
{
    internal interface IMemScanner : IDisposable
    {
        void AddPattern(ulong returnAddress, BytePattern pattern);
        ulong AobScan(BytePattern bytePattern);
        ulong AssertionScan(string assertionFileName, string assertionMsg, long hexOffset);
    }
}