using System.ComponentModel;

namespace GWANet.Main.Settings;

public sealed class InitializationSettings
{
    [DefaultValue("")]
    public string CharacterName { get; init; }
    [DefaultValue(false)]
    public bool ChangeGameTitle { get; init; }
    [DefaultValue(false)]
    public bool InitializeChat { get; set; }
}