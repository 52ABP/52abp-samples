using Abp.Application.Services;
using Abp.Application.Services.Dto;
using postgresqlDemo.MultiTenancy.Dto;

namespace postgresqlDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
