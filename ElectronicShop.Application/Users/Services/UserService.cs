using AutoMapper;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Application.Users.Models;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ElectronicShopDbContext _context;

        public UserService(UserManager<AspNetUser> userManager, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ElectronicShopDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ApiResult<UserVm>> GetByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null || user.Status.Equals(UserStatus.DELETED)/*Đã xóa*/)
            {
                return new ApiErrorResult<UserVm>("Người dùng không tồn tại");
            }

            var result = _mapper.Map<UserVm>(user);

            var roles = await _userManager.GetRolesAsync(user);

            result.UserInRole = roles[0];

            return await Task.FromResult(new ApiSuccessResult<UserVm>(result));
        }

        public async Task<ApiResult<List<UserVm>>> GetAllAsync()
        {
            var users = await _context.Users
                .Where(x => x.Status != UserStatus.DELETED)
                .ToListAsync();

            if (users is null)
            {
                return new ApiErrorResult<List<UserVm>>("Không tìm thấy được người dùng nào.");
            }

            var result = _mapper.Map<List<UserVm>>(users);

            for (int i = 0; i < users.Count; i++)
            {
                var role = await _userManager.GetRolesAsync(users[i]);

                result[i].UserInRole = role[0];
            }

            return await Task.FromResult(new ApiSuccessResult<List<UserVm>>(result));
        }
    }
}
