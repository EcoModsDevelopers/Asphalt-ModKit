using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Service.Permissions
{
    public interface IPermissionChecker
    {
        bool Contains(string permission);
        
        bool HasPermission(User player, string permission);

        bool CheckPermission(User player, string permission);
    }
}
