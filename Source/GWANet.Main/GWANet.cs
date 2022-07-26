using System.Diagnostics;
using System.Linq;
using GWANet.Main.Domain;
using GWANet.Main.Exceptions;
using GWANet.Scanner;

namespace GWANet.Main
{
    public sealed class GWANet : IGWANet
    {
        private const string ProcessName = "Gw";
        private IMemScanner _memScanner; 

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
        private string ScanForCharacterName(in Process gameProcess)
        {
            _memScanner ??= new Scanner.MemScanner(gameProcess);

            var basePtr = _memScanner.FindPattern(AobPatterns.ScanBasePtr);
            if (basePtr.IsFound)
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
