using HarmonyLib;

namespace BiggerSigns;

[HarmonyPatch]
public static class Patches
{
    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.UpdateScale))]
    public static class Patch_uGUI_SignInput_UpdateScale
    {
        private static void Prefix(uGUI_SignInput __instance)
        {
            __instance.baseScale = Plugin.ModConfig.BaseScale;
            __instance.scaleStep = Plugin.ModConfig.ScaleStep;
        }
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.scaleIndex))]
    [HarmonyPatch(MethodType.Setter)]
    public static class Patch_uGUI_SignInput_scaleIndex_Setter
    {
        private static void Prefix(uGUI_SignInput __instance)
        {
            __instance.scaleMin = Plugin.ModConfig.ScaleMin;
            __instance.scaleMax = Plugin.ModConfig.ScaleMax;
        }
    }
}
