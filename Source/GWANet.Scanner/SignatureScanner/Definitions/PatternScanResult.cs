using System;
using System.Runtime.CompilerServices;

namespace GWANet.Scanner.SignatureScanner.Definitions;

public struct PatternScanResult : IEquatable<PatternScanResult>
{
    public long Offset { get; internal set; }
    public bool IsFound
        => Offset != -1;

    public PatternScanResult(long offset)
    {
        Offset = offset;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PatternScanResult AddOffset(long offset)
    {
        return Offset != -1 ? new PatternScanResult(Offset + offset) : this;
    }
    public bool Equals(PatternScanResult other)
    {
        return Offset == other.Offset;
    }

    public override bool Equals(object? obj)
    {
        return obj is PatternScanResult other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Offset.GetHashCode();
    }

    public static bool operator ==(PatternScanResult left, PatternScanResult right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PatternScanResult left, PatternScanResult right)
    {
        return !(left == right);
    }
}