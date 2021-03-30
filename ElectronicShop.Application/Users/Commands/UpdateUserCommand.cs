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
    public class UpdateUserCommand : IRequest<ApiResult<string>>
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstMiddleName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required, EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }

        public string UserInRole { get; set; }
    }

    public class UpdateUserHandle : IRequestHandler<UpdateUserCommand, ApiResult<string>>
    {
        private readonly IUserService _userService;
        public UpdateUserHandle(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResult<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateAsync(request);
        }
    }
}
