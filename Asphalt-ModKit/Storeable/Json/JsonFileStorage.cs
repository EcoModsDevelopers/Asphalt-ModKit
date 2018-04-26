using Asphalt.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorage : IStorage
    {
        public IStorage DefaultStorage { get; protected set; }

        protected Dictionary<string, object> content = new Dictionary<string, object>();

        public string FileName { get; protected set; }

        public JsonFileStorage(string pFileName, IStorage pDefaultValues = null)
        {
            FileName = pFileName;
            DefaultStorage = pDefaultValues;
            Reload();
        }

        public virtual void Reload()
        {
            this.content = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);
        }

        public virtual void Save()
        {
            if (content.Count == 0)
            {
                File.Delete(FileName);
                return;
            }

            ClassSerializer<Dictionary<string, object>>.Serialize(FileName, this.content);
        }

        public virtual string GetString(string key)
        {
            return Get<string>(key);
        }

        public virtual void SetString(string key, string value)
        {
            Set(key, value);
        }

        public virtual int? GetInt(string key)
        {
            return Get<int?>(key);
        }

        public virtual void SetInt(string key, int value)
        {
            Set(key, value);
        }

        public K Get<K>(string key)
        {
            if (content.ContainsKey(key))
                return (K)content[key];

            if (DefaultStorage == null)
                return default(K);

            K ret = DefaultStorage.Get<K>(key);
            if (ret == null)
                throw new InvalidOperationException($"DefaultStorage does not contain value with key: {key}");

            return ret;
        }

        public void Set<K>(string key, K value)
        {
            content.Remove(key);
            content.Add(key, value);
            Save();
        }

        public void Remove(string key)
        {
            content.Remove(key);
            Save();
        }
    }

    /*
    public class CustomJSONFileContent
    {
        public Dictionary<string, object> Content { get; set; }

        public CustomJSONFileContent()
        {
            this.Content = new Dictionary<string, object>();
        }
    }*/
}
