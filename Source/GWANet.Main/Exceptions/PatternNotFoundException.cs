using System;

namespace GWANet.Main.Exceptions;

[Serializable]
public sealed class PatternNotFoundException : InvalidOperationException
{
    public PatternNotFoundException(string patternName) : base($"Pattern {patternName} was not found")
    {
    }
}