using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using UnityEngine.InputSystem;

namespace PEAKTrails;

[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;
    internal static InputAction ToggleView = new("Toggle Trail", InputActionType.Value);
    internal static string LastBinding = string.Empty;

    private void Awake()
    {
        Log = Logger;
        ModConfig.Init(Config);
        Config.SettingChanged += OnSettingChanged;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        Log.LogInfo($"Plugin {Name} is loaded with version {Version}!");
    }

    private void Start()
    {
        //add our key
        ToggleView.AddBinding(ModConfig.ToggleKey.Value);
        LastBinding = ModConfig.ToggleKey.Value;
        ToggleView.Enable();
        InputSystem.actions.AddItem(ToggleView);
    }

    private void OnSettingChanged(object sender, SettingChangedEventArgs settingChangedArg)
    {
        if (settingChangedArg.ChangedSetting == null)
            return;

        if (settingChangedArg.ChangedSetting == ModConfig.ToggleKey)
        {
            ToggleView.ChangeBindingWithPath(LastBinding).Erase();
            ToggleView.AddBinding(ModConfig.ToggleKey.Value);
            LastBinding = ModConfig.ToggleKey.Value;
            return;
        }
    }
}
