using Abp.Authorization;
using MysqlMigrationDemo.Authorization.Roles;
using MysqlMigrationDemo.Authorization.Users;

namespace MysqlMigrationDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
