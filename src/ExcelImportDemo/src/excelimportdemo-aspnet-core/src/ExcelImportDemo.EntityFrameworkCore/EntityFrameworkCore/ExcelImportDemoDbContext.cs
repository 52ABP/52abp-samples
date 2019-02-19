using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ExcelImportDemo.Authorization.Roles;
using ExcelImportDemo.Authorization.Users;
using ExcelImportDemo.MultiTenancy;

namespace ExcelImportDemo.EntityFrameworkCore
{
    public class ExcelImportDemoDbContext : AbpZeroDbContext<Tenant, Role, User, ExcelImportDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ExcelImportDemoDbContext(DbContextOptions<ExcelImportDemoDbContext> options)
            : base(options)
        {
        }
    }
}
