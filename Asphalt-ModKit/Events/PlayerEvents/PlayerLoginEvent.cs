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
        public void Login(Player player, INetClient client)
        {
            PlayerLoginEvent pje = new PlayerLoginEvent(player, client);
            IEvent pjeEvent = pje;

            EventManager.CallEvent(ref pjeEvent);

            Login_original(pje.Player, pje.Client);
        }

        public void Login_original(Player player, INetClient client)
        {
            throw new InvalidOperationException();
        }
    }
}
