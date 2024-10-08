﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using System.Reflection;

namespace BiggerSigns
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }
        public static ModConfig ModConfig = OptionsPanelHandler.RegisterModOptions<ModConfig>();

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        private void Awake()
        {
            // set project-scoped logger instance
            Logger = base.Logger;

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnEnable()
        {
            if (ModConfig.FreeRecipe)
            {
                CraftData.techData[TechType.Sign]._ingredients = new CraftData.Ingredients();
            }
        }
    }
}
