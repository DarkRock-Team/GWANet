using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GWANet.Main.Domain;
using GWANet.Main.Exceptions;
using GWANet.Main.Modules;
using GWANet.Main.Settings;
using RapidMemory;
using RapidMemory.Definitions;

namespace GWANet.Main
{
    public sealed class GWANet : IGWANet
    {
        private const string ProcessName = "Gw";
        private IMemScanner _memScanner; 

        public void Initialize(InitializationSettings settings = default)
        {
            var gwProcess = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (gwProcess is null)
            {
                throw new GameProcessNotFoundException();
            }
            InitializeMemScanner(gwProcess);

            if (settings!.ChangeGameTitle && !string.IsNullOrEmpty(settings.CharacterName))
            {
                var characterName = ScanForCharacterName();
            }

            var patternsToScan = new List<BytePattern>();
            AddGameAobsToList(ref patternsToScan);
            
            if (settings.InitializeChat)
            {
                ChatModule.AddChatAobsToList(ref patternsToScan);
            }
            
        }

        private void InitializeMemScanner(in Process gameProcess)
        {
            _memScanner ??= new MemScanner(gameProcess);
            GamePointers.MainModuleBaseAddress = (int)gameProcess.MainModule!.BaseAddress;
        }
        private string ScanForCharacterName()
        {
            const int maxCharacterNameLengthSize = 40; // 2* maximum character name length
            
            var charNameScanResult = _memScanner.FindPattern(AobPatterns.CharacterName);

            if (!charNameScanResult.IsFound)
            {
                throw new PatternNotFoundException(nameof(AobPatterns.CharacterName));
            }

            var charNameAddr = unchecked((UIntPtr) (GamePointers.MainModuleBaseAddress + charNameScanResult.Offset));

            _memScanner.Read(charNameAddr, out int charNamePtr);
            GamePointers.CharacterName = (IntPtr)charNamePtr + 0x10;
            _memScanner.ReadBytes(GamePointers.CharacterName, out var charNameBytes, maxCharacterNameLengthSize);
            var charName = Encoding.Unicode.GetString(charNameBytes, 0, charNameBytes.Length);
            
            if (string.IsNullOrEmpty(charName))
            {
                throw new EmptyCharacterNameException();
            }
                
            return charName;
        }

        private static void AddGameAobsToList(ref List<BytePattern> patternsToScan)
        {
            if (!patternsToScan.Any())
            {
                patternsToScan = new List<BytePattern>();
            }
            patternsToScan.AddRange(new List<BytePattern>
            {
                AobPatterns.ScanBasePtr,
                // Agent
                AobPatterns.ScanAgentBasePtr,
                AobPatterns.ChangeTargetFunc,
                AobPatterns.PlayerAgentIdPtr,
                AobPatterns.SendDialog,
                AobPatterns.InteractAgent
            });
        }

        public void Dispose()
        {
            _memScanner.Dispose();
        }
    }
}
