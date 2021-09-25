using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GWANet.Context
{
    internal struct LoginCharacter
    {
        private string CharacterName;
    }
    internal struct PreGameContext
    {
        // 0x148 space
        public IEnumerable<LoginCharacter> Chars;
    }
}