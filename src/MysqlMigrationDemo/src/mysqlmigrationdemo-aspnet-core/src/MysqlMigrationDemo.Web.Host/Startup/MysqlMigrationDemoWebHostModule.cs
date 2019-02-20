using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MysqlMigrationDemo.Configuration;

namespace MysqlMigrationDemo.Web.Host.Startup
{
    [DependsOn(
       typeof(MysqlMigrationDemoWebCoreModule))]
    public class MysqlMigrationDemoWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MysqlMigrationDemoWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MysqlMigrationDemoWebHostModule).GetAssembly());
        }
    }
}
