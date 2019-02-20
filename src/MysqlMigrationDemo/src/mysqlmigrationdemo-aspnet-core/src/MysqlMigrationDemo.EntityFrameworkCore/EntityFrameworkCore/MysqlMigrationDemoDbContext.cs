using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MysqlMigrationDemo.Authorization.Roles;
using MysqlMigrationDemo.Authorization.Users;
using MysqlMigrationDemo.MultiTenancy;

namespace MysqlMigrationDemo.EntityFrameworkCore
{
    public class MysqlMigrationDemoDbContext : AbpZeroDbContext<Tenant, Role, User, MysqlMigrationDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MysqlMigrationDemoDbContext(DbContextOptions<MysqlMigrationDemoDbContext> options)
            : base(options)
        {
        }
    }
}
