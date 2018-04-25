using Asphalt.Api.Util;
using Asphalt.Storeable.JSON;
using System.Collections.Generic;
using System.Linq;

namespace Asphalt.Service.Permissions
{
    public class PermissionsFile : CustomJSONFile
    {
        public PermissionsFile() : base("permissions.json")
        {

        }

        public Dictionary<string, object> GetPermissions()
        {
            return this.content;
        }

        public void SetPermissions(Dictionary<string, PermissionGroup> permissions)
        {
            this.content = permissions.ToDictionary(item => item.Key, item => (object)PermissionGroupMethods.GetString(item.Value));
        }
    }
}
