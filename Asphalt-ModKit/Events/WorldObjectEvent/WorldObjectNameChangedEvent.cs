using Eco.Gameplay.Objects;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectNameChangedEvent : IEvent
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectNameChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectNameChangedEventHelper
    {
        public static void Prefix(WorldObject __instance, ref string __state)
        {
            __state = __instance.Name;
        }

        public static void Postfix(WorldObject __instance, ref string __state)
        {
            if (__state == __instance.Name)
                return;

            WorldObjectNameChangedEvent cEvent = new WorldObjectNameChangedEvent(__instance);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }
    }
}
