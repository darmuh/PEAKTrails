using BepInEx.Configuration;

namespace PEAKTrails;
internal class ModConfig
{
    internal static ConfigEntry<float> TrailLength { get; private set; } = null!;
    internal static ConfigEntry<float> TrailWidth { get; private set; } = null!;
    internal static ConfigEntry<bool> ShowLocalTrail { get; private set; } = null!;
    internal static ConfigEntry<bool> AddVisibilityToggle { get; private set; } = null!;
    internal static ConfigEntry<string> ToggleKey { get; private set; } = null!;
    internal static ConfigEntry<float> TogglePressDelay { get; private set; } = null!;

    internal static void Init(ConfigFile config)
    {
        TrailLength = config.Bind("General", "Trail Length", 360f, new ConfigDescription("This sets the length of the trail in seconds. The end of the trail will fade/disappear\nThe maximum value for this config item is 30 minutes", new AcceptableValueRange<float>(3f, 1800f)));
        TrailWidth = config.Bind("General", "Trail Width", 0.22f, new ConfigDescription("This sets the width of the trail line itself", new AcceptableValueRange<float>(0.1f, 1f)));
        ShowLocalTrail = config.Bind("General", "Show Self Trail", true, "If disabled, will not render a trail for yourself (local client)");
        AddVisibilityToggle = config.Bind("General", "Add Visibility Control", true, "When enabled, adds a configurable keybind to enable/disable trail visibility.\nTrails will still be created while not visible");
        ToggleKey = config.Bind("Controls", "Visibility Toggle Key", "<Keyboard>/t", "Set this to a valid unity control string.\nA listing of possible control strings can be found here: https://github.com/glarmer/PEAK-Unbound/blob/main/KeyBindValues.txt");
        TogglePressDelay = config.Bind("Controls", "Visibility Press Delay", 0.1f, new ConfigDescription("This sets the amount of time additional key presses will not affect trail visbility after the Visibility Toggle Key has been pressed.", new AcceptableValueRange<float>(0.1f, 3f)));

    }
}
