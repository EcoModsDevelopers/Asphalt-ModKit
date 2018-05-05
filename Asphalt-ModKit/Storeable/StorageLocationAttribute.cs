using System;

namespace Asphalt.Storeable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StorageLocationAttribute : Attribute
    {
        public string Location { get; protected set; }

        public StorageLocationAttribute(string pStorageLocation)
        {
            Location = pStorageLocation;
        }
    }
}
