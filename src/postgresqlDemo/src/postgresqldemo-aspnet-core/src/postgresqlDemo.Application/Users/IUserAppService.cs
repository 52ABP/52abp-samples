using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using postgresqlDemo.Roles.Dto;
using postgresqlDemo.Users.Dto;

namespace postgresqlDemo.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
