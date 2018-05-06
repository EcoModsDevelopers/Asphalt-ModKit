using System;
using System.IO;

namespace Asphalt.Storeable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StorageLocationAttribute : Attribute
    {
        public string Location { get; protected set; }

        public StorageLocationAttribute(string pStorageLocation)
        {
            if (!string.IsNullOrEmpty(Path.GetExtension(pStorageLocation)))
                throw new ArgumentException($"{nameof(pStorageLocation)} should never be a filename with an extension. Please provide only the Location of an Storage (e.g. the filename without extension)");

            Location = pStorageLocation;
        }
    }
}
