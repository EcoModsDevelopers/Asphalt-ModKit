using Asphalt.Events;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Players;
using Eco.Shared.Items;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player interacts with something
    /// </summary>
    public class PlayerInteractEvent : CancellableEvent
    {
        public InteractionContext Context { get; set; }

        public PlayerInteractEvent(InteractionContext pContext) : base()
        {
            this.Context = pContext;
        }
    }

    internal static class PlayerInteractEventHelper
    {
        /*   var context = info.MakeContext(this);
            if (!context.Authed() && (context.SelectedItem == null || !context.SelectedItem.IgnoreAuth))
                return;                
         */

        public static InteractionContext MakeContext(this InteractionInfo info, PlayerHandle player)
        {
            InteractionContext context = MakeContext_original(info, player);

            PlayerInteractEvent playerInteractEvent = new PlayerInteractEvent(context);
            IEvent playerInteractIEvent = playerInteractEvent;

            EventManager.CallEvent(ref playerInteractIEvent);

            if (playerInteractEvent.IsCancelled())
            {
                //we can not really cancel the event, but we remove all targets ;)

                //context.Target, context.SelectedItem, context.InteractableBlock, context.CarriedItem                    

                context.Target = null;
                context.SelectedItem = null;
                context.Block = null;  // InteractableBlock
                context.CarriedItem = null;

                if (info.BlockPosition.HasValue)
                    context.Player.SendCorrection(info);

                //remove exp because eco will add it
                context.Player.User.XP -= DifficultySettings.Obj.Config.SkillPointsPerAction * (context.Player.User.SkillRate / DifficultySettings.BaselineSkillpoints);

                //Unwanted side effect that we can't change:
                //  var activity = WorldLayerManager.GetLayer(LayerNames.PlayerActivity)?.FuncAtWorldPos(this.Position.XZi, (pos, val) => val = System.Math.Min(1, val + 0.001f));

                return context;
            }

            return playerInteractEvent.Context;
        }

        public static InteractionContext MakeContext_original(this InteractionInfo info, PlayerHandle player)
        {
            throw new InvalidOperationException();
        }
    }
}
