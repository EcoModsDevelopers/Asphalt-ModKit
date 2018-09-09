using Asphalt.Api.Event;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when the Player changes the Selected Hotbar Slot
    /// </summary>
    public class InventoryChangeSelectedSlotEvent : IEvent
    {
        public Player Player { get; protected set; }
        public int SelectedSlot { get; protected set; }
        public ItemStack SelectedStack { get; protected set; }
        public SelectionInventory Inventory { get; protected set; }

        public InventoryChangeSelectedSlotEvent(int slot, Player player, ItemStack itemStack, SelectionInventory inv)
        {
            SelectedSlot = slot;
            Player = player;
            SelectedStack = itemStack;
            Inventory = inv;
        }
    }

    internal class InventoryChangeSelectedSlotEventHelper
    {
        public static void Prefix(Player player, int slot, SelectionInventory __instance)
        {
            InventoryChangeSelectedSlotEvent csse = new InventoryChangeSelectedSlotEvent(slot, player, __instance.SelectedStack, __instance);
            IEvent csseEvent = csse;

            EventManager.CallEvent(ref csseEvent);
        }
    }
}
