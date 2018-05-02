using Asphalt.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorageCollection : IStorageCollection
    {
        private string dir;

        private static IDictionary<string, IStorage> mStorageCache = new Dictionary<string, IStorage>();
        private static IStorage mDefaultStorage;

        public JsonFileStorageCollection(string dir)
        {
            this.dir = dir;
        }

        public IStorage GetDefaultStorage()
        {
            if (mDefaultStorage == null)
            {
                if (!File.Exists(GetFilePath("_default")))
                    throw new FileNotFoundException($"Default settings file '{GetFilePath("_default")}' not found!");

                mDefaultStorage = new JsonFileStorage(GetFilePath("_default"));
            }
            return mDefaultStorage;
        }

        public IStorage GetStorage(string pStorageName)
        {
            if (!mStorageCache.ContainsKey(pStorageName))
            {
                JsonFileStorage file = new JsonFileStorage(GetFilePath(pStorageName), GetDefaultStorage());
                mStorageCache.Add(pStorageName, file);
                return file;
            }

            return mStorageCache[pStorageName];
        }

        public void Reload()
        {
            mStorageCache.Clear();
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(dir, fileName+".json");
        }
    }
}
