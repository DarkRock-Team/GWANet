using System;
using NUnit.Framework;
using GWANet;
using System.Diagnostics;
using System.Linq;

namespace GWANet.UnitTests
{
    [TestFixture]
    public class MemScannerTests
    {
        private Process _gameProcess;
        private const string GameProcessName = "Gw";

        [SetUp]
        public void SetUp()
        {
            var systemProcesses = Process.GetProcesses();
            _gameProcess = systemProcesses.FirstOrDefault(p => p.ProcessName.Equals(GameProcessName));
            if (_gameProcess is null)
            {
                throw new InvalidOperationException("Game must be running for tests to run!");
            }
        }
        [TearDown]
        public void TearDown()
        {
            _gameProcess?.Dispose();
        }

        [Test]
        public void given_validGameProcess_when_Initialize_IsInitialized_is_true()
        {
            var memScanner = new MemScanner();

            memScanner.Initialize(_gameProcess);

            Assert.True(memScanner.IsInitialized);
        }
        //[Fact]
        //public void given_validGameProcess_when_Initialize_IsInitialized_is_true()
        //{
        //    var ss = new MemScanner();
        //}

        
    }
}
