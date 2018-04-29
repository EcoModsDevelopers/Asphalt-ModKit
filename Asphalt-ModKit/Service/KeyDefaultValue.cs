using System;

namespace Asphalt.Service
{
    public class KeyDefaultValue
    {
        public string Key { get; private set; }
        public string DefaultValue { get; private set; }

        public KeyDefaultValue(string key, string defaultValue)
        {
            this.Key = key;
            this.DefaultValue = defaultValue;
        }

        /*   
           public static implicit operator KeyDefaultValue<T>(Tuple<string, T> pKeyAndDefaultValue)
           {
               return new KeyDefaultValue<T>(pKeyAndDefaultValue.Item1, pKeyAndDefaultValue.Item2);
           } */
    }
}
