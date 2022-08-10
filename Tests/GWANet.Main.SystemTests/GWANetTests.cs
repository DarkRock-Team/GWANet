using GWANet.Main.Settings;
using Xunit;

namespace GWANet.Main.SystemTests;

public sealed class GWANetTests : GWANetTestBase
{
    [Fact]
    public void Given_NullCharacterName_Initializes()
    {
        var gwaSettings = new InitializationSettings
        {
            CharacterName = string.Empty,
            ChangeGameTitle = false
        };
        var gwa = new GWANet();
        
        gwa.Initialize(gwaSettings);
        
        Assert.True(true);
    }
}