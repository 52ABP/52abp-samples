using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MysqlMigrationDemo.EntityFrameworkCore
{
    public static class MysqlMigrationDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MysqlMigrationDemoDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MysqlMigrationDemoDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
