using GWANet.Scanner.Extensions;
using Xunit;

namespace GWANet.Scanner.UnitTests.Extensions;

public sealed class SpanSplitEnumeratorTests
{
    [Fact]
    public void MoveNext_Should_SetCurrent()
    {
        var enumerator = new SpanSplitEnumerator<char>("F0 AB B2 ?? 71", ' ');
        var currentItem = enumerator.Current;
        
        enumerator.MoveNext();

        Assert.True(currentItem.IsEmpty is false);
        Assert.True(currentItem.Length == 2);
        Assert.True(currentItem.ToString().Equals("F0"));
    }

    [Theory]
    [InlineData("F0 AB B2 ?? 71", ' ')]
    public void SpanSplitEnumeratorCurrent_Should_BeValid(string stringPattern, char splitItem)
    {
        var enumerator = new SpanSplitEnumerator<char>(stringPattern, splitItem);

        var currentItem = enumerator.Current;

        Assert.True(currentItem.IsEmpty is false);
        Assert.True(currentItem.Length == 14);
    }
}