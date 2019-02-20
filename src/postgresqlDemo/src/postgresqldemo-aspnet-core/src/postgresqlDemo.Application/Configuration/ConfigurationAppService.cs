using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using postgresqlDemo.Configuration.Dto;

namespace postgresqlDemo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : postgresqlDemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
