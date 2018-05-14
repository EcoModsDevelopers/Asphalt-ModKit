using Eco.Core.Serialization;
using Eco.ModKit;
using Eco.Server;
using Harmony;
using System;

namespace Asphalt.Service
{
    [HarmonyPatch(typeof(ModContentSync), "RefreshContent")]
    internal static class InjectPatch
    {
        static void Postfix()
        {
            ServiceHelper.InjectValues();
        }
    }

    [HarmonyPatch(typeof(EcoSerializer), "FinishDeserialization")]
    internal static class PreEnablePatch
    {
        static void Postfix()
        {
            ServiceHelper.CallMethod("OnPreEnable");
        }
    }

    [HarmonyPatch(typeof(PluginManager), "InitializePlugins")]
    internal static class EnablePatch
    {
        static void Postfix()
        {
            ServiceHelper.CallMethod("OnEnable");
        }
    }

    [HarmonyPatch(typeof(DataStore), "Unlock")]
    internal static class PostEnablePatch
    {
        static void Postfix()
        {
            ServiceHelper.CallMethod("OnPostEnable");
        }
    }
}
