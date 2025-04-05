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
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Ошибка: сообщение не может быть пустым.");
            }

            var userId = request.UserId ?? "anonymous";
            string teacher = request.Teacher;

            // Если преподаватель не указан, определяем его по ключевым словам
            if (string.IsNullOrEmpty(teacher))
            {
                teacher = DetermineTeacher(request.Message);
            }

            // Получаем контексты для пользователя
            if (!_userContexts.TryGetValue(userId, out var teacherContexts))
            {
                teacherContexts = new Dictionary<string, IList<long>>();
                _userContexts[userId] = teacherContexts;
            }

            // Получаем контекст для конкретного преподавателя
            if (!teacherContexts.TryGetValue(teacher, out var context))
            {
                context = null; // Новый контекст, если преподаватель впервые
            }

            var basePrompt = "Ты преподаватель. Следуй этим правилам: " +
                            "Если ты Шамина, то повторяй свои предложения дважды, только при повтории спрашивай, типо а я уже говорила что...., а также любишь загадывать загадки. В конце всегда загадывай загадку по теме вопроса. Начинай загадку с фразы вот вам загадка. " +
                            "Если ты Смирнов, то отвечай как быдло, но умно; если в моем вопросе есть что то типо я Милютин, то говори просто переделывай. " +
                            "Если ты Михайлов, то всегда начинай со слова 'Объясняю'. " +
                            "Если не указано, кто ты, то отвечай как обычно, но если речь о английском языке, то ты Шамина, " +
                            "если речь связана с базами данных, то Смирнов, а если речь связана с ОС и системным администрированием, то ты Михайлов.";

            string prompt;
            if (teacher != "general")
            {
                prompt = $"{basePrompt} Ты {teacher}. Ответь на: {request.Message}";
            }
            else
            {
                prompt = $"{basePrompt} Ответь на: {request.Message}";
            }

            var response = await _ollama.Completions.GenerateCompletionAsync(
                model: "llama3.2-vision:latest",
                prompt: prompt,
                stream: false,
                context: context,
                images: string.IsNullOrEmpty(request.ImageBase64) ? null : new List<string> { request.ImageBase64 }
            );

            // Обновляем контекст для этого преподавателя
            teacherContexts[teacher] = response.Context;

            return Ok(response.Response);
        }

        // Метод для определения преподавателя по сообщению
        private string DetermineTeacher(string message)
        {
            var keywords = new Dictionary<string, List<string>>
            {
                {"Шамина", new List<string> {"английский", "язык", "english", "language"}},
                {"Смирнов", new List<string> {"база данных", "SQL", "database"}},
                {"Михайлов", new List<string> {"ОС", "системное администрирование", "operating system", "admin"}}
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
        public string? ImageBase64 { get; set; }
    }
}