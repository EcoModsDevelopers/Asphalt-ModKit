using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectNameChangedEvent : CancellableEvent
    {
        public User User { get; set; }

        public WorldObjectNameChangedEvent(User pUser) : base()
        {
            User = pUser;
        }
    }

    internal class WorldObjectNameChangedEventHelper
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
