using Abp.Authorization;
using postgresqlDemo.Authorization.Roles;
using postgresqlDemo.Authorization.Users;

namespace postgresqlDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
