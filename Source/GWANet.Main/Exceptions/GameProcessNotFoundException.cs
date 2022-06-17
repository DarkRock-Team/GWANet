using System;

namespace GWANet.Main.Exceptions
{
    [Serializable]
    public sealed class GameProcessNotFoundException : InvalidOperationException
    {
        public GameProcessNotFoundException() : base("No running game processes were found")
        {

        }
    }
}
