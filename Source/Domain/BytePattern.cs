namespace GWANet.Domain
{
    internal class BytePattern
    {
        public byte[] Pattern { get; }
        public string Mask { get; }
        public string HexOffset { get; }

        public BytePattern(byte[] pattern, string offset, string mask = "")
        {
            Pattern = pattern;
            HexOffset = offset;
            Mask = mask;
        }
    }
}