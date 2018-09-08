using Asphalt.Storeable.CommonFileStorage;
using Asphalt.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Yaml
{
    public class YamlFileStorageSerializer : IFileStorageSerializer
    {
        public Dictionary<string, object> Deserialize(string pText)
        {
            return YamlSerializationHelper.Serializer.Deserialize<Dictionary<string, object>>(pText);
        }

        public string GetFileExtension()
        {
            return ".yaml";
        }

        public string Serialize(Dictionary<string, object> pObject)
        {
            return YamlSerializationHelper.Serializer.Serialize(pObject);
        }
    }
}
