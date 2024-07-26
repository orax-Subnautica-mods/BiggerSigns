using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace BiggerSigns
{
    [Menu(PluginInfo.PLUGIN_NAME)]
    public class ModConfig : ConfigFile
    {
        public const int SCALE_MIN = -3;
        public const int SCALE_MAX = 3;
        public const float SCALE_STEP = 0.0002f;
        public const float BASE_SCALE = 0.0010f;

        [Slider("Scale min", -5, 0, DefaultValue = SCALE_MIN)]
        public int ScaleMin = SCALE_MIN;

        [Slider("Scale max", 0, 20, DefaultValue = SCALE_MAX)]
        public int ScaleMax = SCALE_MAX;

        [Slider("Scale step", 0.0001f, 0.0009f, DefaultValue = SCALE_STEP, Format = "{0:F4}", Step = 0.0001f)]
        public float ScaleStep = SCALE_STEP;

        [Slider("Base scale", 0.0005f, 0.0090f, DefaultValue = BASE_SCALE, Format = "{0:F4}", Step = 0.0001f)]
        public float BaseScale = BASE_SCALE;
    }
}
