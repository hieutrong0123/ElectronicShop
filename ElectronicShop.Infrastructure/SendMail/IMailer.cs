using System.Threading.Tasks;

namespace ElectronicShop.Infrastructure.SendMail
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
