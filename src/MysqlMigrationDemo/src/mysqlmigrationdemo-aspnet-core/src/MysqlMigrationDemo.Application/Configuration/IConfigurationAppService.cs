using System.Threading.Tasks;
using MysqlMigrationDemo.Configuration.Dto;

namespace MysqlMigrationDemo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
