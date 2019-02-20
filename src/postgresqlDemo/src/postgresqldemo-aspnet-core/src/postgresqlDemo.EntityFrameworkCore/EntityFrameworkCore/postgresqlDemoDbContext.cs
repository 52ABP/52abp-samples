using Abp.Localization;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using postgresqlDemo.Authorization.Roles;
using postgresqlDemo.Authorization.Users;
using postgresqlDemo.MultiTenancy;

namespace postgresqlDemo.EntityFrameworkCore
{
    public class postgresqlDemoDbContext : AbpZeroDbContext<Tenant, Role, User, postgresqlDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public postgresqlDemoDbContext(DbContextOptions<postgresqlDemoDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


       //     modelBuilder.Entity<ApplicationLanguageText>()

        }
    }
}
