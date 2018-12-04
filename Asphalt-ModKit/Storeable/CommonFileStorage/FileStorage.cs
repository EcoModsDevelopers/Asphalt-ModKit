using Asphalt.Api.Util;
using Eco.Core.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Asphalt.Storeable.CommonFileStorage
{
    public class FileStorage : IStorage
    {
        public IDictionary<string, object> DefaultValues { get; protected set; }

        public Dictionary<string, object> Content { get; protected set; }

        public string FileName { get; protected set; }

        protected bool mSaveDefaultValues;

        protected IFileStorageSerializer mSerializer;

        public FileStorage(IFileStorageSerializer pSerializer, string pFileName, IDictionary<string, object> pDefaultValues = null, bool pSaveDefaultValues = false)
        {
            Content = new Dictionary<string, object>();

            FileName = pFileName;
            mSerializer = pSerializer;

            if (string.IsNullOrEmpty(Path.GetExtension(FileName)))
                FileName += mSerializer.GetFileExtension();

            DefaultValues = pDefaultValues;
            mSaveDefaultValues = pSaveDefaultValues;

            Reload();
        }

        public virtual void Reload()
        {
            if (File.Exists(FileName))
                this.Content = mSerializer.Deserialize(FileUtil.ReadFromFile(FileName)) ?? new Dictionary<string, object>();

            if (mSaveDefaultValues && DefaultValues != null)
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
            FileUtil.WriteToFile(FileName, mSerializer.Serialize(this.Content));
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
            if (mSaveDefaultValues || DefaultValues == null)
                return null;

            if (!DefaultValues.ContainsKey(key))
                throw new InvalidOperationException($"DefaultValues do not contain value with key: {key}");

            return DefaultValues[key];
        }

        public T Get<T>(string key)
        {
            try
            {
                var obj = Get(key);

                if (obj == null)
                    return default(T);

                if (obj is T)
                    return (T)Get(key);

                return SerializationUtils.DeserializeJson<T>(Get(key).ToString());
            }
            catch (Exception e)
            {
                Log.WriteError(e.ToString());
                throw;
            }
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
            if (mSaveDefaultValues && DefaultValues != null && DefaultValues.ContainsKey(key))
                Content.Add(key, DefaultValues[key]);

            Save();
        }

        protected Dictionary<string, object> MergeWithDefaultValues(Dictionary<string, object> pContent, IDictionary<string, object> pDefaultValues)
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

        public IDictionary<string, object> GetContent()
        {
            return Content;
        }
    }
}
