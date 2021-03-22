using ElectronicShop.Application.Common.Models;
using ElectronicShop.Application.Users.Services;
using ElectronicShop.Data.Enums;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<ApiResult<string>>
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        [RegularExpression("(?=^.{8,30}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>.<,])(?!.*\\s).*$",
            ErrorMessage = "Validation for 8-30 characters with characters, numbers, 1 upper case letter and special characters.")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string FirstMiddleName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string UserInRole {get;set;}
    }

    public class CreateUserHandle : IRequestHandler<CreateUserCommand, ApiResult<string>>
    {
        private readonly IUserService _userService;
        public CreateUserHandle(IUserService userservice)
        {
            _userService = userservice;
        }
        public async Task<ApiResult<string>> Handle(CreateUserCommand request, CancellationToken cancelationToken)
        {
            return await _userService.CreateAsync(request);
        }
    }
}
