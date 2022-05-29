using System;
using System.Diagnostics;
using System.Linq;

namespace GWANet.SystemTests;

public abstract class TestBase : IDisposable
{
    protected readonly Process GameProcess;
    public const string GameProcessName = "Gw";

    protected TestBase()
    {
        var systemProcesses = Process.GetProcesses();
        GameProcess = systemProcesses.FirstOrDefault(p => p.ProcessName.Equals(GameProcessName));
        if (GameProcess is null)
        {
            throw new InvalidOperationException("Game must be running for tests to run!");
        }
    }

    public void Dispose()
    {
        GameProcess?.Dispose();
        GC.SuppressFinalize(this);
    }
}