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
        public IAtomicAction CreateAtomicAction(User user, AirPollutionComponent obj, float value)
        {
            WorldPolluteEvent wpe = new WorldPolluteEvent(user, obj, value);
            IEvent wpeEvent = wpe;

            EventManager.CallEvent(ref wpeEvent);

            if (!wpe.IsCancelled())
                return CreateAtomicAction_original(wpe.User, wpe.Component, wpe.Value);

            return new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
        }

        public IAtomicAction CreateAtomicAction_original(User user, AirPollutionComponent obj, float value)
        {
            throw new InvalidOperationException();
        }
    }
}
