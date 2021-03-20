using AutoMapper;
using ElectronicShop.Application.Users.Commands;
using ElectronicShop.Application.Users.Models;
using ElectronicShop.Data.Entities;

namespace ElectronicShop.Application.Common.Mapper
{
    public class ElectronicShopProfile : Profile
    {
        public ElectronicShopProfile()
        {
            CreateMap<AspNetUser, UserVm>();
            CreateMap<CreateUserCommand, AspNetUser>();
        }
    }
}