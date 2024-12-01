using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Interface;
using BaseApplication.Services.Interfaces;

namespace BaseApplication.Services.Services
{
    public class EmailService : IEmailService
    {
        public readonly IEmailRepository _emailRepository;
        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<bool> SendEmailAsync(MailTemplate mail)
        {
            var res = await _emailRepository.SendEmailAsync(mail);
            return res;
        }
    }
}
