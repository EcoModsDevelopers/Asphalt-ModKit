using Asphalt.Api.Event;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Reflection;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when an Item in an Inventory gets moved
    /// </summary>
    public class InventoryMoveItemEvent : CancellableEvent
    {
        public ItemStack SourceStack { get; protected set; }
        public ItemStack DestinationStack { get; protected set; }
        public User User { get; protected set; }

        public InventoryMoveItemEvent(ItemStack source, ItemStack destination, User user) : base()
        {
            SourceStack = source;
            DestinationStack = destination;
            User = user;
        }
    }

    internal class InventoryMoveItemEventHelper
    {
        public static bool Prefix(ItemStack source, ItemStack destination, User user)
        {
            InventoryMoveItemEvent imie = new InventoryMoveItemEvent(source, destination, user);
            IEvent imieEvent = imie;

            EventManager.CallEvent(ref imieEvent);

            if (imie.IsCancelled())
                return false;

            return true;
        }
    }
}
