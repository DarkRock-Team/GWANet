using GWANet.Scanner.Definitions;
using GWANet.Scanner.SignatureScanner;
using GWANet.Scanner.SignatureScanner.Definitions;

namespace GWANet.Scanner.UnitTests.SignatureScanner;

public sealed class SignatureScannerEngineTestWrapper : SignatureScannerEngine
{
    public static ReadOnlySpan<ushort> BuildFullMatchTableWrapped(in BytePattern pattern)
    {
        return BuildFullMatchTable(pattern);
    }

    public unsafe PatternScanResult FindPatternSimpleWrapped(byte* data, int dataLength, BytePattern pattern)
    {
        return FindPatternSimple(data, dataLength, pattern);
    }

    public override unsafe PatternScanResult FindPattern(byte* data, int dataLength, BytePattern pattern)
    {
        throw new NotImplementedException();
    }
}