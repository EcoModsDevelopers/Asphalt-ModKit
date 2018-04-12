using Asphalt.Events;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class WorldObjectOperatingChangedEvent : IEvent
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectOperatingChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectOperatingChangedEventHelper
    {
        private void set_Operating(bool pOperating)
        {
            WorldObject _this = (WorldObject)((object)this);
            bool before = _this.Operating;

            set_Operating_original(pOperating);

            if (before == _this.Operating)
                return;

            WorldObjectOperatingChangedEvent cEvent = new WorldObjectOperatingChangedEvent(_this);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }

        private void set_Operating_original(bool pOperating)
        {
            throw new InvalidOperationException();
        }

    }
}
