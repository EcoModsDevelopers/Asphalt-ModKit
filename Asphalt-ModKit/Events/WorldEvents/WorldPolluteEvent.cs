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
    /// <summary>
    /// Called when something pollutes
    /// </summary>
    public class WorldPolluteEvent : ICancellable, IEvent
    {
        private bool cancel = false;

        public User User { get; protected set; }
        public AirPollutionComponent Component { get; protected set; }
        public float Value { get; set; }

        public WorldPolluteEvent(User pUser, AirPollutionComponent pAirPollutionComponent, float pValue) : base()
        {
            User = pUser;
            Component = pAirPollutionComponent;
            Value = pValue;
        }

        public bool IsCancelled()
        {
            return this.cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.cancel = cancel;
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
                return CreateAtomicAction_original(user, obj, wpe.Value);

            return new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
        }

        public IAtomicAction CreateAtomicAction_original(User user, AirPollutionComponent obj, float value)
        {
            throw new InvalidOperationException();
        }
    }
}
