using System;

namespace GWANet.Exceptions
{
    public abstract class GWANetException : Exception
    {
        public GWANetException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
