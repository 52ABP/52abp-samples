using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MysqlMigrationDemo.Controllers
{
    public abstract class MysqlMigrationDemoControllerBase: AbpController
    {
        protected MysqlMigrationDemoControllerBase()
        {
            LocalizationSourceName = MysqlMigrationDemoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
