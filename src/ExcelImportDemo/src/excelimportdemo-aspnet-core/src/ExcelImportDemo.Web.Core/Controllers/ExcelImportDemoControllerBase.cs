using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ExcelImportDemo.Controllers
{
    public abstract class ExcelImportDemoControllerBase: AbpController
    {
        protected ExcelImportDemoControllerBase()
        {
            LocalizationSourceName = ExcelImportDemoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
