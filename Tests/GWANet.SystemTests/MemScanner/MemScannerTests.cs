using Xunit;

namespace GWANet.SystemTests.MemScanner
{
    public class MemScannerTests : TestBase
    {
        [Fact]
        public void Given_ValidGameProcess_Ctor_ShouldCreateMemScanner()
        {
            var memScanner = new global::GWANet.MemScanner(GameProcess);
            
            Assert.NotNull(memScanner);
        }
        [Fact]
        public void Given_ValidPointers_AobScan_ShouldReturnValidAdresses()
        {
            var memScanner = new global::GWANet.MemScanner(GameProcess);
            
            var basePtr = memScanner.AobScan(AobPatterns.ScanBasePtr);
            var agentBasePtr = memScanner.AobScan(AobPatterns.ScanAgentBasePtr);
            // maxAgents = agentBasePtr + 0x8
            var myIdPtr = memScanner.AobScan(AobPatterns.PlayerAgentIdPtr);
            
            Assert.NotNull(memScanner);
        }
    }
}
