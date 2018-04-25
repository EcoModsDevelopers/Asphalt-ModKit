using Asphalt.Api.Util;
using Asphalt.Storeable.JSON;
using Eco.Core.Plugins.Interfaces;
using System.Collections.Generic;

namespace Asphalt.Service.Settings
{/*
    public abstract class CustomSettingsFile : CustomJSONFile
    {
        private AsphaltMod mod;
        private string fileName;

        public CustomSettingsFile(AsphaltMod mod, string fileName)
        {
            this.mod = mod;
            this.fileName = fileName;
        }

        public CustomSettingsFile(AsphaltMod mod)
        {
            this.mod = mod;
            this.fileName = GetFilename();
        }

        public override void Reload()
        {
            base.Reload();

            if (this.content == null)
                this.content = ClassSerializer<Dictionary<string, object>>.Deserialize(GetDefaultFilename());
        }
        /*
        public override string GetFilepath()
        {
            return FileUtil.GetModFolder(mod) + SettingsService.SETTINGS_DIR + "/" + GetSettingsName() + "/";
        }*/

   //     public abstract string GetSettingsName();

        /*
        public override string GetFilename()
        {
            if (!(this.fileName == null || this.fileName.Equals("")))
                return this.fileName;

            return GetDefaultFilename();
        }*/
/*
        private string GetDefaultFilename()
        {
            return "_default.json";
        }
    }*/
}
