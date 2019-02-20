using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace postgresqlDemo.Controllers
{
    public abstract class postgresqlDemoControllerBase: AbpController
    {
        protected postgresqlDemoControllerBase()
        {
            LocalizationSourceName = postgresqlDemoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
