using System;

namespace Asphalt
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AsphaltPluginAttribute : Attribute
    {
        public string ModName { get; set; }

        public AsphaltPluginAttribute(string name = null)
        {
            ModName = name;
        }
    }
}
