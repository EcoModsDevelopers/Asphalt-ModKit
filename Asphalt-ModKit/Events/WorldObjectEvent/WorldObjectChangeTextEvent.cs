using Asphalt.Api.Event;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Reflection;

namespace Asphalt.Events.WorldObjectEvent
{
    /// <summary>
    /// Called when an SignText gets set
    /// </summary>
    public class WorldObjectChangeTextEvent : IEvent
    {
        public Player Player { get; protected set; }
        public WorldObject WorldObject { get; protected set; }
        public string Text { get; protected set; }

        public WorldObjectChangeTextEvent(Player player, WorldObject obj, string text) : base()
        {
            Player = player;
            Text = text;
        }
    }

    internal class WorldObjectChangeTextEventHelper
    {
        public static bool Prefix(ref Player player, string text, ref CustomTextComponent __instance)
        {
            WorldObjectChangeTextEvent imie = new WorldObjectChangeTextEvent(player, __instance.Parent, text);
            IEvent imieEvent = imie;

            EventManager.CallEvent(ref imieEvent);

            return true;
        }
    }
}
