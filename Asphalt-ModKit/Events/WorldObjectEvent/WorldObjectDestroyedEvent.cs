﻿using Asphalt.Events;
using Eco.Gameplay.Objects;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectDestroyedEvent : CancellableEvent
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectDestroyedEvent(ref WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectDestroyedEventHelper
    {
        public static bool Prefix(ref WorldObject __instance, ref bool __state)
        {
            WorldObjectDestroyedEvent dEvent = new WorldObjectDestroyedEvent(ref __instance);
            IEvent iEvent = dEvent;

            EventManager.CallEvent(ref iEvent);

            if (dEvent.IsCancelled())
                return false;

            return true;
        }

    }
}
