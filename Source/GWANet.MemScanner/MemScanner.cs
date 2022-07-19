using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using GWANet.MemScanner.Definitions;
using GWANet.MemScanner.Exceptions;
using GWANet.MemScanner.Native;
using GWANet.MemScanner.SignatureScanner;

namespace GWANet.MemScanner
{
    /// <summary>
    /// GW specific memory scanner
    /// </summary>
    public unsafe class MemScanner : IMemScanner
    {
        private readonly Process _gameProcess;
        private readonly ISignatureScannerEngine _scannerEngine;

        private byte[] _moduleByteBuffer;
        private UIntPtr _moduleBaseAddress;
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

            Read(_moduleBaseAddress, out UIntPtr gameBaseAddress);

            if (Avx2.IsSupported)
            {
                _scannerEngine = new SignatureScannerAvxEngine();
                return;
            }
            if (Sse2.IsSupported)
            {
                _scannerEngine = new SignatureScannerSseEngine();
                return;
            }
            _scannerEngine = new SignatureScannerCompiledEngine();
        }

        private void DetermineSignatureScannerEngine()
        {

        }

        private void AssignModuleData(ProcessModule module)
        {
            if (module is null)
            {
                throw new InvalidProcessModuleException(_gameProcess.Id);
            }
            _moduleBaseAddress = unchecked((UIntPtr)(long)module.BaseAddress);
            _moduleByteBuffer = new byte[module.ModuleMemorySize];
            _moduleMemorySize = module.ModuleMemorySize;
        }

        public void Read<T>(UIntPtr memoryAddress, out T value) where T : unmanaged
        {
            int structSize = Unsafe.SizeOf<T>();
            byte[] buffer = GC.AllocateUninitializedArray<byte>(structSize, false);

            fixed (byte* bufferPtr = buffer)
            {
                var isSuccess = Imports.ReadProcessMemory(_gameProcess.Handle, memoryAddress, (UIntPtr)bufferPtr, (UIntPtr)structSize, out _);
                if (!isSuccess)
                {
                    throw new MemoryOperationException($"ReadProcessMemory failed to read from {memoryAddress}, bytes: {structSize}");
                }
                value = Unsafe.Read<T>((void*)memoryAddress);
            }
        }

        private bool PatternCheck(int nOffset, IEnumerable<byte> arrPattern)
        {
            return !arrPattern
                        .Where((patternByte, patternIndex) => patternByte != 0x0 && patternByte != _moduleByteBuffer[nOffset + patternIndex])
                        .Any();
        }

        public IEnumerable<PatternScanResult> FindPatterns(IReadOnlyList<BytePattern> bytePatterns)
        {
            var results = new PatternScanResult[bytePatterns.Count];
            Parallel.ForEach(Partitioner.Create(0, bytePatterns.Count), tuple =>
            {
                
                for (int x = tuple.Item1; x < tuple.Item2; x++)
                    results[x] = SignatureScanner.SignatureScanner.FindPattern(bytePatterns[x]);
            });
            return results;
        }

        public void Dispose()
        {
            _gameProcess?.Dispose();
        }
    }
}
