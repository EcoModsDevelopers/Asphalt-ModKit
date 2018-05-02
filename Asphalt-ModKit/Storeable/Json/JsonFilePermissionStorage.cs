using Asphalt.Api.Util;
using Asphalt.Service.Permissions;
using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Json
{
    public class JsonFilePermissionStorage : JsonFileStorage, IPermissionService
    {
        public JsonFilePermissionStorage(string pFileName, IStorage pDefaultValues = null) : base(pFileName, pDefaultValues)
        {
        }

        public override void Reload()
        {
            Dictionary<string, object> tmpContent = ClassSerializer<Dictionary<string, object>>.Deserialize(FileName);

            this.mContent = tmpContent.ToDictionary(k => k.Key, v => Enum.Parse(typeof(PermissionGroup), (string) v.Value));

            this.mContent.Clear();

            foreach(KeyValuePair<string, object> pair in tmpContent)
            {
                PermissionGroup value;
                try
                {
                    value = (PermissionGroup) Enum.Parse(typeof(PermissionGroup), (string)pair.Value);
                }
                catch
                {
                    throw new Exception($"Value '{pair.Value}' of permission '{pair.Key}' is not valid");
                }
                this.mContent.Add(pair.Key, value);
            }
        }

        public override void ForceSave()
        {
            ClassSerializer<Dictionary<string, object>>.Serialize(FileName, this.mContent.ToDictionary(k => k.Key, v => (object) v.Value.ToString()));
        }

        public bool CheckPermission(User user, string permission)
        {
            if (HasPermission(user, permission))
                return true;

            user.Player.SendTemporaryErrorAlreadyLocalized($"You don't have the permission to do this! <color=#595959>Permission needed: {permission}</color>");
            return false;
        }

        public bool HasPermission(User user, string permission)
        {
            object obj = Get(permission);

            PermissionGroup? permissionGroup = obj as PermissionGroup?;

            if (permissionGroup == null)
                return false;

            switch (permissionGroup)
            {
                case PermissionGroup.Nobody:
                    return false;
                case PermissionGroup.User:
                    return true;
                case PermissionGroup.Admin:
                    return user.IsAdmin;
            }

            return false;
        }
    }
}
