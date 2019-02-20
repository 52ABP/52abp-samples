using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using postgresqlDemo.Authorization;

namespace postgresqlDemo
{
    [DependsOn(
        typeof(postgresqlDemoCoreModule),
        typeof(AbpAutoMapperModule))]
    public class postgresqlDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<postgresqlDemoAuthorizationProvider>();

            // 自定义类型映射
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                // XXXMapper.CreateMappers(configuration);


            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(postgresqlDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
