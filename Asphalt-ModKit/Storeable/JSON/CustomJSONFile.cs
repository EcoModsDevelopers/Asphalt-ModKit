using Asphalt.Api.Util;
using System;
using System.Collections.Generic;

namespace Asphalt.Storeable.JSON
{
    public class CustomJSONFile : IStorage
    {
        protected Dictionary<string, object> content;

        public string FileName { get; protected set; }

        public CustomJSONFile(string pFileName)
        {
            FileName = pFileName;
            content = new Dictionary<string, object>();
        }

        public virtual void Reload()
        {
            this.content = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);
        }

        public virtual void Save()
        {
            ClassSerializer<Dictionary<string, object>>.Serialize(FileName, this.content);
        }

        public virtual void Store()
        {
            try
            {
                Save();
                Reload();
            }
            catch (Exception e)
            {
                //TODO: Exception handling
                throw e;
            }
        }

        // Getters and Setters

        public virtual object GetObject(string key)
        {
            if (!this.content.ContainsKey(key))
                return null;

            return this.content[key];
        }

        public virtual void SetObject(string key, object obj)
        {
            if (this.content.ContainsKey(key))
                this.content.Remove(key);

            this.content.Add(key, obj);
        }

        public virtual string GetString(string key)
        {
            return (string)GetObject(key);
        }

        public virtual void SetString(string key, string value)
        {
            SetObject(key, value);
        }

        public virtual int GetInt(string key)
        {
            return Convert.ToInt32(GetObject(key));
        }

        public virtual void SetInt(string key, int value)
        {
            SetObject(key, value);
        }

        public K Get<K>(string key)
        {
            throw new NotImplementedException();
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
