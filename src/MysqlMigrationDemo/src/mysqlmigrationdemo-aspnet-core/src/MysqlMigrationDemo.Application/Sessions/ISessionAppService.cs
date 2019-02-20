using System.Threading.Tasks;
using Abp.Application.Services;
using MysqlMigrationDemo.Sessions.Dto;

namespace MysqlMigrationDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
