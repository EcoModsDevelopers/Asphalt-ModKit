using Eco.Core.Serialization;
using Eco.ModKit;
using Eco.Server;
using Eco.Shared.Utils;
using Harmony;
using System;

namespace Asphalt.Service
{
    [HarmonyPatch(typeof(ModContentSync), "RefreshContent")]
    internal static class InjectPatch
    {
        static void Postfix()
        {
            try
            {
                ServiceHelper.InjectValues();
            }
            catch (Exception e)
            {
                Log.WriteError(e.ToString());
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(EcoSerializer), "FinishDeserialization")]
    internal static class PreEnablePatch
    {
        static void Postfix()
        {
            try
            {
                ServiceHelper.CallMethod("OnPreEnable");
            }
            catch (Exception e)
            {
                Log.WriteError(e.ToString());
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(PluginManager), "InitializePlugins")]
    internal static class EnablePatch
    {
        static void Postfix()
        {
            try
            {
                ServiceHelper.CallMethod("OnEnable");
            }
            catch (Exception e)
            {
                Log.WriteError(e.ToString());
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(DataStore), "Unlock")]
    internal static class PostEnablePatch
    {
        static void Postfix()
        {
            try
            {
                ServiceHelper.CallMethod("OnPostEnable");
            }
            catch (Exception e)
            {
                Log.WriteError(e.ToString());
                throw;
            }
        }
    }
}
