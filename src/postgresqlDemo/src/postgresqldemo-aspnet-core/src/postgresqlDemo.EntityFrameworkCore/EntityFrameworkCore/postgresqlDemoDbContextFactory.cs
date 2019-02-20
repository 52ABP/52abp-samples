using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using postgresqlDemo.Configuration;
using postgresqlDemo.Web;

namespace postgresqlDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class postgresqlDemoDbContextFactory : IDesignTimeDbContextFactory<postgresqlDemoDbContext>
    {
        public postgresqlDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<postgresqlDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            postgresqlDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(postgresqlDemoConsts.ConnectionStringName));

            return new postgresqlDemoDbContext(builder.Options);
        }
    }
}
