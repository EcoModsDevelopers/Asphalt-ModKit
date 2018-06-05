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

        public PlayerEatEvent(ref Player pPlayer, ref FoodItem pFoodIten, ref Stomach pStomach) : base()
        {
            this.Player = pPlayer;
            this.FoodItem = pFoodIten;
            this.Stomach = pStomach;
        }
    }

    internal class PlayerEatEventHelper
    {
        public static bool Prefix(ref Player player, ref FoodItem food, ref Stomach __instance)
        {
            PlayerEatEvent cEvent = new PlayerEatEvent(ref player, ref food, ref __instance);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            return !cEvent.IsCancelled();
        }
    }
}
