using GWANet.Scanner.Extensions;
using Xunit;

namespace GWANet.Scanner.UnitTests.Extensions;

public sealed class SpanSplitEnumeratorTests
{
    [Fact]
    public void SpanSplitEnumerator_Instantiates()
    {
        const char splitItem = '?';
        const string item = "A0?B0";
        var enumerator = new SpanSplitEnumerator<char>(item, splitItem);
        
        Assert.True(enumerator.SplitItem == splitItem);
        Assert.True(enumerator.Current.ToString() == item);
    }
    [Fact]
    public void SpanSplitEnumerator_MoveNext_Should_SetCurrent()
    {
        var enumerator = new SpanSplitEnumerator<char>("F0 AB B2 ?? 71", ' ');

        enumerator.MoveNext();

        Assert.True(enumerator.Current.IsEmpty is false);
        Assert.True(enumerator.Current.Length == 2);
        Assert.True(enumerator.Current.ToString().Equals("F0"));
    }

    [Theory]
    [InlineData("F0 AB B2 ?? 71", ' ')]
    [InlineData("FF ?? 00", ' ')]
    [InlineData("FFx??x00", 'x')]
    public void SpanSplitEnumeratorCurrent_Should_BeValid(string stringPattern, char splitItem)
    {
        var enumerator = new SpanSplitEnumerator<char>(stringPattern, splitItem);

        var currentItem = enumerator.Current;

        Assert.True(currentItem.IsEmpty is false);
    }
    [Fact]
    public void SpanSplitEnumeratorEmptySplit_Should_Work()
    {
        const string splitPattern = "";
        const char splitItem = ' ';
        var splitEnumerator = new SpanSplitEnumerator<char>(splitPattern.ToCharArray(), splitItem);

        Assert.True(splitEnumerator.MoveNext());
        Assert.Equal("", splitEnumerator.Current.ToString());
        Assert.False(splitEnumerator.MoveNext());
    }
}