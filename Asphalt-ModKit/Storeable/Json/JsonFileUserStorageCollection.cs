using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Storeable.Json
{
    public class JsonFileUserStorageCollection : JsonFileStorageCollection, IUserStorageCollection
    {
        public JsonFileUserStorageCollection(string dir, Dictionary<string, object> defaultValues = null) : base(dir, defaultValues)
        {

        }

        public IStorage GetStorage(User user)
        {
            //use steam ID if the file is already existant or the user only has a steam ID
            if ((!string.IsNullOrEmpty(user.SteamId) && File.Exists(GetFilePath(user.SteamId))) || (string.IsNullOrEmpty(user.SlgId) && !string.IsNullOrEmpty(user.SteamId)))
                return GetStorage(user.SteamId);

            if (!string.IsNullOrEmpty(user.SlgId))
                return GetStorage(user.SlgId);

            throw new InvalidOperationException($"User {user.Name} does not have an SteamID nor a SlgID. (This should not occur at all!)");
        }
    }
}
