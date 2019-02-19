using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExcelImportDemo.Configuration;

namespace ExcelImportDemo.Web.Host.Startup
{
    [DependsOn(
       typeof(ExcelImportDemoWebCoreModule))]
    public class ExcelImportDemoWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ExcelImportDemoWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ExcelImportDemoWebHostModule).GetAssembly());
        }
    }
}
