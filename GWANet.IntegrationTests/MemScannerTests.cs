using System;
using NUnit.Framework;
using GWANet;
using System.Diagnostics;
using System.Linq;
using GWANet.Domain;

namespace GWANet.IntegrationTests
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
        public void given_validGameProcess_when_ctor_doesnt_throw()
        {
            try
            {
                var memScanner = new MemScanner(_gameProcess);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected not exception when initializing {nameof(MemScanner)}, but got: " + ex.Message);
            }
        }

        /// <summary>
        /// This test is a subject to break when a new game version comes out
        /// </summary>
        [Test]
        public void given_baseScanAob_when_AobScan_returns_valid_pointer()
        {
            const ulong validBasePtr = 0x306211;
            var memScanner = new MemScanner(_gameProcess);
            
            var basePtr = memScanner.AobScan(AobPatterns.ScanBasePtr);
            if (validBasePtr == basePtr)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail($"{nameof(given_baseScanAob_when_AobScan_returns_valid_pointer)} failed, basePtr was {basePtr.ToString()}");
            }
        }
    }
}
