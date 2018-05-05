using Eco.Gameplay.Players;

namespace Asphalt.Storeable
{
    public interface IUserStorageCollection : IStorageCollection
    {
        IStorage GetUserStorage(User user);
    }
}
