namespace Asphalt.Storeable
{
    public interface IStorage
    {
        int GetInt(string key);
        string GetString(string key);
        void Save();
        void SetInt(string key, int value);
        void SetString(string key, string value);
        K Get<K>(string key);
    }

    public interface IStorage<T>
    {
        int GetInt(T key);
        string GetString(T key);
        void Save();
        void SetInt(T key, int value);
        void SetString(T key, string value);
        K Get<K>(T key);
    }
}