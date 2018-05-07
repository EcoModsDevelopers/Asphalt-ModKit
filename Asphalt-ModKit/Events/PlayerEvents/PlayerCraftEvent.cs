using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerCraftEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public CraftingComponent Table { get; set; }

        public Item Item { get; set; }

        public PlayerCraftEvent(Player pPlayer, CraftingComponent pTable, Item pItem) : base()
        {
            this.Player = pPlayer;
            this.Table = pTable;
            this.Item = pItem;
        }
    }

    internal class PlayerCraftEventHelper
    {
        public static bool Prefix(Player player, CraftingComponent table, Item item, ref IAtomicAction __result)
        {
            PlayerCraftEvent cEvent = new PlayerCraftEvent(player, table, item);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
