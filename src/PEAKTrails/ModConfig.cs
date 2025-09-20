using BepInEx.Configuration;
using static PEAKTrails.Patching;

namespace PEAKTrails;
internal class ModConfig
{
    internal static ConfigEntry<float> TrailLength { get; private set; } = null!;
    internal static ConfigEntry<float> TrailStartWidth { get; private set; } = null!;
    internal static ConfigEntry<float> TrailEndWidth { get; private set; } = null!;
    internal static ConfigEntry<float> TrailStartAlpha { get; private set; } = null!;
    internal static ConfigEntry<float> TrailEndAlpha { get; private set; } = null!;

    internal static ConfigEntry<bool> ShowLocalTrail { get; private set; } = null!;
    internal static ConfigEntry<ShowTrailSetting> VisibilitySetting { get; private set; } = null!;
    internal static ConfigEntry<string> ToggleKey { get; private set; } = null!;
    internal static ConfigEntry<float> TogglePressDelay { get; private set; } = null!;
    internal static ConfigEntry<TrailAction> ToggleAction { get; private set; } = null!;
    internal static ConfigEntry<TrailItem> ToggleItem { get; private set; } = null!;

    internal static void Init(ConfigFile config)
    {
        TrailLength = config.Bind("General", "Trail Length", 360f, new ConfigDescription("This sets the length of the trail in seconds. The end of the trail will fade/disappear\nThe maximum value for this config item is 30 minutes", new AcceptableValueRange<float>(3f, 1800f)));
        TrailStartWidth = config.Bind("General", "Trail Start Width", 0.22f, new ConfigDescription("This sets the width of the trail line itself", new AcceptableValueRange<float>(0f, 1f)));
        TrailEndWidth = config.Bind("General", "Trail End Width", 0f, new ConfigDescription("This sets the width of the trail line itself", new AcceptableValueRange<float>(0f, 1f)));
        TrailStartAlpha = config.Bind("General", "Trail Start Alpha", 0.65f, new ConfigDescription("This sets the transparency of the start of the trail", new AcceptableValueRange<float>(0.05f, 1f)));
        TrailEndAlpha = config.Bind("General", "Trail End Alpha", 0.25f, new ConfigDescription("This sets the transparency of the end of the trail.", new AcceptableValueRange<float>(0.05f, 1f)));
        ShowLocalTrail = config.Bind("General", "Show Self Trail", true, "If disabled, will not render a trail for yourself (local client)");
        VisibilitySetting = config.Bind("General", "Visbility Setting", ShowTrailSetting.ToggleKey, "Determines how trail visibility will be handled.\nAlwaysOn means the trail will always be visible.\nToggleKey means the trail can be toggled on/off with a keybind.\nOnAction means the trail will be visible when a player is performing a specific action.\nOnHoldItem means the trail will be visible while holding a specific item.\nOnItemUseButton means the trail will be visible when a player is pressing the primary or secondary use buttons while holding a specific item.");
        ToggleKey = config.Bind("Controls", "Visibility Toggle Key", "<Keyboard>/t", "When Visbility Setting is set to ToggleKey, this key will be used to toggle the trail visibility.\nSet this to a valid unity control string.\nA listing of possible control strings can be found here: https://github.com/glarmer/PEAK-Unbound/blob/main/KeyBindValues.txt");
        TogglePressDelay = config.Bind("Controls", "Visibility Press Delay", 0.1f, new ConfigDescription("This sets the amount of time additional key presses will not affect trail visbility after the Visibility Toggle Key has been pressed.", new AcceptableValueRange<float>(0.1f, 3f)));
        ToggleAction = config.Bind("Controls", "Visibility Toggle Action", TrailAction.Reach, "When Visibility Setting is set to OnAction, this action will be used to display player trails");
        ToggleItem = config.Bind("Controls", "Visibility Toggle Item", TrailItem.Guidebook, "When Visibility Setting is set to OnHoldItem OR OnItemUsed, this item will be used to display player trails");

    }
}
