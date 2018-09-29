using System.Collections.Generic;
using System.IO;

namespace Asphalt.Storeable.CommonFileStorage
{
    public class FileStorageCollection : IStorageCollection
    {
        protected string mDirectory;

        protected IDictionary<string, IStorage> mStorageCache = new Dictionary<string, IStorage>();
        protected IStorage mDefaultStorage;
        protected Dictionary<string, object> mDefaultValues;
        protected IFileStorageSerializer mSerializer;

        public FileStorageCollection(IFileStorageSerializer pSerializer, string pDirectory, Dictionary<string, object> pDefaultValues = null)
        {
            mDirectory = pDirectory;
            mDefaultValues = pDefaultValues;
            mSerializer = pSerializer;

            //create DefaultFile
            if (mDefaultStorage == null && pDefaultValues != null)
                mDefaultStorage = new FileStorage(mSerializer, GetFilePath("_default"), pDefaultValues, true);
        }

        public virtual IStorage GetDefaultStorage()
        {
            return mDefaultStorage;
        }

        public virtual IStorage GetStorage(string pStorageName)
        {
            if (!mStorageCache.ContainsKey(pStorageName))
            {
                FileStorage file = new FileStorage(mSerializer, GetFilePath(pStorageName), GetDefaultStorage()?.GetContent());
                mStorageCache.Add(pStorageName, file);
                return file;
            }

            return mStorageCache[pStorageName];
        }

        public virtual void Reload()
        {
            mStorageCache.Clear();
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(mDirectory, fileName) + mSerializer.GetFileExtension();
        }
    }
}
