using ElectronicShop.Application.Categories.Commands;
using ElectronicShop.Application.Common.Models;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Categories.Services
{
    public interface ICategoryService
    {
        Task<ApiResult<string>> CreateAsync(CreateCategoryCommand request);

        Task<ApiResult<string>> UpdateAsync(UpdateCategoryCommand request);
    }
}
