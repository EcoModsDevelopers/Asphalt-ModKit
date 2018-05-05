using System;

namespace Asphalt.Storeable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DefaultValuesAttribute : Attribute
    {
        public string MethodName { get; protected set; }

        public DefaultValuesAttribute(string pMethodName)
        {
            MethodName = pMethodName;
        }
    }
}
