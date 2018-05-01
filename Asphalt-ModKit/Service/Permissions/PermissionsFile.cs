using Asphalt.Api.Util;
using Asphalt.Storeable.Json;
using System.Collections.Generic;
using System.Linq;

namespace Asphalt.Service.Permissions
{
    public class PermissionsFile : JsonFileStorage
    {
        public PermissionsFile() : base("permissions.json")
        {

        }

        public Dictionary<string, object> GetPermissions()
        {
            return this.mContent;
        }

        public void SetPermissions(Dictionary<string, PermissionGroup> permissions)
        {
            this.mContent = permissions.ToDictionary(item => item.Key, item => (object)PermissionGroupMethods.GetString(item.Value));
        }
    }
}
