using ElectronicShop.Application.Products.Commands;
using ElectronicShop.Application.Products.Queries;
using ElectronicShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Products.Services
{
    public interface IProductService
    {
        Task<int> CreateProduct(CreateProduct request);

        Task<int> UpdateProduct(UpdateProduct request);

        Task<int> DeleteProduct(int productId);

        Task<List<Product>> GetAllProduct();

        Task<Product> GetProductByIdAsync(int productId);

        Task<List<Product>> GetProductByCategoryId(int categoryId);

        Task<List<Product>> GetProductByFiler(FilterProductQuery filter);
    }
}
