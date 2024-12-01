using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BaseApplication.Repository.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly MailSettings _mailSettings;    
        public EmailRepository(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(MailTemplate mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                email.Headers.Add(HeaderId.From, _mailSettings.DisplayName);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                if (mailRequest.Attach != null)
                {
                    builder.Attachments.Add(mailRequest.Attach);
                }
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
