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
        
        var isInitialized = gwa.Initialize(gwaSettings);
        
        Assert.True(isInitialized);
    }
    // TODO: Improve the test by making it less 'black boxy'
    [Fact]
    public void Given_CharacterName_Initializes()
    {
        var gwaSettings = new InitializationSettings
        {
            CharacterName = "what ever",
            ChangeGameTitle = true
        };
        var gwa = new GWANet();

        var isInitialized = gwa.Initialize(gwaSettings);

        Assert.True(isInitialized);
    }
    [Fact]
    public void Given_InitializeChat_Initializes()
    {
        var gwaSettings = new InitializationSettings
        {
            InitializeChat = true
        };
        var gwa = new GWANet();

        var isInitialized = gwa.Initialize(gwaSettings);

        Assert.True(isInitialized);
    }
}