using System.Net;
using System.Net.Mail;
namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class SMTPService
    {
        private readonly string _appEmail;
        private readonly string _securityPassword;
        private readonly SmtpClient _smtpClient;

        public SMTPService(string appEmail, string securityPassword)
        {
            _securityPassword = securityPassword;
            _appEmail = appEmail;
            _smtpClient = new SmtpClient("smtp.gmail.com", 587);
            _smtpClient.Credentials = new NetworkCredential(_appEmail, "zqqsptorzxqzeecj");
            _smtpClient.EnableSsl = true;
        }

        public async Task sendMailAsync(string recipient, string subject, string body)
        {
            await _smtpClient.SendMailAsync(new MailMessage(_appEmail, recipient, subject, body));
        }
    }
}
