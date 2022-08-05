using Xunit;

namespace GWANet.Main.SystemTests;

public sealed class GWANetTests : GWANetTestBase
{
    [Fact]
    public void Given_NullCharacterName_Initializes()
    {
        var gwa = new GWANet();
        gwa.Initialize(null);
        
        Assert.True(true);
    }
}