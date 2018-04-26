using Asphalt.Service;

namespace Asphalt.Storeable
{
    public interface IStorage : IReloadable
    {
        int? GetInt(string key);
        string GetString(string key);
        void Save();
        void SetInt(string key, int value);
        void SetString(string key, string value);
        K Get<K>(string key);
        void Set<K>(string key, K value);
        void Remove(string key);
    }
}