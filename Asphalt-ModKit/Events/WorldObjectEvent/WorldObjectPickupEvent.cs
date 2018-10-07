using Asphalt.Api.Event;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Events.WorldObjectEvent
{
    public class WorldObjectPickupEvent : CancellableEvent
    {
        public WorldObject WorldObject { get; set; }
        public Player Picker { get; set; }

        public WorldObjectPickupEvent(ref WorldObject worldObject, ref Player picker) : base()
        {
            WorldObject = worldObject;
            Picker = picker;
        }
    }

    internal class WorldObjectPickupEventHelper
    {
        public static bool Prefix(ref Player player, ref WorldObject __instance, ref IAtomicAction __result)
        {
            var wope = new WorldObjectPickupEvent(ref __instance, ref player);
            var wopeEvent = (IEvent)wope;

            EventManager.CallEvent(ref wopeEvent);

            if (wope.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldObjectPickupEvent)));
                return false;
            }

            return true;
        }
    }
}