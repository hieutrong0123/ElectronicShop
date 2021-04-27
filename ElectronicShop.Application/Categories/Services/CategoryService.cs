using AutoMapper;
using ElectronicShop.Application.Categories.Commands;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ElectronicShopDbContext _context;

        public CategoryService(IMapper mapper, IHttpContextAccessor httpContextAccessor, ElectronicShopDbContext context)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<ApiResult<string>> CreateAsync(CreateCategoryCommand request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var category = _mapper.Map<Category>(request);

            category.DateCreated = DateTime.Now;

            category.DateModified = category.DateCreated;

            category.CreatedBy = Int32.Parse(currentUser);

            category.ModifiedBy = Int32.Parse(currentUser);

            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(new ApiErrorResult<string>("Thêm danh mục thất bại"));
            }
            return await Task.FromResult(new ApiSuccessResult<string>("Thêm danh mục thành công"));
        }
    }
}
