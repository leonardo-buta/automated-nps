using Abp.Authorization;
using NPS.Authorization.Roles;
using NPS.Authorization.Users;

namespace NPS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
