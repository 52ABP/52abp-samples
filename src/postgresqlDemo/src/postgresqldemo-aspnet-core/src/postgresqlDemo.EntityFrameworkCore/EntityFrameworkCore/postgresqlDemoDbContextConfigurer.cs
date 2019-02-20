using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace postgresqlDemo.EntityFrameworkCore
{
    public static class postgresqlDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<postgresqlDemoDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<postgresqlDemoDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
