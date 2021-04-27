using ElectronicShop.Application.Categories.Commands;
using ElectronicShop.Utilities.SystemConstants;
using ElectronicShop.WebApi.ActionFilters;
using ElectronicShop.WebApi.AuthorizeRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(Constants.ADMIN, Constants.EMPLOYEE)]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
