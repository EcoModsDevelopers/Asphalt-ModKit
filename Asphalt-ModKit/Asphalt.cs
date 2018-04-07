/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 29, 2018
* ------------------------------------
**/

using Asphalt.Api.Event;
using Asphalt.Api.Event.Player;
using Asphalt.Api.Util;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Utils;
using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        bool Initialized;

        public Asphalt()
        {
            /*
            Injection.Install(
                typeof(ChatManager).GetMethod("SendChat", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(PlayerSendMessageEventHelper).GetMethod("SendChat", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(PlayerSendMessageEventHelper).GetMethod("SendChat_original", BindingFlags.Instance | BindingFlags.NonPublic)
                );
                */

            if (!IsAdministrator)
            {
                for (int i = 0; i < 10; i++)
                {
                    Log.WriteError("Asphalt requires, that you run Eco as Administrator!");
                }
            }

            Injection.Install(
                     typeof(MessagePlayerActionManager).GetMethod("CreateAtomicAction", BindingFlags.Instance | BindingFlags.Public),
                     typeof(PlayerSendMessageEventHelper).GetMethod("CreateAtomicAction", BindingFlags.Instance | BindingFlags.Public),
                     typeof(PlayerSendMessageEventHelper).GetMethod("CreateAtomicAction_original", BindingFlags.Instance | BindingFlags.Public)
                     );

            this.Initialized = true;
        }


        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public string GetStatus()
        {
            return Initialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
