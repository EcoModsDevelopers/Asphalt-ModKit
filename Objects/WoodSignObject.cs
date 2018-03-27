// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using Eco.Gameplay.Objects;
using Eco.Gameplay.Interactions;
using Eco.Shared.Serialization;
using Eco.Gameplay.Components;
using Asphalt.api.Event.player;
using Asphalt.api.Event;

namespace Eco.Mods.TechTree
{
    [RequireComponent(typeof(CustomTextComponent))]
    public partial class WoodSignObject : WorldObject
    {
        public override InteractResult OnActRight(InteractionContext context)
        {
            PlayerInteractObjectEvent _event = new PlayerInteractObjectEvent(context);
            EventManager.Instance.CallEvent(_event);
            if (_event.IsCancelled()) return base.OnActRight(context);

            return base.OnActRight(context);
        }
    }

    [Serialized]
    public partial class SmallWoodSignObject : WoodSignObject
    { }
}