using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using GWANet.Scanner.Native.Enums;
using GWANet.Scanner.Native.Structs;

namespace GWANet.Scanner.Native
{
    internal class Imports
    {
        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
            => OpenProcess((uint)flags, false, proc.Id);

        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WriteProcessMemory([In] IntPtr hProcess, [In] UIntPtr lpBaseAddress, [In] UIntPtr lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory([In] IntPtr hProcess, [In] UIntPtr lpBaseAddress, UIntPtr lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesRead);
        
        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory([In] IntPtr hProcess, [In] IntPtr lpBaseAddress, IntPtr lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int VirtualQueryEx([In] IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
    }
}
