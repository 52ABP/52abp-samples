using System.Threading.Tasks;
using Abp.Application.Services;
using MysqlMigrationDemo.Authorization.Accounts.Dto;

namespace MysqlMigrationDemo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
