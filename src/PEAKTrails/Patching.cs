using HarmonyLib;
using pworld.Scripts.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static PEAKTrails.Plugin;

namespace PEAKTrails;
internal class Patching
{
    internal enum ShowTrailSetting
    {
        AlwaysOn,
        ToggleKey,
        OnActionShow,
        OnActionHide,
        OnHoldItem,
        OnItemUseButton
    }

    internal enum TrailAction
    {
        Reach,
        Crouch,
        SaluteEmote,
        ThumbsUpEmote,
        NoNoEmote,
        ThinkEmote,
        PlayDeadEmote,
        ShrugEmote,
        CrossedArmsEmote,
        DanceEmote,
    }

    internal enum TrailItem
    {
        Binoculars,
        BingBong,
        Bugle,
        Coconut,
        Conch,
        Compass,
        Flare,
        Guidebook,
        Lantern
    }

    internal static readonly List<string> ValidItems = ["Binoculars", "Bing Bong", "Bugle", "Coconut", "Conch", "Compass", "Flare", "Guidebook", "Lantern"];

    [HarmonyPatch(typeof(Character), nameof(Character.Update))]
    internal class PlayerVisualUpdatePatch
    {
        internal static bool ShowTrail = true;
        internal static float TimeSinceLastChange = 0f;
        internal static float UpdateDelay = 0f;

        private static void Postfix(Character __instance)
        {
            if (__instance.isBot)
                return;

            bool status = ShowTrail;

            if (__instance.IsLocal)
                UpdateLocalTrailControls(__instance);

            TrailRenderer trail = TryGetTrailRenderer(__instance);

            if (!__instance.data.enabled || __instance.data.dead || (__instance.IsLocal && !ModConfig.ShowLocalTrail.Value))
            {
                trail.emitting = false;
                return;
            }

            if(!trail.emitting)
                trail.emitting = true;

            if (status != ShowTrail)
                UpdateTrailColor(__instance, trail);
            else
                TimeSinceLastChange += Time.deltaTime;
        }

        private static void UpdateLocalTrailControls(Character __instance)
        {
            if (ModConfig.VisibilitySetting.Value == ShowTrailSetting.AlwaysOn)
                AlwaysOn();
            else if (ModConfig.VisibilitySetting.Value == ShowTrailSetting.ToggleKey)
                KeyListener();
            else if (ActionSettings(ModConfig.VisibilitySetting.Value))
                ActionListener(__instance);
            else
                ItemListener(__instance);

        }

        private static bool ActionSettings(ShowTrailSetting setting)
        {
            if(setting == ShowTrailSetting.OnActionHide || setting == ShowTrailSetting.OnActionShow)
                return true;

            return false;
        }

        private static void AlwaysOn()
        {
            if (!ShowTrail)
                ShowTrail = true;
        }

        private static void ActionListener(Character __instance)
        {
            if(ModConfig.VisibilitySetting.Value == ShowTrailSetting.OnActionShow)
                ShowTrail = ActionShowTrail(__instance);
            else
                ShowTrail = !ActionShowTrail(__instance);
        }

        private static bool ActionShowTrail(Character __instance)
        {
            if (ModConfig.ToggleAction.Value == TrailAction.Reach)
                return __instance.data.isReaching;
            else if (ModConfig.ToggleAction.Value == TrailAction.Crouch)
                return __instance.data.isCrouching;
            else
            {
                string emoteName = Misc.GetEmoteFrom(ModConfig.ToggleAction.Value);
                return __instance.refs.animator.IsPlaying(emoteName);
            }
        }

        private static void ItemListener(Character __instance)
        {
            Item item = __instance.data.currentItem;
            if (item == null)
            {
                ShowTrail = false;
                return;
            }

            if(IsValidItem(item))
            {
                if (ModConfig.VisibilitySetting.Value == ShowTrailSetting.OnHoldItem)
                    ShowTrail = true;
                else
                {
                    ShowTrail = CharacterInput.action_usePrimary.IsPressed() || CharacterInput.action_useSecondary.IsPressed();
                }

                return;
            }

            ShowTrail = false;
        }

        private static bool IsValidItem(Item item)
        {
            if (item == null) 
                return false;

            return ValidItems.Any(x => item.UIData.itemName.Contains(x, System.StringComparison.InvariantCultureIgnoreCase) && ((int)ModConfig.ToggleItem.Value) == ValidItems.IndexOf(x));

        }

        private static void KeyListener()
        {
            if (ModConfig.VisibilitySetting.Value != ShowTrailSetting.ToggleKey)
                return;

            if(UpdateDelay > 0f)
                UpdateDelay -= Time.deltaTime;

            if(ToggleView.IsPressed() && UpdateDelay <= 0f)
            {
                ShowTrail = !ShowTrail;
                UpdateDelay = ModConfig.TogglePressDelay.Value; //add delay after pressing the button (aproximate value in seconds)
                Log.LogMessage($"Trail toggled to [ {ShowTrail} ]");
            }
                
        }

        private static void UpdateTrailColor(Character __instance, TrailRenderer trail)
        {
            TimeSinceLastChange = 0f;
            if (ShowTrail)
                __instance.StartCoroutine(Misc.TransitionShowColor(trail, __instance, ModConfig.TrailStartAlpha.Value, ModConfig.TrailEndAlpha.Value));
            else
            {
                __instance.StartCoroutine(Misc.TransitionHideColor(trail, __instance));
            }
        }

        private static TrailRenderer TryGetTrailRenderer(Character __instance)
        {
            if(!__instance.refs.animationPositionTransform.gameObject.TryGetComponent(out TrailRenderer trail))
            {
                trail = __instance.refs.animationPositionTransform.gameObject.AddComponent<TrailRenderer>();
                Color playerColor = __instance.refs.customization.PlayerColor;
                trail.startColor = new(playerColor.r, playerColor.g, playerColor.b, ModConfig.TrailStartAlpha.Value);
                trail.endColor = new(playerColor.g, playerColor.b, playerColor.a, ModConfig.TrailEndAlpha.Value);
                Log.LogDebug("Trail created!");
            }
                
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.time = ModConfig.TrailLength.Value;
            trail.startWidth = ModConfig.TrailStartWidth.Value;
            trail.endWidth = ModConfig.TrailEndWidth.Value;
            
            return trail;
        }
    }
}
