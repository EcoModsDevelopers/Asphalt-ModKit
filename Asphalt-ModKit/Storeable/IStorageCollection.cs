using Asphalt.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
