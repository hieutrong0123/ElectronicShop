using AutoMapper;
using ElectronicShop.Application.Authentications.Commands;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Enums;
using ElectronicShop.Utilities.Session;
using ElectronicShop.Utilities.SystemConstants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Authentications.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager,
            IConfiguration config, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> AuthenticateAsync(AuthenticateCommand request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || user.Status.Equals(UserStatus.DELETED))
            {
                return new ApiErrorResult<string>("Tài khoản không tồn tại");
            }

            if (user.Status.Equals(UserStatus.DISABLE))
            {
                return new ApiErrorResult<string>("Tài khoản đã bị khóa");
            }

            if (user.PasswordHash is null)
            {
                return new ApiErrorResult<string>("Đăng nhập thất bại, sai phương thức đăng nhập.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập thất bại");
            }

            _httpContextAccessor.HttpContext.Session.SetComplexData(Constants.CURRENTUSER, user);

            var roles = await _userManager.GetRolesAsync(user);

            var token = CreateToken(roles, user);

            return await Task.FromResult(
                new ApiSuccessResult<string>()
                {
                    Message = roles[0],
                    ResultObj = token
                });
        }

        private string CreateToken(IList<string> roles, AspNetUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
