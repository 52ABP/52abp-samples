using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using ExcelImportDemo.Authorization.Users;
using ExcelImportDemo.Editions;

namespace ExcelImportDemo.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }





    }
}
