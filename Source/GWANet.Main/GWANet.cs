using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GWANet.Native.Enums;
using GWANet.Domain;
using GWANet.Main.Exceptions;

namespace GWANet
{
    public sealed class GWANet : IGWANet
    {
        private const string ProcessName = "Gw";
        private MemScanner _memScanner; 

        public void Initialize(string characterName, bool isChangeGameTitle)
        {
            var processes = Process.GetProcessesByName(ProcessName);
            if (processes is { Length: < 1 })
            {
                throw new GameProcessNotFoundException();
            }

            if (!string.IsNullOrEmpty(characterName))
            {
                foreach(var process in processes)
                {
                    ScanForCharacterName(process);
                }
            }
            // Initialize for a single game instance
            else
            {
                ScanForCharacterName(processes.First());
            }
        }
        private static IntPtr OpenProcess(int processId)
        {
            var processHandle = Native.Imports.OpenProcess((uint)ProcessAccessFlags.All, bInheritHandle: false, processId);
            return processHandle;
        }
        private string ScanForCharacterName(in Process gameProcess)
        {
            _memScanner ??= new MemScanner(gameProcess);

            var basePtr = _memScanner.AobScan(AobPatterns.ScanBasePtr);
            if (basePtr != 0)
            {
                
            }
            return string.Empty;
        }

        private void InitializeManagers()
        {
            
        }

        public void Dispose()
        {
            _memScanner.Dispose();
        }
    }
}
