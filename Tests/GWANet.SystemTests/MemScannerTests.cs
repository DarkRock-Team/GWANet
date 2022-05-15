using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace GWANet.SystemTests
{
    public class MemScannerTests : IDisposable
    {
        private readonly Process _gameProcess;
        private const string GameProcessName = "Gw";

        public MemScannerTests()
        {
            var systemProcesses = Process.GetProcesses();
            _gameProcess = systemProcesses.FirstOrDefault(p => p.ProcessName.Equals(GameProcessName));
            if (_gameProcess is null)
            {
                throw new InvalidOperationException("Game must be running for tests to run!");
            }
        }

        [Fact]
        public void Given_ValidGameProcess_Ctor_ShouldCreateMemScanner()
        {
            var memScanner = new MemScanner(_gameProcess);
            
            Assert.NotNull(memScanner);
        }

        public void Dispose()
        {
            _gameProcess?.Dispose();
        }
    }
}
