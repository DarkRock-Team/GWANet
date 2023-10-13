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

        public bool Initialize(InitializationSettings settings = default)
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

            return true; 
        }

        private void InitializeMemScanner(in Process gameProcess)
        {
            _memScanner ??= new MemScanner(gameProcess);
            GamePointers.MainModuleBaseAddress = (int)gameProcess.MainModule!.BaseAddress;

            if(GamePointers.MainModuleBaseAddress <= 0)
            {
                throw new InvalidMemoryValueException(GamePointers.MainModuleBaseAddress);
            }
        }
        private string ScanForCharacterName()
        {
            const int maxCharacterNameLengthSize = 40; // 2* maximum character name length for unicode
            
            var charNameScanResult = _memScanner.FindPattern(AobPatterns.CharacterName);

            if (!charNameScanResult.IsFound)
            {
                throw new PatternNotFoundException(nameof(AobPatterns.CharacterName));
            }

            var charNameAddr = unchecked((UIntPtr) (GamePointers.MainModuleBaseAddress + charNameScanResult.Offset));
            _memScanner.Read(charNameAddr, out int charNamePtr);

            GamePointers.CharacterName = charNamePtr;
            _memScanner.ReadBytes(GamePointers.CharacterName, out var charNameBytes, maxCharacterNameLengthSize);

            var nullCharacterIndex = GetUnicodeNullCharacterIndex(in charNameBytes);
            var charName = Encoding.Unicode.GetString(charNameBytes, 0, nullCharacterIndex);
            
            if (string.IsNullOrEmpty(charName))
            {
                throw new EmptyCharacterNameException();
            }
                
            return charName;
        }

        /// <summary>
        /// Finds a NUL terminator represented as 0x00 in provided byte array
        /// </summary>
        /// <param name="unicodeArray">unicode byte array</param>
        /// <returns>array index at which the null terminator is found</returns>
        private static int GetUnicodeNullCharacterIndex(in byte[] unicodeArray)
        {
            const int minimumCharNameLength = 3;
            var successiveNullTerminatorsCount = 0;
            for (var i = minimumCharNameLength; i < unicodeArray.Length; i++)
            {

                if (unicodeArray[i] == 0x00)
                {
                    successiveNullTerminatorsCount++;
                }
                else
                {
                    successiveNullTerminatorsCount = 0;
                }
                if(successiveNullTerminatorsCount == 2)
                {
                    return i;
                }     
            }
            return unicodeArray.Length;
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
