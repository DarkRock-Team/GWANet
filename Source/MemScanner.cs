using GWANet.Domain;
using GWANet.Native.Structs;
using GWANet.Source.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GWANet
{
    internal class MemScanner : IDisposable
    {
        private Process _gameProcess;

        private List<MEMORY_BASIC_INFORMATION> MemoryRegion { get; set; }

        public MemScanner(Process gameProcess)
        {
            if(gameProcess.Id == _gameProcess?.Id)
            {
                throw new ScannerIsAlreadyInitializedException(gameProcess?.Id.ToString());
            }  
            _gameProcess = gameProcess;
            InitializeMemoryRegions(_gameProcess.Handle);
        }

        private void InitializeMemoryRegions(IntPtr pHandle)
        {
            MemoryRegion = new List<MEMORY_BASIC_INFORMATION>();
            var regionAddress = new IntPtr();
            while (true)
            {
                var memInfo = new MEMORY_BASIC_INFORMATION();
                int memDump = Native.Imports.VirtualQueryEx(pHandle, regionAddress, out memInfo, (uint)Marshal.SizeOf(memInfo));
                // Check if VirtualQueryEx failed (returns 0)
                if (memDump == 0)
                {
                    break;
                }
                // TODO: change to CONSTS or ENUMs
                if ((memInfo.State & 0x1000) != 0 && (memInfo.Protect & 0x100) == 0)
                {
                    MemoryRegion.Add(memInfo);
                }
                regionAddress = new IntPtr(memInfo.BaseAddress.ToInt32() + (int)memInfo.RegionSize);
            }
        }
        protected IntPtr Scan(byte[] sIn, byte[] sFor)
        {
            int[] sBytes = new int[256]; int Pool = 0;
            int End = sFor.Length - 1;
            for (int i = 0; i < 256; i++)
                sBytes[i] = sFor.Length;
            for (int i = 0; i < End; i++)
                sBytes[sFor[i]] = End - i;
            while (Pool <= sIn.Length - sFor.Length)
            {
                for (int i = End; sIn[Pool + i] == sFor[i]; i--)
                    if (i == 0) return new IntPtr(Pool);
                Pool += sBytes[sIn[Pool + End]];
            }
            return IntPtr.Zero;
        }
        public IntPtr AobScan(BytePattern bytePattern)
        {
            for (int i = 0; i < MemoryRegion.Count; i++)
            {
                byte[] buff = new byte[MemoryRegion[i].RegionSize];
                Native.Imports.ReadProcessMemory(_gameProcess.Handle, MemoryRegion[i].BaseAddress, buff, (int)MemoryRegion[i].RegionSize, out _);

                IntPtr Result = Scan(buff, bytePattern.Pattern);
                if (Result != IntPtr.Zero)
                {
                    return new IntPtr(MemoryRegion[i].BaseAddress.ToInt32() + Result.ToInt32());
                }
            }
            return IntPtr.Zero;
        }

        public IntPtr AssertionScan(string assertionFileName, string assertionMsg, string hexOffset)
        {
            if (!string.IsNullOrEmpty(assertionFileName))
            {
                
            }
        }
        // After that, the class can be re-used
        public void PrepareForReuse()
        {
            MemoryRegion = null;
            _gameProcess = null;
        }

        public void Dispose()
        {
            _gameProcess?.Dispose();
        }
    }
}
