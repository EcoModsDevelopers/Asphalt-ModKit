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

    [RequireComponent(typeof(CustomTextComponent))]
    public partial class WoodSignObject : WorldObject
    {
        public override InteractResult OnActRight(InteractionContext context)
        {
            Eco.Mods.Kronox.AdvancedTeleportation.CallWarpSign(context.Player, GetComponent<CustomTextComponent>().Text);

            Asphalt.api.AsphaltPlugin.Instance.GetEventService().CallEvent(new PlayerInteractObjectEvent(context));

            return base.OnActRight(context);
        }
    }

    [Serialized]
    public partial class SmallWoodSignObject : WoodSignObject
    { }
}