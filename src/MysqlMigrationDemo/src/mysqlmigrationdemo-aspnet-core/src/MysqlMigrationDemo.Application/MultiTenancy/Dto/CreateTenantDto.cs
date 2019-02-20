using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace MysqlMigrationDemo.MultiTenancy.Dto
{
    /// <summary>
    /// �����⻧��DTO��Ϣ
    /// </summary>
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantDto
    {
        /// <summary>
        /// ȫ��Ψһ���⻧Id
        /// </summary>
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [StringLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }

        public bool IsActive {get; set;}

        /// <summary>
        /// �⻧����Ա����
        /// </summary>
        public string TenantAdminPassword { get; set; }

    }
}
