using Asphalt.Events;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;

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
        public static bool Prefix(Player player, FoodItem food, Stomach __instance)
        {
            PlayerEatEvent cEvent = new PlayerEatEvent(player, food, __instance);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            return !cEvent.IsCancelled();
        }
    }
}
