using System;

namespace GWANet.Main.Exceptions;

[Serializable]
public sealed class InvalidCharacterNameException : InvalidOperationException
{
    public InvalidCharacterNameException(string characterName) : base($"Invalid character name: {characterName}")
    {
    }
}