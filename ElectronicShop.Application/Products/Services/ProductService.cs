using ElectronicShop.Application.Products.Commands;
using ElectronicShop.Application.Products.Queries;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly ElectronicShopDbContext _context;
        public ProductService(ElectronicShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateProduct(CreateProduct request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Specifications = request.Specifications,
                Description = request.Description,
                GoodsReceipt = request.GoodsReceipt,
                Inventory = request.Inventory,
                Status = request.Status,
                CategoryId = request.CategoryId,
                Alias = request.Alias,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CreatedBy = "",
                ModefiedBy = ""
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateProduct(UpdateProduct update)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == update.Id && x.Status != ProductStatus.HIDDEN);

            if (product is null)
            {
                //return await Task.FromResult(new ApiErrorResult<string>("Không tìm thấy sản phẩm cần cập nhật"));
            }

            product.Name = update.Name;
            product.Price = update.Price;
            product.Specifications = update.Specifications;
            product.Description = update.Description;
            product.GoodsReceipt = update.GoodsReceipt;
            product.Inventory = update.Inventory;
            product.Status = update.Status;
            product.CategoryId = update.CategoryId;
            product.Alias = update.Alias;
            product.DateModified = DateTime.Now;
            product.ModefiedBy = update.ModefiedBy;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId && x.Status != ProductStatus.HIDDEN);

            if (product is null)
            {
                //return await Task.FromResult(new ApiErrorResult<string>("Không tìm thấy sản phẩm cần cập nhật"));
            }

            product.Status = ProductStatus.HIDDEN;

            return await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId && x.Status != ProductStatus.HIDDEN);

            if (product is null)
            {
                //return await Task.FromResult(new ApiErrorResult<Product>("Không tìm thấy sản phẩm"));
            }

            return product;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            var products = await _context.Products
                .Where(x => x.Status != ProductStatus.HIDDEN)
                .ToListAsync();

            if (products is null)
            {
                //return await Task.FromResult(new ApiErrorResult<List<Product>>("Không tìm thấy sản phẩm"));
            }

            return products;
        }

        public async Task<List<Product>> GetProductByCategoryId(int categoryId)
        {
            var cate = await _context.Categories.FindAsync(categoryId);

            List<Product> products = new List<Product>();

            // Nếu Category là Root
            if (cate.RootId is null)
            {
                var query = from category in _context.Categories
                            where category.RootId.Equals(categoryId)
                            select new { CategoryChildren = category };

                foreach (var c in query)
                {
                    products = await _context.Products
                    .Where(x => x.CategoryId.Equals(c.CategoryChildren.Id))
                    .ToListAsync();
                }
            }
            // Nếu Category thông thường
            else
            {
                products = await _context.Products
                    .Where(x => x.CategoryId.Equals(categoryId))
                    .ToListAsync();
            }

            if (products is null)
            {
                //return await Task.FromResult(new ApiErrorResult<List<Product>>("Không tìm thấy sản phẩm"));
            }
            return products;
        }

        public async Task<List<Product>> GetProductByFiler(FilterProductQuery filter)
        {
            var query = await _context.Products
                .Where(x => x.Status != ProductStatus.HIDDEN)
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter.KeyWord))
            {
                query = query.Where(x
                        => x.Name.Contains(filter.KeyWord)
                           || x.Specifications.Contains(filter.KeyWord)
                           || x.Description.Contains(filter.KeyWord))
                    .ToList();
            }

            return query;
        }

        public Task<Product> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
