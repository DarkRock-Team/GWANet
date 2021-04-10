using System;

namespace GWANet.Exceptions
{
    [Serializable]
    public sealed class GameProcessNotFoundException : GWANetException
    {
        public GameProcessNotFoundException() : base("No running game processes were found")
        {

        }
    }
}
