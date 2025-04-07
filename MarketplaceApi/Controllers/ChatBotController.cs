using Microsoft.AspNetCore.Mvc;
using Ollama;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly OllamaApiClient _ollama;
        private static readonly Dictionary<string, Dictionary<string, IList<long>>> _userContexts = new Dictionary<string, Dictionary<string, IList<long>>>();

        public ChatBotController(OllamaApiClient ollama)
        {
            _ollama = ollama;
        }

        [HttpPost("send")]
        public async Task SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Ошибка: сообщение не может быть пустым.");
                return;
            }

            var userId = request.UserId ?? "anonymous";
            string teacher = request.Teacher;
            if (string.IsNullOrEmpty(teacher))
            {
                teacher = DetermineTeacher(request.Message);
            }

            if (!_userContexts.TryGetValue(userId, out var teacherContexts))
            {
                teacherContexts = new Dictionary<string, IList<long>>();
                _userContexts[userId] = teacherContexts;
            }

            if (!teacherContexts.TryGetValue(teacher, out var context))
            {
                context = null;
            }

            // Базовый промпт для случая, когда стиль преподавателя не определён
            string basePrompt = "Ты преподаватель. Отвечай на вопрос строго как преподаватель, без лишних деталей, но понятно и профессионально. Вопрос: {request.Message}";

            // Если стиль преподавателя определён, используем стили
            if (teacher != "general")
            {
                basePrompt = "Ты преподаватель и как преподаватель отвечай на вопросы в стиле какого-то преподавателя. Строго следуй этим правилам и НИКОГДА не смешивай стили преподавателей. " +
                             "Отвечай ТОЛЬКО в стиле указанного преподавателя. Никаких исключений, даже если не можешь ответить в стиле — просто дай краткий ответ без лишнего. " +
                             "Вот стили:\n" +
                             "- Шамина: Повторяй ответ дважды. Перед вторым повторением говори 'а я говорила что...', затем дублируй ответ. В конце добавь загадку по теме вопроса с фразой 'Кстати, вот вам загадка:'.\n" +
                             "- Смирнов: Отвечай грубо, но умно. Если в вопросе есть 'я Милютин', пиши только 'Милютин, иди переделывай'.\n" +
                             "- Михайлов: Начинай с 'Объясняю' и пиши кратко, без лишнего.\n" +
                             "- Логинов: Рассуждай философски, глубоко и абстрактно. Упоминай мотоциклы.\n" +
                             "- Самарина: Свяжи ответ с религией или духовностью.\n" +
                             "- Ларионов: Вставь ровно ОДИН раз 'DOCKERRRRRRR' в случайном месте ответа. Упомяни FUMO или Touhou Project. НИКАКИХ загадок.\n" +
                             "Сейчас ты {teacher}. Ответь на вопрос: {request.Message}";
            }

            // Формируем финальный промпт, заменяя {teacher} и {request.Message}
            string prompt = basePrompt.Replace("{teacher}", teacher).Replace("{request.Message}", request.Message);

            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            await foreach (var chunk in _ollama.Completions.GenerateCompletionAsync(
                model: "llama3.2-vision:latest",
                prompt: prompt,
                stream: true,
                context: context))
            {
                await Response.WriteAsync($"data: {chunk.Response}\n\n");
                await Response.Body.FlushAsync();
                teacherContexts[teacher] = chunk.Context;
            }
        }

        private string DetermineTeacher(string message)
        {
            var keywords = new Dictionary<string, List<string>>
            {
                {"Шамина", new List<string> {"английский", "язык", "english", "language"}},
                {"Смирнов", new List<string> {"база данных", "SQL", "database"}},
                {"Михайлов", new List<string> {"ОС", "системное администрирование", "operating system", "admin"}},
                {"Логинов", new List<string> {"философия", "philosophy", "смысл", "meaning", "мотоцикл"}},
                {"Самарина", new List<string> {"религия", "вера", "religion", "faith"}},
                {"Ларионов", new List<string> {"докер", "docker", "контейнер", "container", "Fumo", "Touhou"}}
            };

            var lowerMessage = message.ToLower();
            foreach (var kvp in keywords)
            {
                if (kvp.Value.Any(keyword => lowerMessage.Contains(keyword.ToLower())))
                {
                    return kvp.Key;
                }
            }
            return "general";
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
        public string? UserId { get; set; }
        public string? Teacher { get; set; }
    }
}