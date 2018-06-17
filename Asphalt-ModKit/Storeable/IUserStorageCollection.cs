using Eco.Gameplay.Players;
using System;

namespace Asphalt.Storeable
{
    public interface IUserStorageCollection : IStorageCollection
    {
        IStorage GetStorage(User pUser);

        [Obsolete("Please use only the GetStorage(User) method if you are using IUserStorageCollection")]
        new IStorage GetStorage(string pStorageName);
    }
}
