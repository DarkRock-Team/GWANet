using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWANet.Exceptions;
using GWANet.Native.Enums;

namespace GWANet
{
    public class GWANet : IGWANet
    {
        private const string ProcessName = "Guild Wars";

        public void Initialize(string characterName, bool isChangeGameTitle)
        {
            var processes = Process.GetProcessesByName(ProcessName);
            if (processes != null && processes.Length < 1)
            {
                throw new GameProcessNotFoundException();
            }

            if (!string.IsNullOrEmpty(characterName))
            {
                foreach(var process in processes)
                {
                    ScanForCharacterName();
                }
            }
            // Initialize for a single game instance
            else
            {
                ScanForCharacterName();
            }
        }
        private static IntPtr OpenProcess(int processId)
        {
            var processHandle = Native.Imports.OpenProcess((uint)ProcessAccessFlags.All, bInheritHandle: false, processId);
            return processHandle;
        }
        private string ScanForCharacterName()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
