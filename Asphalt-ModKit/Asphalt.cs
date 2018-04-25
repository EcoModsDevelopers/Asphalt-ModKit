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
using System.Reflection;
using System.Security.Principal;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        public static bool IsInitialized { get; protected set; }

        static Asphalt()
        {
            if (!IsAdministrator)
                Log.WriteError("If Asphalt is not working, try running Eco as Administrator!");

            //<OnNameChanged>k__BackingField

            /*    Injection.Install(
                    typeof(ThreadSafeAction).GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public),
                    typeof(AsphaltThreadSafeAction).GetMethod("AsphaltInvoke", BindingFlags.Instance | BindingFlags.Public),
                    typeof(AsphaltThreadSafeAction).GetMethod("Invoke_original", BindingFlags.Instance | BindingFlags.Public));


                FieldInfo fi = typeof(WorldObject).GetField("<OnNameChanged>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                foreach (WorldObject wo in WorldObjectManager.All)
                    fi.SetValue(wo, new AsphaltThreadSafeAction());
                    */

            // Injection.Install(typeof(WorldObject).GetField("<OnNameChanged>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic),           )

            AsphaltDependencyInjectionHelper.init();

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
