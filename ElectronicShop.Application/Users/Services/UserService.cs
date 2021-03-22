using AutoMapper;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Application.Users.Commands;
using ElectronicShop.Application.Users.Models;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Enums;
using ElectronicShop.Utilities.SystemConstants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<ApiResult<string>> CreateAsync(CreateUserCommand request)
        {
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            var userUserName = await _userManager.FindByNameAsync(request.UserName);

            if(userEmail !=null || userUserName != null )
            {
                return new ApiErrorResult<string>("Tài khoản đã tồn tại");
            }

            var user = _mapper.Map<AspNetUser>(request);

            user.DateCreated = DateTime.Now;

            user.DateModified = user.DateCreated;

            user.Status = UserStatus.ACTIVE;

            try
            {
                await _userManager.CreateAsync(user, request.Password);
                await AddUserRoleAsync(user, request.UserInRole);
            }
            catch
            {
                await _userManager.DeleteAsync(user);

                return await Task.FromResult(
                    new ApiErrorResult<string>("Đăng ký thất bại, không thể thêm quyền cho người dùng"));
            }

            return await Task.FromResult(new ApiSuccessResult<string>("Thêm người dùng thành công"));

        }

        private async Task AddUserRoleAsync(AspNetUser user, string roleName)
        {
            var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole(Constants.ADMIN);

            //var curentUser = _httpContextAccessor.HttpContext.User.Identity.Name;

            var curentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string role = Constants.USERROLENAME;

            user.CreatedBy = user.Id;

            if(isAdmin)
            {
                role = roleName;
                //user.CreatedBy = curentUser;

                user.CreatedBy = Int32.Parse(curentUser);
            }

            await _userManager.UpdateAsync(user);

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
