using HarmonyLib;
using UnityEngine;
using Nautilus.Utility;
using System;

namespace BiggerSigns;

[HarmonyPatch]
public static class Patches
{
    private static Quaternion? _oldRotation = null;

    public static void InitSignInput(uGUI_SignInput __instance)
    {
        SetSignInputScale(__instance);

        __instance.inputField.richText = Plugin.ModConfig.EnableRichText;
        __instance.inputField.isRichTextEditingAllowed = Plugin.ModConfig.IsRichTextEditingAllowed;
        __instance.inputField.uppercase = Plugin.ModConfig.ForcedUppercase;

        if (Plugin.ModConfig.RemoveCharacterLimit)
        {
            __instance.inputField.characterLimit = int.MaxValue;
        }
    }

    public static void SetSignInputScale(uGUI_SignInput __instance)
    {
        __instance.baseScale = Plugin.ModConfig.BaseScale;
        __instance.scaleStep = Plugin.ModConfig.ScaleStep;
        __instance.scaleMin = Plugin.ModConfig.ScaleMin;
        __instance.scaleMax = Plugin.ModConfig.ScaleMax;
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.Awake))]
    public static class Patch_uGUI_SignInput_Awake
    {
        public static void Postfix(uGUI_SignInput __instance)
        {
            InitSignInput(__instance);
        }
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.UpdateScale))]
    public static class Patch_uGUI_SignInput_UpdateScale
    {
        public static void Prefix(uGUI_SignInput __instance)
        {
            SetSignInputScale(__instance);
        }

        public static void Postfix(uGUI_SignInput __instance)
        {
            // refresh to avoid low definition text
            __instance.inputField.textComponent.ForceMeshUpdate();
        }
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.scaleIndex))]
    [HarmonyPatch(MethodType.Setter)]
    public static class Patch_uGUI_SignInput_scaleIndex_Setter
    {
        public static void Prefix(uGUI_SignInput __instance)
        {
            SetSignInputScale(__instance);
        }
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.OnSelect))]
    public static class Patch_uGUI_SignInput_OnSelect
    {
        public static void Prefix(uGUI_SignInput __instance)
        {
            _oldRotation = null;

            InitSignInput(__instance);
        }
    }

    [HarmonyPatch(typeof(uGUI_SignInput), nameof(uGUI_SignInput.Update))]
    public static class Patch_uGUI_SignInput_Update
    {
        public static void Postfix(uGUI_SignInput __instance)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Plugin.Logger.LogInfo("........m");
                SNCameraRoot main = SNCameraRoot.main;
                main.SetFov(60);
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Plugin.Logger.LogInfo("........p");
                SNCameraRoot main = SNCameraRoot.main;
                main.SetFov(90);
            }

            if (__instance.focused)
            {
                string text = string.Empty;

                text += string.Format(
                    $"Scale index +{Plugin.ModConfig.ScaleMultiplier} ({uGUI.formatButton}), ",
                    KeyCodeUtils.GetDisplayTextForKeyCode(Plugin.ModConfig.ScaleMultiplierKey));

                text += string.Format(
                    $"Scale index −{Plugin.ModConfig.ScaleDivider} ({uGUI.formatButton}), ",
                    KeyCodeUtils.GetDisplayTextForKeyCode(Plugin.ModConfig.ScaleDividerKey)); ;

                text += string.Format(
                    $"Custom scale index 1 ={Plugin.ModConfig.CustomScale1} ({uGUI.formatButton}), ",
                    KeyCodeUtils.GetDisplayTextForKeyCode(Plugin.ModConfig.CustomScale1Key)); ;

                text += string.Format(
                       $"Custom scale index 2 ={Plugin.ModConfig.CustomScale2} ({uGUI.formatButton}), ",
                       KeyCodeUtils.GetDisplayTextForKeyCode(Plugin.ModConfig.CustomScale2Key)); ;

                text += string.Format(
                       $"Upright rotation ({uGUI.formatButton}), ",
                       KeyCodeUtils.GetDisplayTextForKeyCode(Plugin.ModConfig.UprightRotationKey)); ;

                // display text
                HandReticle.main.SetTextRaw(HandReticle.TextType.Use, text);
                HandReticle.main.SetTextRaw(HandReticle.TextType.UseSubscript,
                    $"Scale index min: {__instance.scaleMin}, Current scale index: {__instance.scaleIndex}, Scale index max: {__instance.scaleMax}");

                if (Input.GetKeyDown(Plugin.ModConfig.ScaleMultiplierKey))
                {
                    __instance.scaleIndex += Plugin.ModConfig.ScaleMultiplier;
                }
                else if (Input.GetKeyDown(Plugin.ModConfig.ScaleDividerKey))
                {
                    __instance.scaleIndex -= Plugin.ModConfig.ScaleDivider;
                }
                else if (Input.GetKeyDown(Plugin.ModConfig.CustomScale1Key))
                {
                    __instance.scaleIndex = Plugin.ModConfig.CustomScale1;
                }
                else if (Input.GetKeyDown(Plugin.ModConfig.CustomScale2Key))
                {
                    __instance.scaleIndex = Plugin.ModConfig.CustomScale2;
                }
                else if (Input.GetKeyDown(Plugin.ModConfig.UprightRotationKey))
                {
                    if (_oldRotation == null)
                    {
                        _oldRotation = __instance.transform.rotation;

                        Vector3 vector;
                        Vector3 vector2;

                        vector = __instance.transform.forward;
                        vector.y = 0f;
                        vector.Normalize();
                        vector2 = Vector3.up;

                        Quaternion rotation = Quaternion.LookRotation(vector, vector2);

                        __instance.transform.rotation = rotation;
                    }
                    else
                    {
                        __instance.transform.rotation = (Quaternion)_oldRotation;
                        _oldRotation = null;
                    }
                }
            }
        }
    }
}
