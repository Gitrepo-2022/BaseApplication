using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseApplication.Domains.ViewModels;

namespace BaseApplication.Repository.Interface
{
    public interface IEmailRepository
    {
        public Task<bool> SendEmailAsync(MailTemplate mail);
    }
}
