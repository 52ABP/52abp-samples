using System.Threading.Tasks;
using Abp.Application.Services;
using ExcelImportDemo.Authorization.Accounts.Dto;

namespace ExcelImportDemo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
