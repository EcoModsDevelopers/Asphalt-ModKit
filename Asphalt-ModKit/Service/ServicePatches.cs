using Eco.Core.Serialization;
using Eco.Server;
using Harmony;

namespace Asphalt.Service
{
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
            ServiceHelper.InjectValues();
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
