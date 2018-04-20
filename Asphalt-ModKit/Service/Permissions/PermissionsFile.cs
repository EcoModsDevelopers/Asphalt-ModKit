using Asphalt.Api.Util;
using Asphalt.Storeable.JSON;
using System.Collections.Generic;
using System.Linq;

namespace Asphalt.Service.Permissions
{
    public class PermissionsFile : CustomJSONFile
    {
        private AsphaltMod mod;

        public PermissionsFile(AsphaltMod mod)
        {
            this.mod = mod;
        }

        public override string GetFilename()
        {
            return "permissions.json";
        }

        public override string GetFilepath()
        {
            return FileUtil.GetModFolder(mod);
        }

        public Dictionary<string, object> GetPermissions()
        {
            return this.content.Content;
        }

        public void SetPermissions(Dictionary<string, PermissionGroup> permissions)
        {
            this.content.Content = permissions.ToDictionary(item => item.Key, item => (object) PermissionGroupMethods.GetString(item.Value));
        }
    }
}
