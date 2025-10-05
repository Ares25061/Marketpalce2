using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async System.Threading.Tasks.Task Send(string to, string subject, string html, string from = null)
        {
            try
            {
                // Используем подтвержденного отправителя
                var fromEmail = _appSettings.EmailFrom; // явно указываем подтвержденный email
                var fromName = _appSettings.EmailFromName;

                Console.WriteLine($"Sending email from: {fromEmail} to: {to}");
                Console.WriteLine($"API Key: {_appSettings.BrevoApiKey?.Substring(0, 10)}...");

                // Настройка Brevo API
                var config = new brevo_csharp.Client.Configuration();
                config.ApiKey.Add("api-key", _appSettings.BrevoApiKey);

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
            catch (brevo_csharp.Client.ApiException brevoEx)
            {
                Console.WriteLine($"❌ Brevo API Exception: {brevoEx.Message}");
                Console.WriteLine($"❌ Error Code: {brevoEx.ErrorCode}");
                Console.WriteLine($"❌ Error Content: {brevoEx.ErrorContent}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ General error: {ex.Message}");
                throw;
            }
        }
    }
}