﻿using ElectronicShop.Application.Users.Commands;
using ElectronicShop.Application.Users.Queries;
using ElectronicShop.Utilities.SystemConstants;
using ElectronicShop.WebApi.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = Constants.ADMIN)]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var query = new GetByIdUserQuery(userId);

            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-all")]
        [Authorize(Roles = Constants.ADMIN)]
        public async Task<IActionResult> GetAllUser()
        {
            var query = new GetAllUserQuery();

            return Ok(await _mediator.Send(query));
        }

        [HttpPost("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
