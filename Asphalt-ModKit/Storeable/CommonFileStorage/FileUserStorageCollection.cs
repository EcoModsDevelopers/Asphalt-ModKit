using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Storeable.CommonFileStorage
{
    public class FileUserStorageCollection : FileStorageCollection, IUserStorageCollection
    {
        public FileUserStorageCollection(IFileStorageSerializer pSerializer, string pDirectory, Dictionary<string, object> pDefaultValues = null) : base(pSerializer, pDirectory, pDefaultValues)
        {
        }

        public IStorage GetStorage(User pUser)
        {
            //use steam ID if the file is already existant or the user only has a steam ID
            if ((!string.IsNullOrEmpty(pUser.SteamId) && File.Exists(GetFilePath(pUser.SteamId))) || (string.IsNullOrEmpty(pUser.SlgId) && !string.IsNullOrEmpty(pUser.SteamId)))
                return GetStorage(pUser.SteamId);

            if (!string.IsNullOrEmpty(pUser.SlgId))
                return GetStorage(pUser.SlgId);

            throw new InvalidOperationException($"User {pUser.Name} does not have an SteamID nor a SlgID. (This should not occur at all!)");
        }
    }
}
