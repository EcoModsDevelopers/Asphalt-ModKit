using Asphalt.Service;

namespace Asphalt.Storeable
{
    /// <summary>
    /// Collection of IStorage.
    /// Contains a IStorage as default file
    /// </summary>
    public interface IStorageCollection : IReloadable
    {
        IStorage GetDefaultStorage();

        IStorage GetStorage(string pStorageName);
    }
}
