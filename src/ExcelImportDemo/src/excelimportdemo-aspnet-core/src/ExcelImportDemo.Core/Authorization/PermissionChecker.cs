using Abp.Authorization;
using ExcelImportDemo.Authorization.Roles;
using ExcelImportDemo.Authorization.Users;

namespace ExcelImportDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
