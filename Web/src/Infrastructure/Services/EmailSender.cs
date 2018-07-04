using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;

namespace EnjoyCodes.eShopOnWeb.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
