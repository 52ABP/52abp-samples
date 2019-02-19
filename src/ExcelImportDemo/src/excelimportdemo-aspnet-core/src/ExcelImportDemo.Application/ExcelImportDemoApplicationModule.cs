using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExcelImportDemo.Authorization;

namespace ExcelImportDemo
{
    [DependsOn(
        typeof(ExcelImportDemoCoreModule),
        typeof(AbpAutoMapperModule))]
    public class ExcelImportDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ExcelImportDemoAuthorizationProvider>();

            // 自定义类型映射
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                // XXXMapper.CreateMappers(configuration);


            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ExcelImportDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
