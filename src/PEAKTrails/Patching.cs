using HarmonyLib;
using UnityEngine;
using static PEAKTrails.Plugin;
using static Zorro.ControllerSupport.Rumble.RumbleClip;

namespace PEAKTrails;
internal class Patching
{
    [HarmonyPatch(typeof(Character), nameof(Character.Update))]
    internal class PlayerVisualUpdatePatch
    {
        internal static bool ShowTrail = true;
        internal static float UpdateDelay = 0f;

        private static void Postfix(Character __instance)
        {
            if (__instance.isBot)
                return;

            if (__instance.IsLocal)
                KeyListener();

            TrailRenderer trail = TryGetTrailRenderer(__instance);

            if (!__instance.data.enabled || __instance.data.dead || (__instance.IsLocal && !ModConfig.ShowLocalTrail.Value))
            {
                trail.emitting = false;
                return;
            }

            if(!trail.emitting)
                trail.emitting = true;

            SetTrailColor(__instance, trail);
        }

        private static void KeyListener()
        {
            if (!ModConfig.AddVisibilityToggle.Value)
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

        private static void SetTrailColor(Character __instance, TrailRenderer trail)
        {
            Color none = new(0f, 0f, 0f, 0f);
            if (ShowTrail)
            {
                Color playerColor = __instance.refs.customization.PlayerColor;
                trail.startColor = new(playerColor.r, playerColor.g, playerColor.b, 0.65f);
                trail.endColor = new(playerColor.r, playerColor.g, playerColor.b, 0.2f);
            }
            else
            {
                trail.startColor = none;
                trail.endColor = none;
            }
        }

        private static TrailRenderer TryGetTrailRenderer(Character __instance)
        {
            if(!__instance.refs.animationPositionTransform.gameObject.TryGetComponent(out TrailRenderer trail))
            {
                trail = __instance.refs.animationPositionTransform.gameObject.AddComponent<TrailRenderer>();
                Log.LogDebug("Trail created!");
            }
                
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.time = ModConfig.TrailLength.Value;
            trail.startWidth = ModConfig.TrailWidth.Value;
            trail.endWidth = 0f;
            
            return trail;
        }
    }
}
