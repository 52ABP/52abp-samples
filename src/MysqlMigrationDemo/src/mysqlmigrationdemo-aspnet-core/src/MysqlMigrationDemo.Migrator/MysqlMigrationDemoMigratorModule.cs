using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MysqlMigrationDemo.Configuration;
using MysqlMigrationDemo.EntityFrameworkCore;
using MysqlMigrationDemo.Migrator.DependencyInjection;

namespace MysqlMigrationDemo.Migrator
{
    [DependsOn(typeof(MysqlMigrationDemoEntityFrameworkModule))]
    public class MysqlMigrationDemoMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MysqlMigrationDemoMigratorModule(MysqlMigrationDemoEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(MysqlMigrationDemoMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                MysqlMigrationDemoConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MysqlMigrationDemoMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
