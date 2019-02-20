using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MysqlMigrationDemo.MultiTenancy.Dto;

namespace MysqlMigrationDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
