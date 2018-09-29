﻿using Asphalt.Api.Event;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Reflection;

namespace Asphalt.Events.WorldObjectEvent
{
    /// <summary>
    /// Called when a Tree is felled.
    /// </summary>
    public class TreeFellEvent : CancellableEvent
    {
        public TreeEntity TreeEntity { get; set; }
        public INetObject Killer { get; set; }

        public TreeFellEvent(ref TreeEntity tree, ref INetObject killer) : base()
        {
            TreeEntity = tree;
            Killer = killer;
        }
    }

    internal class TreeFellEventHelper
    {
        public static bool Prefix(ref TreeEntity __instance, ref INetObject killer)
        {
            TreeFellEvent tfe = new TreeFellEvent(ref __instance, ref killer);
            IEvent tfeEvent = tfe;

            EventManager.CallEvent(ref tfeEvent);

            if (tfe.IsCancelled())
                return false;

            return true;
        }
    }
}
