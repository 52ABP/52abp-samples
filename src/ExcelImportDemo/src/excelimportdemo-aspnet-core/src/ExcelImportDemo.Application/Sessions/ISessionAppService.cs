using System.Threading.Tasks;
using Abp.Application.Services;
using ExcelImportDemo.Sessions.Dto;

namespace ExcelImportDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
