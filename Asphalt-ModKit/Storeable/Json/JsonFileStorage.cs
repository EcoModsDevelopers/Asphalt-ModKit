using Asphalt.Api.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorage : IStorage
    {
        public Dictionary<string, object> DefaultValues { get; protected set; }
        protected bool saveDefaultValues;

        public Dictionary<string, object> Content { get; protected set; }
        private object path;
        private string v;

        public string FileName { get; protected set; }

        public JsonFileStorage(string pFileName, Dictionary<string, object> pDefaultValues = null, bool pSaveDefaultValues = false)
        {
            Content = new Dictionary<string, object>();

            FileName = pFileName;
            DefaultValues = pDefaultValues;
            saveDefaultValues = pSaveDefaultValues;

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
            if (File.Exists(FileName))
                this.Content = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);

            if (saveDefaultValues && DefaultValues != null)
            {
                Content = MergeWithDefaultValues(Content, DefaultValues);
                //save the file even if it's empty to show that there are no default values
                ForceSave();
            }
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

            //if saveDefaultValues is set to true the defaultvalues are already contained in the Content
            if (saveDefaultValues || DefaultValues == null)
                return null;

            if (!DefaultValues.ContainsKey(key))
                throw new InvalidOperationException($"DefaultValues do not contain value with key: {key}");

            return DefaultValues[key];
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

            //if it's a default value and saveDefaultValues is set to true just reset the value
            if (saveDefaultValues && DefaultValues.ContainsKey(key))
                Content.Add(key, DefaultValues[key]);

            Save();
        }

        protected Dictionary<string, object> MergeWithDefaultValues(Dictionary<string, object> pContent, Dictionary<string, object> pDefaultValues)
        {
            Dictionary<string, object> tmpDic = pContent;

            foreach (var key in tmpDic.Keys.ToArray())
            {
                if (!pDefaultValues.ContainsKey(key))
                    tmpDic.Remove(key);
            }

            foreach (var key in pDefaultValues.Keys)
            {
                if (!tmpDic.ContainsKey(key))
                    tmpDic.Add(key, pDefaultValues[key]);
            }

            return tmpDic;
        }

        public Dictionary<string, object> GetContent()
        {
            return Content;
        }
    }
}
