using Eco.Gameplay.Players;
using Eco.Shared.Networking;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    ///  Called when the loading screen of a user appears
    /// </summary>
    public class PlayerLoginEvent : IEvent
    {
        public Player Player { get; set; }

        public INetClient Client { get; set; }

        public PlayerLoginEvent(Player pPlayer, INetClient pClient) : base()
        {
            this.Player = pPlayer;
            this.Client = pClient;
        }
    }

    internal class PlayerLoginEventHelper
    {
        public static void Prefix(Player player, INetClient client)
        {
            PlayerLoginEvent cEvent = new PlayerLoginEvent(player, client);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }
    }
}
