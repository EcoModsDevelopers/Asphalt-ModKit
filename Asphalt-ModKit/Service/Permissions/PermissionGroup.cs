namespace Asphalt.Service.Permissions
{
    public enum PermissionGroup
    {
        Null,
        Nobody,
        User,
        Admin
    }

    static class PermissionGroupMethods
    {
        public static string GetString(PermissionGroup group)
        {
            switch(group)
            {
                case PermissionGroup.Nobody:
                    return "nobody";
                case PermissionGroup.User:
                    return "user";
                case PermissionGroup.Admin:
                    return "admin";
            }

            return null;
        }

        public static PermissionGroup GetGroup(string group)
        {
            switch (group)
            {
                case "nobody":
                    return PermissionGroup.Nobody;
                case "user":
                    return PermissionGroup.User;
                case "admin":
                    return PermissionGroup.Admin;
            }

            return PermissionGroup.Null;
        }
    }
}
