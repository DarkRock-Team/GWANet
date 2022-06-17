using GWANet.MemScanner.Definitions;

namespace GWANet.MemScanner
{
    internal interface IMemScanner : IDisposable
    {
        PatternScanResult FindPattern(BytePattern bytePattern);
        IEnumerable<PatternScanResult> FindPatterns(IReadOnlyList<BytePattern> bytePatterns);
        PatternScanResult AssertionScan(string assertionFileName, string assertionMsg, long hexOffset);
    }
}