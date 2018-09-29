using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.CommonFileStorage
{
    public interface IFileStorageSerializer
    {
        Dictionary<string, object> Deserialize(string pText);

        string Serialize(Dictionary<string, object> pObject);

        string GetFileExtension();
    }
}
