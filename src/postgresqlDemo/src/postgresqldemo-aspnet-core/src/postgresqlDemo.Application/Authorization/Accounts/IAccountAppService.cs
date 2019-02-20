using System.Threading.Tasks;
using Abp.Application.Services;
using postgresqlDemo.Authorization.Accounts.Dto;

namespace postgresqlDemo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
