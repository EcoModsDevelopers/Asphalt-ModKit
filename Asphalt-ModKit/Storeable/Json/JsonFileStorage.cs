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

        public Dictionary<string, object> Content { get; protected set; }
        private object path;
        private string v;

        public string FileName { get; protected set; }

        public JsonFileStorage(string pFileName, IStorage pDefaultValues = null)
        {
            Content = new Dictionary<string, object>();

            FileName = pFileName;
            DefaultStorage = pDefaultValues;

            if(File.Exists(FileName))
                Reload();
        }

        public JsonFileStorage(object path, string v)
        {
            Content = new Dictionary<string, object>();

            this.path = path;
            this.v = v;
        }

        public virtual void Reload()
        {
            this.Content = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);
        }

        public virtual void Save()
        {
            if (Content.Count == 0)
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                return;
            }

            ForceSave();
        }

        public virtual void ForceSave()
        {
            ClassSerializer<Dictionary<string, object>>.Serialize(FileName, this.Content);
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
            if (Content.ContainsKey(key))
                return Content[key];

            if (DefaultStorage == null)
                return null;

            object ret = DefaultStorage.Get(key);
            if (ret == null)
                throw new InvalidOperationException($"DefaultStorage does not contain value with key: {key}");

            return ret;
        }

        public void Set<K>(string key, K value)
        {
            Content.Remove(key);
            Content.Add(key, value);
            Save();
        }

        public void Remove(string key)
        {
            Content.Remove(key);
            Save();
        }

        internal void MergeWithDefaultValues(Dictionary<string, object> pContent)
        {
            foreach (var key in Content.Keys.ToArray())
            {
                if (!pContent.ContainsKey(key))
                    Content.Remove(key);
            }

            foreach (var key in pContent.Keys)
            {
                if (!Content.ContainsKey(key))
                    Content.Add(key, pContent[key]);
            }
        }
    }
}
