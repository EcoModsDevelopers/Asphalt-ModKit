using SharpYaml.Serialization;
using SharpYaml.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Yaml
{
    internal class YamlSerializationHelper
    {

        public static Serializer Serializer = new Serializer(new SerializerSettings()
        {
            EmitShortTypeName = true,
            EmitAlias = false,
        });

        static YamlSerializationHelper()
        {
            PrimitiveSerializer.RoundFloatingPointValues = true;
        }
    }
}
