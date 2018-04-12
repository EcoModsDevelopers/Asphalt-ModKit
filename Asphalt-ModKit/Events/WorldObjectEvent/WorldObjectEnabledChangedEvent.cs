using Asphalt.Events;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectEnabledChangedEvent : IEvent
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectEnabledChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectEnabledChangedEventHelper
    {
        private void set_Enabled(bool pEnabled)
        {
            WorldObject _this = (WorldObject)((object)this);
            bool before = _this.Enabled;

            set_Enabled_original(pEnabled);

            if (before == _this.Enabled)
                return;

            WorldObjectEnabledChangedEvent cEvent = new WorldObjectEnabledChangedEvent(_this);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }

        private void set_Enabled_original(bool pOperating)
        {
            throw new InvalidOperationException();
        }

    }
}
