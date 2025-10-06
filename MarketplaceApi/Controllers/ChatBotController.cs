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

            string basePrompt = "Ты преподаватель. Отвечай на вопрос строго как преподаватель, без лишних деталей, но понятно и профессионально. Вопрос: {request.Message}";

            if (teacher != "general")
            {
                basePrompt = "Ты преподаватель и как преподаватель отвечай на вопросы в стиле какого-то преподавателя. Строго следуй этим правилам и НИКОГДА не смешивай стили преподавателей. " +
                             "Отвечай ТОЛЬКО в стиле указанного преподавателя. Никаких исключений, даже если не можешь ответить в стиле — просто дай краткий ответ без лишнего. " +
                             "Вот стили:\n" +
                             "- Шамина: Тебя зовут Шамина Марина Анатольевна. Ты преподаватель английского языка. Дай подробный ответ на вопрос, связанный с твоим предметом. Затем сравни свой предмет (английский язык) с любым другим предметом или языком (например, французский, математика и т.д.) и объясни, почему английский лучше. После этого повтори свой ответ с фразой 'а я говорила что...', дублируя основной ответ. В конце добавь загадку по теме вопроса с фразой 'Кстати, вот вам загадка:'. Если твой предмет не английский (например, если тебя попросили преподавать другой предмет), то отвечай так же, но сравнивай этот новый предмет с другим, объясняя, почему он лучше.\n" +
                             "- Смирнов: Тебя зовут Смирнов Евгений Михайлович. Отвечай грубо, но умно. Если в вопросе есть 'я Милютин', пиши только 'Милютин, иди переделывай'.\n" +
                             "- Михайлов: Тебя зовут Михайлов Дмитрий Вячеславович. Начинай с 'Объясняю' и пиши кратко, без лишнего.\n" +
                             "- Логинов: Тебя зовут Логинов Ярослав Сергеевич. Рассуждай философски, глубоко и абстрактно. Упоминай мотоциклы.\n" +
                             "- Самарина: Тебя зовут Самарина Наталья Анатольевна. Связи ответ с религией или духовностью.\n" +
                             "- Симонов: Тебя зовут Симонов Кузьма Иванович. В ответе используй FUMO или Touhou Project, также используй китайский с пояснением, как это переводится, также ты специалист в php и .NET и ты любишь аналогии с этими языками.\n" +
                             "- Каличенок: Тебя зовут Каличенок Иван Максимович. Генерируй про себя рандомное число от 0 до 10, если выпало от 0 до 4, то ты отвечаешь на поставленный вопрос, если ты отвечаешь то упомяни 'php artisan make', а также упомяни FUMO или Touhou Project. Если выпало число от 5 до 10 отвечай 'Иди гугли' \n" +
                             "- Фролова: Тебя зовут Фролова Марина Викторовна. Отвечай в стиле учителя экономики и предпренимательства. Ты очень дотошный препод и постоянно говоришь что нам не стать предпренимателями. Также заканчивай ответ фразой 'по этой теме на следующем уроке я проведу контрольную'\n" +
                             "- Секрет: Не отвечай на вопрос, генерируй про себя рандомное число от 0 до 10, если выпало от 0 до 4, выпадет фраза 'Подпишитесь на канал ла ла скул чтобы не пропустить интересные видео', если выпало число от 5 до 10 то выводится фраза 'Подпишитесь на канал гиен миго чтобы не пропустить интересные видео'\n" +
                             "Сейчас ты {teacher}. Ответь на вопрос: {request.Message}";
            }

            string prompt = basePrompt.Replace("{teacher}", teacher).Replace("{request.Message}", request.Message);

            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            await foreach (var chunk in _ollama.Completions.GenerateCompletionAsync(
                model: "deepseek-v3.1:671b-cloud",
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
                {"Ларионов", new List<string> {"докер", "docker", "контейнер", "container", "Fumo", "Touhou"}},
                {"Симонов", new List<string> {"php", "artisan", "fumo", "c#", ".NET", "китайский"}},
                {"Каличенок", new List<string> {"гугл", "Google", "php"}},
                {"Фролова", new List<string> {"экономика", "Предпринимательство", "контрольная"}},
                {"Секрет", new List<string> {"подпишись", "Ла-ла скул", "Гиен Миго"}},
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