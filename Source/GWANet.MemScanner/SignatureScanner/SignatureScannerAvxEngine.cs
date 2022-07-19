using GWANet.MemScanner.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GWANet.MemScanner.SignatureScanner
{
    internal unsafe class SignatureScannerAvxEngine : ISignatureScannerEngine
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static PatternScanResult FindPattern(byte* data, int dataLength, BytePattern pattern)
        {

        }
    }
}
