using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExcelImportDemo.MultiTenancy.Dto;

namespace ExcelImportDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
