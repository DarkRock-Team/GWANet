using System;

namespace GWANet.Exceptions
{
    public abstract class GWANetException : Exception
    {
        protected GWANetException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
