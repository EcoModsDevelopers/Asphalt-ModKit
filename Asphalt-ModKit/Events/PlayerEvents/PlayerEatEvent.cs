using Asphalt.Events;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerEatEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public FoodItem FoodItem { get; set; }

        public Stomach Stomach { get; protected set; }  //protected because we can't change it

        public PlayerEatEvent(Player pPlayer, FoodItem pFoodIten, Stomach pStomach) : base()
        {
            this.Player = pPlayer;
            this.FoodItem = pFoodIten;
            this.Stomach = pStomach;
        }
    }

    internal class PlayerEatEventHelper
    {
        public bool Eat(Player player, FoodItem food)
        {
            PlayerEatEvent cEvent = new PlayerEatEvent(player, food, (Stomach)((object)this));
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return Eat_original(cEvent.Player, cEvent.FoodItem);

            return false;
        }

        public bool Eat_original(Player player, FoodItem food)
        {
            throw new InvalidOperationException();
        }
    }
}
