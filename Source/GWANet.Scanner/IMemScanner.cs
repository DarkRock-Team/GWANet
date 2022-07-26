using System;
using System.Collections.Generic;
using GWANet.Scanner.Definitions;
using GWANet.Scanner.SignatureScanner.Definitions;

namespace GWANet.Scanner;

public interface IMemScanner : IDisposable
{
    unsafe void Read<T>(UIntPtr memoryAddress, out T value) where T : unmanaged;
    unsafe IEnumerable<PatternScanResult> FindPatterns(IReadOnlyList<BytePattern> bytePatterns);
    unsafe PatternScanResult FindPattern(BytePattern bytePattern, long offset = 0);
}