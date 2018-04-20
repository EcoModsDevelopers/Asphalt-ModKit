namespace Asphalt.Service.Config
{
    public class ConfigField
    {
        public string Key { get; private set; }
        public object DefaultValue { get; private set; }

        public ConfigField(string key, object defaultValue)
        {
            this.Key = key;
            this.DefaultValue = defaultValue;
        }
    }
}
