using Asphalt.Api.Event;
using Eco.Gameplay.Items;
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
        public string Text { get; protected set; }

        public WorldObjectChangeTextEvent(ref Player player, string text) : base()
        {
            Player = player;
            Text = text;
        }
    }

    internal class WorldObjectChangeTextEventHelper
    {
        public static bool Prefix(ref Player player, string text)
        {
            WorldObjectChangeTextEvent imie = new WorldObjectChangeTextEvent(ref player, text);
            IEvent imieEvent = imie;

            EventManager.CallEvent(ref imieEvent);

            return true;
        }
    }
}
