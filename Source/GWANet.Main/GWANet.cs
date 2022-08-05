using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GWANet.Main.Domain;
using GWANet.Main.Exceptions;
using GWANet.Scanner;
using GWANet.Scanner.Native.Enums;

namespace GWANet.Main
{
    public sealed class GWANet : IGWANet
    {
        private const string ProcessName = "Gw";
        private IMemScanner _memScanner; 

        public void Initialize(string characterName, bool isChangeGameTitle = false)
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
        private string ScanForCharacterName(in Process gameProcess)
        {
            _memScanner ??= new MemScanner(gameProcess);
            const int maxCharacterNameLengthSize = 40; // 2* maximum character name length
            
            var charNameScanResult = _memScanner.FindPattern(AobPatterns.CharacterName);

            if (!charNameScanResult.IsFound)
            {
                throw new PatternNotFoundException(nameof(AobPatterns.CharacterName));
            }
            
            var mainModuleBaseAddr = (int)gameProcess.MainModule!.BaseAddress;
            var charNameAddr = unchecked((UIntPtr) (mainModuleBaseAddr + charNameScanResult.Offset));

            _memScanner.Read(charNameAddr, out int charNamePtr);
            _memScanner.ReadBytes((IntPtr)(charNamePtr + 0x10), out var charNameBytes, maxCharacterNameLengthSize);
            var charName = Encoding.Unicode.GetString(charNameBytes, 0, charNameBytes.Length);
            
            if (string.IsNullOrEmpty(charName))
            {
                throw new EmptyCharacterNameException();
            }
                
            return charName;
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
