﻿using Eco.Gameplay.Objects;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectEnabledChangedEvent : IEvent
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectEnabledChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectEnabledChangedEventHelper
    {
        public static void Prefix(WorldObject __instance, ref bool __state)
        {
            __state = __instance.Enabled;
        }

        public static void Postfix(WorldObject __instance, ref bool __state)
        {
            if (__state == __instance.Enabled)
                return;

            WorldObjectEnabledChangedEvent cEvent = new WorldObjectEnabledChangedEvent(__instance);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }

    }
}
