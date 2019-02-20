using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MysqlMigrationDemo.Authorization;

namespace MysqlMigrationDemo
{
    [DependsOn(
        typeof(MysqlMigrationDemoCoreModule),
        typeof(AbpAutoMapperModule))]
    public class MysqlMigrationDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MysqlMigrationDemoAuthorizationProvider>();

            // 自定义类型映射
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                // XXXMapper.CreateMappers(configuration);


            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MysqlMigrationDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
