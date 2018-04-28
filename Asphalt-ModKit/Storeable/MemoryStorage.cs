using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable
{
    public class MemoryStorage : IStorage
    {
        private IDictionary<string, object> mValues;

        public MemoryStorage(IDictionary<string, object> pValues)
        {
            mValues = pValues;
        }

        public K Get<K>(string key)
        {
            return (K)mValues[key];
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

        public void Reload()
        {
            throw new NotSupportedException();
        }

        public void Remove(string key)
        {
            mValues.Remove(key);
        }

        public void Save()
        {
            throw new NotSupportedException();
        }

        public void Set<K>(string key, K value)
        {
            mValues.Remove(key);
            mValues.Add(key, value);
        }
    }
}
