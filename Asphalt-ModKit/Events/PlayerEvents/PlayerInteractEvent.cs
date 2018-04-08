using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using Eco.Shared.Utils;
using Eco.Shared.Voxel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player interacts with something
    /// </summary>
    public class PlayerInteractEvent : ICancellable, IEvent
    {
        private bool cancel = false;

        public InteractionContext Context { get; protected set; }

        public PlayerInteractEvent(InteractionContext pContext) : base()
        {
            this.Context = pContext;
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

            }

            return context;
        }

        public static InteractionContext MakeContext_original(this InteractionInfo info, PlayerHandle player)
        {
            throw new InvalidOperationException();
        }
    }
}
