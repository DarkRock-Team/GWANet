namespace GWANet.MemScanner.Definitions;

public struct PatternScanResult : IEquatable<PatternScanResult>
{
    public long Offset { get; init; }
    public bool IsFound 
        => Offset != -1;

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
}