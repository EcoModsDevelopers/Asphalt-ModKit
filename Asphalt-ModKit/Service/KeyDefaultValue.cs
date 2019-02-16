namespace Asphalt.Service
{
    public class KeyDefaultValue
    {
        public string Key { get; private set; }
        public object DefaultValue { get; private set; }

        public KeyDefaultValue(string key, object defaultValue)
        {
            this.Key = key;
            this.DefaultValue = defaultValue;
        }
    }
}
