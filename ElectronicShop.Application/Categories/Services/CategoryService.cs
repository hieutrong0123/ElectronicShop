using AutoMapper;
using ElectronicShop.Application.Categories.Commands;
using ElectronicShop.Application.Categories.Mapper;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<ApiResult<string>> UpdateAsync(UpdateCategoryCommand request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var category = await _context.Categories.FindAsync(request.Id);

                category.Map(request);

                category.ModifiedBy = Int32.Parse(currentUser);

                _context.Categories.Update(category);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(new ApiErrorResult<string>("Cập nhật danh mục sản phẩm thất bại"));
            }

            return await Task.FromResult(new ApiSuccessResult<string>("Cập nhật danh mục sản phẩm thành công"));
        }

        public async Task<ApiResult<Category>> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == id);

            return await Task.FromResult(new ApiSuccessResult<Category>(category));
        }

        public async Task<ApiResult<List<Category>>> GetAllAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();

            return await Task.FromResult(new ApiSuccessResult<List<Category>>(categories));
        }
    }
}
