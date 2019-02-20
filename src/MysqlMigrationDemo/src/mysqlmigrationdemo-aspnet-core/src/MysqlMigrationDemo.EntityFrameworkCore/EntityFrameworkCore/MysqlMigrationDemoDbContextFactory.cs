using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MysqlMigrationDemo.Configuration;
using MysqlMigrationDemo.Web;

namespace MysqlMigrationDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MysqlMigrationDemoDbContextFactory : IDesignTimeDbContextFactory<MysqlMigrationDemoDbContext>
    {
        public MysqlMigrationDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MysqlMigrationDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MysqlMigrationDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MysqlMigrationDemoConsts.ConnectionStringName));

            return new MysqlMigrationDemoDbContext(builder.Options);
        }
    }
}
