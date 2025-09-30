using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            try
            {
                var email = new MimeMessage();
                // Используем EmailFrom из конфигурации, если from не передан
                Console.WriteLine('1');
                email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
                Console.WriteLine('2');
                email.To.Add(MailboxAddress.Parse(to));
                Console.WriteLine('3');
                email.Subject = subject;
                Console.WriteLine('4');
                email.Body = new TextPart(TextFormat.Html) { Text = html };
                Console.WriteLine('5');
                using var smtp = new SmtpClient();
                Console.WriteLine('6');
                smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.SslOnConnect);
                Console.WriteLine('7');
                smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
                Console.WriteLine('8');
                smtp.Send(email);
                Console.WriteLine('9');
                smtp.Disconnect(true);
                Console.WriteLine("10");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ЛЕЕЕ ОШИБКА БРАТ " + ex.Message);
            }
        }
    }
}