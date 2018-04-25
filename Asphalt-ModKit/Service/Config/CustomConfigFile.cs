using Asphalt.Api.Util;
using Asphalt.Storeable.JSON;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Service.Config
{
    public class CustomConfigFile : CustomJSONFile
    {
        public CustomConfigFile(string pFilePath) : base(Path.Combine(pFilePath, "config.json"))
        {
        }

        public Dictionary<string, object> GetValues()
        {
            return this.content;
        }

        public void SetValues(Dictionary<string, object> values)
        {
            this.content = values;
        }
    }
}
