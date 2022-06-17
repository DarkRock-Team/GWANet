namespace GWANet.MemScanner.Exceptions
{
    [Serializable]
    public sealed class InvalidProcessHandleException : InvalidOperationException
    {
        public InvalidProcessHandleException(string processHandleValue) : base($"Process handle is invalid, value: {processHandleValue}")
        {
        }
    }
}
