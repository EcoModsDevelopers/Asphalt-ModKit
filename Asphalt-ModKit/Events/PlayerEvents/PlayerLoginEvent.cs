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
        public Player Player { get; protected set; }

        public PlayerLoginEvent(Player pPlayer) : base()
        {
            this.Player = pPlayer;
        }
    }

    internal class PlayerLoginEventHelper
    {
        public void Login(Player player, INetClient client)
        {
            PlayerLoginEvent pje = new PlayerLoginEvent(player);
            IEvent pjeEvent = pje;

            EventManager.CallEvent(ref pjeEvent);

            Login_original(player, client);
        }

        public void Login_original(Player player, INetClient client)
        {
            throw new InvalidOperationException();
        }
    }
}
