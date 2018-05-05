using System;

namespace Asphalt.Storeable
{
    public class StorageName : Attribute
    {
        public string Name { get; protected set; }

        public StorageName(string pStorageName)
        {
            Name = pStorageName;
        }
    }
}
