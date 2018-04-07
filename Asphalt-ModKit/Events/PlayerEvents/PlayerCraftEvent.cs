using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /**
     * Called when a player sends a chat message;
     * */
    public class PlayerCraftEvent : ICancellable, IEvent
    {
        private bool cancel = false;

        public Player Player { get; protected set; }

        public CraftingComponent Table { get; protected set; }

        public Item Item { get; protected set; }

        public PlayerCraftEvent(Player pPlayer, CraftingComponent pTable, Item pItem) : base()
        {
            this.Player = pPlayer;
            this.Table = pTable;
            this.Item = pItem;
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

    internal class PlayerCraftEventEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, CraftingComponent table, Item item)
        {
            PlayerCraftEvent pcfe = new PlayerCraftEvent(player, table, item);
            IEvent pcfEvent = pcfe;

            EventManager.CallEvent(ref pcfEvent);

            if (!pcfe.IsCancelled())
                return CreateAtomicAction_original(player, table, item);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, CraftingComponent table, Item item)
        {
            throw new InvalidOperationException();
        }
    }
}
