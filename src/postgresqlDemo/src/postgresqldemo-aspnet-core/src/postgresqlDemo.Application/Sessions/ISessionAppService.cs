using System.Threading.Tasks;
using Abp.Application.Services;
using postgresqlDemo.Sessions.Dto;

namespace postgresqlDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
