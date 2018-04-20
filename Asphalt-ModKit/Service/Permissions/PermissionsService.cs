using Eco.Gameplay.Players;
using System.Collections.Generic;

namespace Asphalt.Service.Permissions
{
    public class PermissionsService : AbstractService
    {
        private PermissionsFile file;

        private Dictionary<string, PermissionGroup> permissions;

        public PermissionsService(AsphaltMod mod) : base(mod) { }

        public override void Init()
        {
            file = new PermissionsFile(Mod);
            permissions = new Dictionary<string, PermissionGroup>();

            LoadDefaultPermissions();
            LoadPermissionsFromFile();
        }

        public override void Reload()
        {
            permissions.Clear();

            LoadDefaultPermissions();
            LoadPermissionsFromFile();
        }

        private void LoadDefaultPermissions()
        {
            foreach(Permission permission in Mod.GetPermissions())
            {
                if (permissions.ContainsKey(permission.Key))
                    permissions.Remove(permission.Key);

                permissions.Add(permission.Key, permission.DefaultGroup);
            }
        }

        private void LoadPermissionsFromFile()
        {
            file.Reload();

            foreach (KeyValuePair<string, object> pair in file.GetPermissions())
            {
                try
                {
                    string group_s = (string)pair.Value;
                    PermissionGroup group = PermissionGroupMethods.GetGroup(group_s);

                    if (group.Equals(PermissionGroup.Null))
                        continue;

                    if (!permissions.ContainsKey(pair.Key))
                        continue;

                    permissions[pair.Key] = group;
                }
                catch
                {
                    //TODO: maybe log smth
                }
            }

            file.SetPermissions(permissions);
            file.Save();
        }

        public bool HasPermission(User user, string key)
        {
            if (!permissions.ContainsKey(key))
                //TODO: Exception?
                return false;

            switch(permissions[key])
            {
                case PermissionGroup.Null:
                    //TODO: Exception?
                    return false;
                case PermissionGroup.Nobody:
                    return false;
                case PermissionGroup.User:
                    return true;
                case PermissionGroup.Admin:
                    return user.IsAdmin;
            }

            return false;
        }

        public bool CheckPermission(User user, string key)
        {
            if (HasPermission(user, key))
                return true;

            user.Player.SendTemporaryErrorAlreadyLocalized("Sorry, but you don't have the permission to do this!");
            return false;
        }
    }
}
