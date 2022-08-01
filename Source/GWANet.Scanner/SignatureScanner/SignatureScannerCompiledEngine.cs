using GWANet.Scanner.Definitions;
using GWANet.Scanner.SignatureScanner.Definitions;

namespace GWANet.Scanner.SignatureScanner
{
    internal unsafe class SignatureScannerCompiledEngine : SignatureScannerEngine
    {
        public override PatternScanResult FindPattern(byte* data, int dataLength, BytePattern pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}
