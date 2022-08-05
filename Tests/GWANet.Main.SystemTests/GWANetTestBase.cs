using GWANet.Main.SystemTests.Settings;
using Microsoft.Extensions.Configuration;

namespace GWANet.Main.SystemTests;

public abstract class GWANetTestBase
{
    protected readonly TestSettings TestSettings;
    public GWANetTestBase()
    {
        var configManager = new ConfigurationManager();
        TestSettings = configManager.GetSection("TestConfig") as TestSettings;
    }
}