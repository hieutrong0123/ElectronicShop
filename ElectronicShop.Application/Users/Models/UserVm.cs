using ElectronicShop.Data.Enums;
using System;

namespace ElectronicShop.Application.Users.Models
{
    public class UserVm
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstMiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public UserStatus Status { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserInRole { get; set; }
    }
}
