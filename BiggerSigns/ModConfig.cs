using Nautilus.Json;
using Nautilus.Options.Attributes;
using UnityEngine;

namespace BiggerSigns
{
    [Menu(PluginInfo.PLUGIN_NAME)]
    public class ModConfig : ConfigFile
    {
        public const string STR_GAME_DEFAULT = "Game default:";
        public const string STR_RESTART_REQUIRED = "Game restart required to take effect.";

        public const int SLIDER_MAX = 100;

        public const int SCALE_MIN = -3;
        public const int SCALE_MAX = 20;
        public const int SCALE_ADD = 10;
        public const int SCALE_SUB = SCALE_ADD;
        public const int SCALE_CUST1 = 0;
        public const int SCALE_CUST2 = 5;
        public const float SCALE_STEP = 0.0002f;
        public const float BASE_SCALE = 0.0010f;

        [Slider("Scale min", -5, 0, DefaultValue = SCALE_MIN, Tooltip = STR_GAME_DEFAULT + " -3")]
        public int ScaleMin = SCALE_MIN;

        [Slider("Scale max", 0, SLIDER_MAX, Tooltip = STR_GAME_DEFAULT + " 3")]
        public int ScaleMax = SCALE_MAX;

        [Slider("Scale step (advanced)", 0.0001f, 0.0009f, DefaultValue = SCALE_STEP, Format = "{0:F4}", Step = 0.0001f,
            Tooltip = "This is an advanced option. All your existing \"Signs\" will be modified! " + STR_RESTART_REQUIRED)]
        public float ScaleStep = SCALE_STEP;

        [Slider("Base scale (advanced)", 0.0005f, 0.0090f, DefaultValue = BASE_SCALE, Format = "{0:F4}", Step = 0.0001f,
            Tooltip = "This is an advanced option. All your existing \"Signs\" will be modified! " + STR_RESTART_REQUIRED)]
        public float BaseScale = BASE_SCALE;

        [Toggle("Forced uppercase",
            Tooltip = STR_GAME_DEFAULT + " true (forced).")]
        public bool ForcedUppercase = true;

        [Toggle("Enable RichText",
            Tooltip = STR_GAME_DEFAULT + " true (enabled). " +
            "Recommendations: enable \"Remove character limit\" " +
            "and disable \"Forced uppercase\" if you want to write RichText.")]
        public bool EnableRichText = true;

        [Toggle("Is RichText editing allowed",
            Tooltip = STR_GAME_DEFAULT + " false (not allowed).")]
        public bool IsRichTextEditingAllowed = false;

        [Toggle("Remove character limit")]
        public bool RemoveCharacterLimit = true;

        [Slider("Add scale by...", 1, SLIDER_MAX, DefaultValue = SCALE_ADD, Step = 1,
            Tooltip = STR_GAME_DEFAULT + " 1")]
        public int ScaleMultiplier = SCALE_ADD;

        [Keybind("Add scale by... (keybind)")]
        public KeyCode ScaleMultiplierKey = KeyCode.PageUp;

        [Slider("Subtract scale by...", 1, SLIDER_MAX, DefaultValue = SCALE_SUB, Step = 1,
            Tooltip = STR_GAME_DEFAULT + " 1")]
        public int ScaleDivider = SCALE_SUB;

        [Keybind("Subtract scale by... (keybind)")]
        public KeyCode ScaleDividerKey = KeyCode.PageDown;

        [Slider("Custom scale 1", 0, SLIDER_MAX, Step = 1)]
        public int CustomScale1 = SCALE_CUST1;

        [Keybind("Custom scale 1 (keybind)")]
        public KeyCode CustomScale1Key = KeyCode.Home;

        [Slider("Custom scale 2", 0, SLIDER_MAX, Step = 1)]
        public int CustomScale2 = SCALE_CUST2;

        [Keybind("Custom scale 2 (keybind)")]
        public KeyCode CustomScale2Key = KeyCode.End;

        [Keybind("Upright rotation")]
        public KeyCode UprightRotationKey = KeyCode.UpArrow;

        [Toggle("Free recipe to create the Sign object", Tooltip = STR_RESTART_REQUIRED)]
        public bool FreeRecipe = false;
    }
}
