using GWANet.Scanner.Extensions;
using Xunit;

namespace GWANet.Scanner.UnitTests.Extensions;

public class SpanSplitEnumeratorTests
{
    [Fact]
    public void MoveNext_Should_SetCurrent()
    {
        
    }

    [Theory]
    [InlineData("F0 AB B2 ?? 71", ' ')]
    public void SpanSplitEnumerator(string stringPattern, char splitItem)
    {
        var enumerator = new SpanSplitEnumerator<char>(stringPattern, splitItem);
    }
}