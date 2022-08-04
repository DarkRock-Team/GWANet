using GWANet.Main.Domain;
using Xunit;

namespace GWANet.Scanner.UnitTests.SignatureScanner;

public sealed class SignatureScannerEngineTests
{
    [Fact]
    public void BuildFullMatchTable_Should_ReturnValidTable()
    {
        var signatureScanner = new SignatureScannerEngineTestWrapper();
        var bytePattern = AobPatterns.ScanBasePtr;
        ushort[] expectedResults = {0, 1, 2, 3, 4, 5};
        var expectedResultSpan = new ReadOnlySpan<ushort>(expectedResults);

        var result = SignatureScannerEngineTestWrapper.BuildFullMatchTableWrapped(bytePattern);
        
        Assert.False(result.IsEmpty);
        Assert.True(result.Length == 6);
        var isSpanEqual = expectedResultSpan.SequenceEqual(result);
        Assert.True(isSpanEqual);
    }
}