using Abp.MultiTenancy;
using MysqlMigrationDemo.Authorization.Users;

namespace MysqlMigrationDemo.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
