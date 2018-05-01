namespace Asphalt.Service.Permissions
{
    public class Permission
    {
        public string Key { get; private set; }
        public PermissionGroup DefaultGroup { get; private set; }

        public Permission(string key, PermissionGroup defaultGroup)
        {
            this.Key = key;
            this.DefaultGroup = defaultGroup;
        }
    }
}
