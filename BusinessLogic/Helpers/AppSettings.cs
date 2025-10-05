namespace BusinessLogic.Helpers
{
    public class AppSettings
    {
        // Ключ шифрования токена
        public string Secret { get; set; }

        // рефреш токен для обновления, неактивные токены будут
        // автоматически удалены после указанного времени
        public int RefreshTokenTTL { get; set; }

        // Данные для отправки email
        public string EmailFrom { get; set; }
        public string EmailFromName { get; set; }
        public string BrevoApiKey { get; set; }
    }
}