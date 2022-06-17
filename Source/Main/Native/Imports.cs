using GWANet.Native.Enums;
using GWANet.Native.Structs;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GWANet.Native
{
    internal class Imports
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
            => OpenProcess((uint)flags, false, proc.Id);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, ulong lpBaseAddress, byte[] lpBuffer, int dwSize, int lpNumberOfBytesRead = 0);

        [DllImport("kernel32.dll")]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
    }
}
