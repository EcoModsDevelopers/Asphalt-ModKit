using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

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
        public IAtomicAction CreateAtomicAction(Player player, CraftingComponent table, Item item)
        {
            PlayerCraftEvent pcfe = new PlayerCraftEvent(player, table, item);
            IEvent pcfEvent = pcfe;

            EventManager.CallEvent(ref pcfEvent);

            if (!pcfe.IsCancelled())
                return CreateAtomicAction_original(pcfe.Player, pcfe.Table, pcfe.Item);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, CraftingComponent table, Item item)
        {
            throw new InvalidOperationException();
        }
    }
}
