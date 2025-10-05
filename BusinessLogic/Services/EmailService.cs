using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public EmailService(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        public async System.Threading.Tasks.Task Send(string to, string subject, string html, string from = null)
        {
            try
            {
                // Получаем API ключ из переменных окружения Render
                var apiKey = _configuration["AppSettings:BrevoApiKey"] ?? _appSettings.BrevoApiKey;

                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("Brevo API Key is missing");
                }

                var fromEmail = _appSettings.EmailFrom;
                var fromName = _appSettings.EmailFromName;

                Console.WriteLine($"Sending email from: {fromEmail} to: {to}");
                Console.WriteLine($"API Key exists: {!string.IsNullOrEmpty(apiKey)}");

                // Настройка Brevo API
                var config = new brevo_csharp.Client.Configuration();
                config.ApiKey.Add("api-key", apiKey);

                var brevoApi = new TransactionalEmailsApi(config);

                // Создание письма
                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender(email: fromEmail, name: fromName),
                    to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                    subject: subject,
                    htmlContent: html
                );

                Console.WriteLine("Attempting to send email via Brevo...");

                // Отправка
                var result = await brevoApi.SendTransacEmailAsync(sendSmtpEmail);
                Console.WriteLine($"✅ Email sent via Brevo. Message ID: {result.MessageId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                throw;
            }
        }
    }
}