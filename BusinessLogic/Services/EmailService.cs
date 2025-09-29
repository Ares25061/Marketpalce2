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

        public async Task SendAsync(string to, string subject, string html, string from = null)
        {
            // Пробуем разные порты
            var portConfigs = new[]
            {
        new { Port = 587, Security = SecureSocketOptions.StartTls },
        new { Port = 465, Security = SecureSocketOptions.SslOnConnect },
        new { Port = 25, Security = SecureSocketOptions.StartTls },
        new { Port = 2525, Security = SecureSocketOptions.StartTls } // Альтернативный порт
    };

            foreach (var config in portConfigs)
            {
                using var smtp = new SmtpClient();
                smtp.Timeout = 30000;

                try
                {
                    Console.WriteLine($"🔄 Trying SMTP {_appSettings.SmtpHost}:{config.Port}");

                    await smtp.ConnectAsync(_appSettings.SmtpHost, config.Port, config.Security);
                    await smtp.AuthenticateAsync(_appSettings.SmtpUser, _appSettings.SmtpPass);

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
                    email.To.Add(MailboxAddress.Parse(to));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = html };

                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);

                    Console.WriteLine($"✅ Email sent via port {config.Port}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Port {config.Port} failed: {ex.Message}");
                }
            }

            Console.WriteLine("❌ All SMTP ports failed");
        }

        public async Task SendWithRetryAsync(string to, string subject, string html, string from = null, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    await SendAsync(to, subject, html, from);
                    return; // Успешно отправили
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {i + 1} failed: {ex.Message}");
                    if (i == maxRetries - 1)
                    {
                        Console.WriteLine($"All {maxRetries} attempts failed for email to {to}");
                        throw;
                    }

                    await Task.Delay(2000 * (i + 1)); // Экспоненциальная задержка
                }
            }
        }
    }
}