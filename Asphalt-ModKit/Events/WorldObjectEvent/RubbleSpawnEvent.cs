using Asphalt.Api.Event;
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
    /// Called when an RubbleObject is added to the World
    /// </summary>
    public class RubbleSpawnEvent : CancellableEvent
    {
        public RubbleObject RubbleObject { get; set; }

        public RubbleSpawnEvent(ref RubbleObject obj) : base()
        {
            RubbleObject = obj;
        }
    }

    internal class RubbleSpawnEventHelper
    {
        public static void Postfix(ref IEcoObject ecoObject)
        {
            if (!(ecoObject is RubbleObject))
                return;

            RubbleObject rubble = (RubbleObject) ecoObject;

            RubbleSpawnEvent sre = new RubbleSpawnEvent(ref rubble);
            IEvent sreEvent = sre;

            EventManager.CallEvent(ref sreEvent);

            if (sre.IsCancelled())
                typeof(RubbleObject).GetMethod("Destroy", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(rubble, new object[] { });

        }
    }
}
