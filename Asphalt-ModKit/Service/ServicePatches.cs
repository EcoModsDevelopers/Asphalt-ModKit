using Asphalt.Api.Event;
using Asphalt.Events.Console;
using Eco.Core;
using Eco.Core.Serialization;
using Eco.ModKit;
using Eco.Server;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Service
{
    [HarmonyPatch(typeof(ModDataSync), "InitMods")]
    internal static class InjectPatch
    {
        static void Prefix()
        {
            try
            {
                ServiceHelper.InjectValues();
            }
            catch (Exception e)
            {
                Log.WriteError(new LocString(e.ToString()));
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
                Log.WriteError(new LocString(e.ToString()));
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(Eco.Server.PluginManager), "InitializePlugins")]
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
                Log.WriteError(new LocString(e.ToString()));
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(Eco.Server.PluginManager), "StartPlugins")]
    internal static class PostEnablePatch
    {
        static void Prefix()
        {
            try
            {
                ServiceHelper.CallMethod("OnPostEnable");
            }
            catch (Exception e)
            {
                Log.WriteError(new LocString(e.ToString()));
                throw;
            }
        }
    }

    [HarmonyPatch(typeof(ShutdownHooks), "AddShutdownHook")]
    internal static class ConsoleCommandPatch
    {
        static void Postfix()
        {
            try
            {
                var line = string.Empty;
                while (line != "exit" && line != "stop")
                {
                    line = Console.ReadLine() ?? string.Empty;
                    line = line.Trim();

                    IEvent evt = new ConsoleInputEvent(line);
                    EventManager.CallEvent(ref evt);
                }

                //call Stop()
                Type startupType = Assembly.GetEntryAssembly().GetType("Eco.Server.Startup");
                var mi = startupType.GetMethod("Stop", BindingFlags.Static | BindingFlags.NonPublic);
                mi.Invoke(null, new object[] { });
            }
            catch (Exception e)
            {
                Log.WriteLine(Localizer.DoStr("Caught an exception checking for console input, console input disabled. (probably safe to ignore)"));
                Log.WriteLine(Localizer.DoStr(e.Message));
            }
        }
    }
}
