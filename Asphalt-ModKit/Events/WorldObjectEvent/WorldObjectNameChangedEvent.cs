using Asphalt.Events;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using System;

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
        public void SetName(string newName)
        {
            WorldObject _this = (WorldObject)((object)this);
            string before = _this.Name;

            SetName_original(newName);

            if (before == _this.Name)
                return;

            WorldObjectNameChangedEvent cEvent = new WorldObjectNameChangedEvent(_this);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }

        public void SetName_original(string newName)
        {
            throw new InvalidOperationException();
        }

    }
}
