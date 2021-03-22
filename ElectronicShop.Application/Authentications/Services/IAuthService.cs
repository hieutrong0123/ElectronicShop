using ElectronicShop.Application.Authentications.Commands;
using ElectronicShop.Application.Common.Models;
using System.Threading.Tasks;

namespace ElectronicShop.Application.Authentications.Services
{
    public interface IAuthService
    {
        Task<ApiResult<string>> AuthenticateAsync(AuthenticateCommand request);
        Task<bool> SignOutAsync();
    }
}
