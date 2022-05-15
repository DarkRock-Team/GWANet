namespace GWANet.Domain
{
    public class BytePattern
    {
        public byte[] Pattern { get; }
        public string Mask { get; }
        public long HexOffset { get; }

        public BytePattern(byte[] pattern, long offset, string mask = "")
        {
            Pattern = pattern;
            HexOffset = offset;
            Mask = mask;
        }
    }
}