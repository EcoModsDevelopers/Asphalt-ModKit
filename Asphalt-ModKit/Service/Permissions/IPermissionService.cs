using Eco.Gameplay.Players;

namespace Asphalt.Service.Permissions
{
    public interface IPermissionService
    {
        bool Contains(string permission);
        
        bool HasPermission(User user, string permission);

        bool CheckPermission(User user, string permission);
    }
}
