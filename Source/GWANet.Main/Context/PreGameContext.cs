using System.Collections.Generic;

namespace GWANet.Main.Context
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