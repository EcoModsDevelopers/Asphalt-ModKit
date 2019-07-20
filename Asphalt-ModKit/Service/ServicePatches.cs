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
using System.Threading;

namespace Asphalt.Service
{
    [HarmonyPatch(typeof(ModDataSync), "InitMods")]
    internal static class InjectPatch
    {
        static void Prefix()
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                ServiceHelper.InjectValues();
#pragma warning restore CS0618 // Type or member is obsolete
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

    [HarmonyPatch(typeof(Thread), "Start", new Type[] { })]
    internal static class ConsoleCommandPatch2
    {
        static bool Prefix(Thread __instance)
        {
            if (__instance.Name == "Input Thread")
            {
                return false;
            }

            return true;
        }

    }

    [HarmonyPatch(typeof(ShutdownHooks), "AddShutdownHook")]
    internal static class ConsoleCommandPatch
    {
        static void Postfix()
        {
            new Thread(delegate ()
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

                    var startupType = Assembly.GetEntryAssembly().GetType("Eco.Server.Startup");
                    var fi = startupType.GetField("ShutDowned", BindingFlags.Static | BindingFlags.NonPublic);
                    var ShutDowned = fi.GetValue(null) as ManualResetEvent;
                    ShutDowned.Set();
                }
                catch (Exception e)
                {
                    Log.WriteLine(Localizer.DoStr("Caught an exception checking for console input, console input disabled. (probably safe to ignore)"));
                    Log.WriteLine(Localizer.DoStr(e.Message));
                }
            })
            {
                IsBackground = true,
                Name = "Asphalt Input Thread"
            }.Start();
        }
    }
}
