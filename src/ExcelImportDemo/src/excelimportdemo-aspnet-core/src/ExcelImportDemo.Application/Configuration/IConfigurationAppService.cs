using System.Threading.Tasks;
using ExcelImportDemo.Configuration.Dto;

namespace ExcelImportDemo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
