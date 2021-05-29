using ElectronicShop.Application.Categories.Services;
using ElectronicShop.Application.Common.Models;
using ElectronicShop.Data.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<ApiResult<Category>>
    {
        public int CategoryId { get; }

        public GetCategoryByIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetCategoryByIdHandle : IRequestHandler<GetCategoryByIdQuery, ApiResult<Category>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdHandle(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ApiResult<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetByIdAsync(request.CategoryId);
        }
    }
}
