using Asphalt.Api.Event;
using Eco.Gameplay.Interactions;
using Eco.Shared.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events.WorldObjectEvent
{
    /// <summary>
    /// Called when a tree (or stump, branch, slice) is hit with an axe.
    /// </summary>
    public class TreeChopEvent : CancellableEvent
    {
        public TreeEntity TreeEntity { get; set; }
        public INetObject Damager { get; set; }
        public float DamageAmount { get; set; }
        public InteractionContext Context { get; set; }

        public TreeChopEvent(ref TreeEntity tree, ref INetObject damager, ref float amount, ref InteractionContext context)
        {
            TreeEntity = tree;
            Damager = damager;
            DamageAmount = amount;
            Context = context;
        }
    }

    internal class TreeChopEventHelper
    {
        public static bool Prefix(ref TreeEntity __instance, INetObject damager, float amount, InteractionContext context)
        {
            var tce = new TreeChopEvent(ref __instance, ref damager, ref amount, ref context);
            var tceEvent = (IEvent)tce;

            EventManager.CallEvent(ref tceEvent);

            return tce.IsCancelled();
        }
    }
}
