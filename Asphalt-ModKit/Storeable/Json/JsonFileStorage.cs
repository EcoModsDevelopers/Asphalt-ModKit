using Asphalt.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorage : IStorage
    {
        public IStorage DefaultStorage { get; protected set; }

        protected Dictionary<string, object> mContent = new Dictionary<string, object>();
        private object path;
        private string v;

        public string FileName { get; protected set; }

        public JsonFileStorage(string pFileName, IStorage pDefaultValues = null)
        {
            FileName = pFileName;
            DefaultStorage = pDefaultValues;

            if(File.Exists(FileName))
                Reload();
        }

        public JsonFileStorage(object path, string v)
        {
            this.path = path;
            this.v = v;
        }

        public virtual void Reload()
        {
            this.mContent = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);
        }

        public virtual void Save()
        {
            if (mContent.Count == 0)
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                return;
            }

            ForceSave();
        }

        public virtual void ForceSave()
        {
            ClassSerializer<Dictionary<string, object>>.Serialize(FileName, this.mContent);
        }

        public virtual string GetString(string key)
        {
            return Get(key)?.ToString();
        }

        public virtual void SetString(string key, string value)
        {
            Set(key, value);
        }

        public virtual int GetInt(string key)
        {
            return Convert.ToInt32(GetString(key));
        }

        public virtual void SetInt(string key, int value)
        {
            Set(key, value);
        }

        public object Get(string key)
        {
            if (mContent.ContainsKey(key))
                return mContent[key];

            if (DefaultStorage == null)
                return null;

            object ret = DefaultStorage.Get(key);
            if (ret == null)
                throw new InvalidOperationException($"DefaultStorage does not contain value with key: {key}");

            return ret;
        }

        public void Set<K>(string key, K value)
        {
            mContent.Remove(key);
            mContent.Add(key, value);
            Save();
        }

        public void Remove(string key)
        {
            mContent.Remove(key);
            Save();
        }

        internal void MergeWithDefaultValues(Dictionary<string, object> pContent)
        {
            foreach (var key in mContent.Keys.ToArray())
            {
                if (!pContent.ContainsKey(key))
                    mContent.Remove(key);
            }

            foreach (var key in pContent.Keys)
            {
                if (!mContent.ContainsKey(key))
                    mContent.Add(key, pContent[key]);
            }
        }
    }
}
