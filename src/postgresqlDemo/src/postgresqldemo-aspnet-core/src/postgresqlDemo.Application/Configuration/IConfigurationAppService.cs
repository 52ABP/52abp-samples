using System.Threading.Tasks;
using postgresqlDemo.Configuration.Dto;

namespace postgresqlDemo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
