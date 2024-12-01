using BaseApplication.Domains.ViewModels;

namespace BaseApplication.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(MailTemplate mail);
    }
}
