using System.Collections;
using UnityEngine;
using static PEAKTrails.Patching;
using static PEAKTrails.Patching.PlayerVisualUpdatePatch;

namespace PEAKTrails;
internal class Misc
{
    internal static IEnumerator TransitionShowColor(TrailRenderer trail, Character __instance, float start, float end)
    {
        float currentStart = trail.startColor.a;
        float currentEnd = trail.endColor.a;

        while (ShowTrail && trail.startColor.a < start)
        {
            Color newColor = __instance.refs.customization.PlayerColor;
            currentStart += Time.deltaTime;
            currentEnd += Time.deltaTime;

            if (currentEnd > end)
                currentEnd = end;

            if (currentStart > start)
                currentStart = start;

            trail.startColor = new(newColor.r, newColor.g, newColor.b, currentStart);
            trail.endColor = new(newColor.r, newColor.g, newColor.b, currentEnd);

            yield return null;
        }
    }

    internal static IEnumerator TransitionHideColor(TrailRenderer trail, Character __instance)
    {
        float start = trail.startColor.a;
        float end = trail.endColor.a;

        while (!ShowTrail && trail.startColor.a > 0f)
        {
            Color newColor = __instance.refs.customization.PlayerColor;
            trail.startColor = new(newColor.r, newColor.g, newColor.b, start);
            trail.endColor = new(newColor.r, newColor.g, newColor.b, end);
            start -= Time.deltaTime;
            end -= Time.deltaTime;

            if (end < 0f)
                end = 0f;

            if (start < 0f)
                start = 0f;

            yield return null;
        }
    }

    internal static string GetEmoteFrom(TrailAction action)
    {
        if (action == TrailAction.DanceEmote)
            return "A_Scout_Emote_Dance1";

        if (action == TrailAction.NoNoEmote)
            return "A_Scout_Emote_Nono";

        if (action == TrailAction.SaluteEmote)
            return "A_Scout_Emote_Salute";

        if (action == TrailAction.ThinkEmote)
            return "A_Scout_Emote_Think";

        if (action == TrailAction.ThumbsUpEmote)
            return "A_Scout_Emote_ThumbsUp";

        if (action == TrailAction.CrossedArmsEmote)
            return "A_Scout_Emote_CrossedArms";

        if (action == TrailAction.PlayDeadEmote)
            return "A_Scout_Emote_Flex";

        if (action == TrailAction.ShrugEmote)
            return "A_Scout_Emote_Shrug";

        return "NoValidEmotesDetected";

    }
}
