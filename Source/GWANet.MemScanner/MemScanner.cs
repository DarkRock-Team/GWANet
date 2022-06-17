using System.Diagnostics;
using GWANet.MemScanner.Definitions;
using GWANet.MemScanner.Exceptions;

namespace GWANet.MemScanner
{
    public class MemScanner : IMemScanner
    {
        private readonly Process _gameProcess;
        
        private byte[] _moduleByteBuffer;
        private ulong _moduleBaseAddress;
        private int _moduleMemorySize;
        private Dictionary<ulong, BytePattern> bytePatterns { get; }

        public MemScanner(Process gameProcess, ProcessModule targetModule = null)
        {
            if (gameProcess.Handle == IntPtr.Zero)
            {
                throw new InvalidProcessHandleException(gameProcess.Handle.ToString());
            }
            _gameProcess = gameProcess;

            AssignModuleData(targetModule ?? gameProcess.MainModule);

            Imports.ReadProcessMemory(_gameProcess.Handle, _moduleBaseAddress, _moduleByteBuffer, _moduleMemorySize);
        }

        private void AssignModuleData(ProcessModule module)
        {
            if (module is null)
            {
                throw new InvalidProcessModuleException(_gameProcess.Id);
            }
            _moduleBaseAddress = (ulong)module.BaseAddress;
            _moduleByteBuffer = new byte[module.ModuleMemorySize];
            _moduleMemorySize = module.ModuleMemorySize;
        }

        private bool PatternCheck(int nOffset, IEnumerable<byte> arrPattern)
        {
            return !arrPattern
                        .Where((patternByte, patternIndex) => patternByte != 0x0 && patternByte != _moduleByteBuffer[nOffset + patternIndex])
                        .Any();
        }

        public IEnumerable<PatternScanResult> FindPatterns(IReadOnlyList<BytePattern> bytePatterns)
        {
            throw new NotImplementedException();
        }

        PatternScanResult IMemScanner.AssertionScan(string assertionFileName, string assertionMsg, long hexOffset)
        {
            throw new NotImplementedException();
        }

        PatternScanResult IMemScanner.FindPattern(BytePattern bytePattern)
        {
            for (var moduleIndex = 0; moduleIndex < _moduleByteBuffer.Length; moduleIndex++)
            {
                // Check the beginning bytes of the module
                if (_moduleByteBuffer[moduleIndex] != bytePattern.Pattern[0])
                {
                    continue;
                }
                    

                if (PatternCheck(moduleIndex, bytePattern.Pattern))
                {
                    if (bytePattern.HexOffset < 0)
                    {
                        return (_moduleBaseAddress + (ulong)moduleIndex) - (ulong)bytePattern.HexOffset;
                    }
                    else
                    {
                        return (_moduleBaseAddress + (ulong)moduleIndex) + (ulong)bytePattern.HexOffset;
                    }
                }
            }
            return 0;
        }

        public ulong AssertionScan(string assertionFileName, string assertionMsg, long hexOffset)
        {
            throw new NotImplementedException();
            if (!string.IsNullOrEmpty(assertionFileName))
            {
                
            }
        }

        public void Dispose()
        {
            _gameProcess?.Dispose();
        }
    }
}
