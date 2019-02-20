using MysqlMigrationDemo.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysqlMigrationDemo.MultiTenancy
{
    public interface ITenantRegistrationAppService
    {
        /// <summary>
        /// 注册租户信息
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        Task<TenantDto> RegisterTenantAsync(CreateTenantDto input);
    }
}
