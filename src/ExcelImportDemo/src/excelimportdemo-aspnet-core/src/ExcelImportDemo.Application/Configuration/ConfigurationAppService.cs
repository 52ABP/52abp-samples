using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ExcelImportDemo.Configuration.Dto;

namespace ExcelImportDemo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ExcelImportDemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
