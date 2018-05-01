using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when something pollutes
    /// </summary>
    public class WorldPolluteEvent : CancellableEvent
    {
        public User User { get; set; }
        public AirPollutionComponent Component { get; set; }
        public float Value { get; set; }

        public WorldPolluteEvent(User pUser, AirPollutionComponent pAirPollutionComponent, float pValue) : base()
        {
            User = pUser;
            Component = pAirPollutionComponent;
            Value = pValue;
        }
    }

    internal class WorldPolluteEventHelper
    {
        public static bool Prefix(User user, AirPollutionComponent obj, float value, ref IAtomicAction __result)
        {
            WorldPolluteEvent wpe = new WorldPolluteEvent(user, obj, value);
            IEvent wpeEvent = wpe;

            EventManager.CallEvent(ref wpeEvent);

            if (wpe.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
                return false;
            }

            return true;
        }
    }
}
