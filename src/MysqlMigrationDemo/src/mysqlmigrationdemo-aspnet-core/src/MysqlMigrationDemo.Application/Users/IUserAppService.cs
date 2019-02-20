using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MysqlMigrationDemo.Roles.Dto;
using MysqlMigrationDemo.Users.Dto;

namespace MysqlMigrationDemo.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
