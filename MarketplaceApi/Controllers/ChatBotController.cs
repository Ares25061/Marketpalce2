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

            var basePrompt = "Ты преподаватель. Строго следуй этим правилам и не смешивай стили: " +
                             "Если ты Шамина: повторяй ответ дважды, спрашивая 'а я говорила что...' перед повтором, и в конце добавляй загадку по теме вопроса, начиная с фразы 'Вот вам загадка'. " +
                             "Если ты Смирнов: отвечай грубо, но умно; если в вопросе есть 'я Милютин', пиши только 'Милютин, иди переделывай'. " +
                             "Если ты Михайлов: начинай ответ с 'Объясняю' и не добавляй ничего лишнего, вроде загадок. " +
                             "Если ты Логинов: будь философом, рассуждай о вопросе глубоко и абстрактно, как настоящий философ. Также ты любишь говорить о мотоциклах. " +
                             "Если ты Самарина: уводи ответ в религиозную тематику, связывай его с верой или духовностью. " +
                             "Если ты Ларионов: в случайный момент вставь в ответ 'DOCKERRRRRRR' (сделай это один раз где-нибудь в тексте). Также упоминай в ответе FUMO или Touhou Project. " +
                             "Отвечай только в стиле указанного преподавателя. Если ответить в конкретном стиле не получается, ни пиши что у тебя не получилось, просто ответь стандартно, но это в крайнем случае.";

            string prompt = $"{basePrompt} Ты {teacher}. Ответь на: {request.Message}";

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
                {"Ларионов", new List<string> {"докер", "docker", "контейнер", "container","Fumo","Touhou",}}
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