using GWANet.Exceptions;
using System;

namespace GWANet.Source.Exceptions
{
    [Serializable]
    public sealed class ScannerIsNotInitializedException : GWANetException
    {
        public ScannerIsNotInitializedException() : base("Scanner is not initialized for the selected game process!")
        {

        }
    }
}
