using Asphalt.Api.Util;
using System;
using System.Collections.Generic;

namespace Asphalt.Storeable.JSON
{
    public abstract class CustomJSONFile
    {
        protected CustomJSONFileContent content;

        public CustomJSONFile()
        {
        }

        public abstract string GetFilepath();

        public abstract string GetFilename();

        public virtual void Reload()
        {
            this.content = ClassSerializer<CustomJSONFileContent>.Deserialize(GetFilepath(), GetFilename());
        }

        public virtual void Save()
        {
            ClassSerializer<CustomJSONFileContent>.Serialize(GetFilepath(), GetFilename(), this.content);
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
            if (!this.content.Content.ContainsKey(key))
                return null;

            return this.content.Content[key];
        }

        public virtual void SetObject(string key, object obj)
        {
            if (this.content.Content.ContainsKey(key))
                this.content.Content.Remove(key);

            this.content.Content.Add(key, obj);
        }

        public virtual string GetString(string key)
        {
            return (string) GetObject(key);
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
    }

    public class CustomJSONFileContent
    {
        public Dictionary<string, object> Content { get; set; }

        public CustomJSONFileContent()
        {
            this.Content = new Dictionary<string, object>();
        }
    }
}
