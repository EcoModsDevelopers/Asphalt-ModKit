using Asphalt.Storeable.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable
{
    public class JsonStorageCollection : IStorageCollection
    {
        private static IDictionary<string, IStorage> mStorageCache = new Dictionary<string, IStorage>();

        private static IStorage mDefaultStorage;

        public IStorage GetDefaultStorage()
        {
            if (mDefaultStorage == null)
                mDefaultStorage = new CustomJSONFile("_default.json");
            return mDefaultStorage;
        }

        public IStorage GetStorage(string pStorageName)
        {
            if (!mStorageCache.ContainsKey(pStorageName))
            {
                CustomJSONFile file = new CustomJSONFile(pStorageName + ".json");
                //init file
                mStorageCache.Add(pStorageName, file);
                return file;
            }

            return mStorageCache[pStorageName];
        }
    }
}
