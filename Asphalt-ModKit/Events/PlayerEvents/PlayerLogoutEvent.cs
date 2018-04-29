using Eco.Gameplay.Players;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    ///  Called when the user logs out
    /// </summary>
    public class PlayerLogoutEvent : IEvent
    {
        public User User { get; protected set; }  //protected because we can't change it

        public PlayerLogoutEvent(User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerLogoutEventHelper
    {
        public static void Prefix(User __instance)
        {
            PlayerLogoutEvent cEvent = new PlayerLogoutEvent(__instance);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);
        }
    }
}
