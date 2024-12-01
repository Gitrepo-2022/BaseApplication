using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApplication.Domains.ViewModels
{
    public  class EmailDomainViewModel
    {
        
    }
    public class MailTemplate
    {

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attach { get; set; }
        public int Id { get; set; }
    }
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

  
}
