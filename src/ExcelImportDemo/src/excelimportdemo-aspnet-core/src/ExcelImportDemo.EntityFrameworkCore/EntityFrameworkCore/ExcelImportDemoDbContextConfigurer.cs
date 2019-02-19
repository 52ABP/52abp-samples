using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ExcelImportDemo.EntityFrameworkCore
{
    public static class ExcelImportDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ExcelImportDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ExcelImportDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
