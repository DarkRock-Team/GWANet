using GWANet.Domain;
using GWANet.Native.Structs;
using GWANet.Source.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GWANet
{
    internal class MemScanner
    {
        public bool IsInitialized { get; private set; }
        public Process GameProcess { get; private set; }

        protected List<MEMORY_BASIC_INFORMATION> MemoryRegion { get; set; }

        public void Initialize(Process gameProcess)
        {
            if(IsInitialized && gameProcess.Id == GameProcess?.Id)
            {
                throw new ScannerIsAlreadyInitializedException(gameProcess?.Id.ToString());
            }  
            GameProcess = gameProcess;
            InitializeMemoryRegions(GameProcess.Handle);

            IsInitialized = true;
        }

        protected void InitializeMemoryRegions(IntPtr pHandle)
        {
            MemoryRegion = new List<MEMORY_BASIC_INFORMATION>();
            var regionAddress = new IntPtr();
            while (true)
            {
                var MemInfo = new MEMORY_BASIC_INFORMATION();
                int MemDump = Native.Imports.VirtualQueryEx(pHandle, regionAddress, out MemInfo, (uint)Marshal.SizeOf(MemInfo));
                // Check if VirtualQueryEx failed (returns 0)
                if (MemDump == 0)
                {
                    break;
                }
                // TODO: change to CONSTS or ENUMs
                if ((MemInfo.State & 0x1000) != 0 && (MemInfo.Protect & 0x100) == 0)
                {
                    MemoryRegion.Add(MemInfo);
                }
                regionAddress = new IntPtr(MemInfo.BaseAddress.ToInt32() + (int)MemInfo.RegionSize);
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
            if (IsInitialized is false)
            {
                throw new ScannerIsNotInitializedException();
            }
            for (int i = 0; i < MemoryRegion.Count; i++)
            {
                byte[] buff = new byte[MemoryRegion[i].RegionSize];
                Native.Imports.ReadProcessMemory(GameProcess.Handle, MemoryRegion[i].BaseAddress, buff, (int)MemoryRegion[i].RegionSize, out _);

                IntPtr Result = Scan(buff, bytePattern.Pattern);
                if (Result != IntPtr.Zero)
                {
                    return new IntPtr(MemoryRegion[i].BaseAddress.ToInt32() + Result.ToInt32());
                }
            }
            return IntPtr.Zero;
        }
        // After that, the class can be re-used
        public void PrepareForReuse()
        {
            MemoryRegion = null;
            GameProcess = null;
            IsInitialized = false;
        }
    }
}
