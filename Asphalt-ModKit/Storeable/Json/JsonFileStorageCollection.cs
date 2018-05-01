using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorageCollection : IStorageCollection
    {
        private static IDictionary<string, IStorage> mStorageCache = new Dictionary<string, IStorage>();

        private static IStorage mDefaultStorage;

        public IStorage GetDefaultStorage()
        {
            if (mDefaultStorage == null)
                mDefaultStorage = new JsonFileStorage("_default.json");
            return mDefaultStorage;
        }

        public IStorage GetStorage(string pStorageName)
        {
            if (!mStorageCache.ContainsKey(pStorageName))
            {
                JsonFileStorage file = new JsonFileStorage(pStorageName + ".json", GetDefaultStorage());
                //init file
                mStorageCache.Add(pStorageName, file);
                return file;
            }

            return mStorageCache[pStorageName];
        }

        public void Reload()
        {
            mStorageCache.Clear();
        }
    }
}
