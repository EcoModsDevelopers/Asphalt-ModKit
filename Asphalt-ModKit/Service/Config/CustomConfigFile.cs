using Asphalt.Api.Util;
using Asphalt.Storeable.JSON;
using System.Collections.Generic;

namespace Asphalt.Service.Config
{
    public class CustomConfigFile : CustomJSONFile
    {
        private AsphaltMod mod;

        public CustomConfigFile(AsphaltMod mod)
        {
            this.mod = mod;
        }

        public override string GetFilename()
        {
            return "config.json";
        }

        public override string GetFilepath()
        {
            return FileUtil.GetModFolder(mod);
        }

        public Dictionary<string, object> GetValues()
        {
            return this.content.Content;
        }

        public void SetValues(Dictionary<string, object> values)
        {
            this.content.Content = values;
        }
    }
}
