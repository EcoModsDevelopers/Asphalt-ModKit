/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 29, 2018
* ------------------------------------
**/

using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Util;
using Asphalt.Util;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Utils;
using Harmony;
using System;
using System.Reflection;
using System.Security.Principal;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        public static bool IsInitialized { get; protected set; }

        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            if (!IsAdministrator)
                Log.WriteError("If Asphalt is not working, try running Eco as Administrator!");

            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());  //Patch injections for default Services onEnable etc.

            IsInitialized = true;
        }

        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public string GetStatus()
        {
            return IsInitialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
