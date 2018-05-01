using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
