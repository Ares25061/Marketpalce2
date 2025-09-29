namespace BusinessLogic.Authorization
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string html, string from = null);
        Task SendWithRetryAsync(string to, string subject, string html, string from = null, int maxRetries = 3);
    }
}