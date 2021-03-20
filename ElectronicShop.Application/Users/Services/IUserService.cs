using ElectronicShop.Application.Common.Models;
using ElectronicShop.Application.Users.Commands;
using ElectronicShop.Application.Users.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Users.Services
{
    public interface IUserService
    {
        Task<ApiResult<UserVm>> GetByIdAsync(int userId);

        Task<ApiResult<List<UserVm>>> GetAllAsync();

        Task<ApiResult<string>> CreateAsync(CreateUserCommand request);
    }
}
