// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Interactions;
    using Eco.Shared.Serialization;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Asphalt.api.Event.player;
    using System;

    [RequireComponent(typeof(CustomTextComponent))]
    public partial class WoodSignObject : WorldObject
    {
        public override InteractResult OnActRight(InteractionContext context)
        {
            PlayerInteractObjectEvent _event = new PlayerInteractObjectEvent(context);
            Asphalt.api.AsphaltPlugin.Instance.GetEventService().CallEvent(_event);
            if (_event.IsCancelled()) return base.OnActRight(context);

            return base.OnActRight(context);
        }
    }

    [Serialized]
    public partial class SmallWoodSignObject : WoodSignObject
    { }
}