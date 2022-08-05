using System;

namespace GWANet.Main.Exceptions;

[Serializable]
public sealed class EmptyCharacterNameException : InvalidOperationException
{
    public EmptyCharacterNameException() : base($"Character name is empty")
    {
    }
}