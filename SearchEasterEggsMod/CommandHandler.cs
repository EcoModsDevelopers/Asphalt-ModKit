using Asphalt.Storeable;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEasterEggsMod
{
    public class CommandHandler : IChatCommandHandler
    {
        [ChatCommand("Create Easter Egg")]
        public static void CreateEasterEgg(User user)
        {
            if (!SearchEasterEggsMod.PermissionChecker.CheckPermission(user, "easteregg.create"))  //CheckPermission will notify the user if he doesn't have permission
                return;

            user.Player.SendTemporaryMessage($"Easter Egg created!");
        }

        [ChatCommand("Collect Easter Egg")]
        public static void CollectEasterEgg(User user)
        {
            if (!SearchEasterEggsMod.PermissionChecker.CheckPermission(user, "easteregg.collect"))  //CheckPermission will notify the user if he doesn't have permission
                return;

            IStorage userStorage = SearchEasterEggsMod.CollectedEggsStorage.GetStorage(user);

            int collectedEggs = userStorage.GetInt("collectedEggs"); //if no value was stored before, this will return 0

            collectedEggs++;

            userStorage.Set("collectedEggs", collectedEggs);

            user.Player.SendTemporaryMessage($"easter egg collected!");
            user.Player.SendTemporaryMessage($"You have collected {collectedEggs} eggs");
        }
    }
}
