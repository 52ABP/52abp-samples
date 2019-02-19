using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ExcelImportDemo.Configuration;
using ExcelImportDemo.Web;

namespace ExcelImportDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ExcelImportDemoDbContextFactory : IDesignTimeDbContextFactory<ExcelImportDemoDbContext>
    {
        public ExcelImportDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ExcelImportDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ExcelImportDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ExcelImportDemoConsts.ConnectionStringName));

            return new ExcelImportDemoDbContext(builder.Options);
        }
    }
}
