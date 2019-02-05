using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Util
{
    public static class PlayerExtensions
    {
        public static void SendTemporaryMessage(this Player pPlayer, string message, ChatCategory category = ChatCategory.Info)
        {
            pPlayer.SendTemporaryMessage(new LocString(message), category);
        }

        public static void SendTemporaryError(this Player pPlayer, string message, ChatCategory category = ChatCategory.Info)
        {
            pPlayer.SendTemporaryError(new LocString(message), category);
        }

        public static void SendMessage(this User pUser, string pMessage, bool temporary = true, DefaultChatTags tag = DefaultChatTags.Notifications, ChatCategory category = ChatCategory.Info)
        {
            ChatManager.ServerMessageToPlayer(new LocString(pMessage), pUser, temporary, tag, category);
        }
    }
}
