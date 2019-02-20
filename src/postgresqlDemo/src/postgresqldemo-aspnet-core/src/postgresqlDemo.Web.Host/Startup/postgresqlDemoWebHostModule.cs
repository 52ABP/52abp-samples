using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using postgresqlDemo.Configuration;

namespace postgresqlDemo.Web.Host.Startup
{
    [DependsOn(
       typeof(postgresqlDemoWebCoreModule))]
    public class postgresqlDemoWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public postgresqlDemoWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(postgresqlDemoWebHostModule).GetAssembly());
        }
    }
}
