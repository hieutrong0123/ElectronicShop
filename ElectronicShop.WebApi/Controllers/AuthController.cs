using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicShop.Application.Authentications.Commands;
using ElectronicShop.Infrastructure.SendMail;
using ElectronicShop.WebApi.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailer _mailer;

        public AuthController(IMediator mediator, IMailer mailer)
        {
            _mediator = mediator;
            _mailer = mailer;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
        {
            var result = await _mediator.Send(request);

            if(!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            string href = "http://localhost:3001/tao-moi-mat-khau/" + request.Email + "/" + result.ResultObj;

            string body =
                "<h3> Quý khách vui lòng click vào đường link bên dưới để chuyển đến trang thay đổi mật khẩu.</h3>" +
                "<a href=\"" + href + "\">Click vào đây</a>";

            await _mailer.SendEmailAsync("electronicshop0123@gmail.com", "Reset Password", body);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await _mediator.Send(new SignOutCommand());
            return NoContent();
        }
    }
}
