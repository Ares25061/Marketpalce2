using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Получение информации о всех сообщениях
        /// </summary>
        /// <returns></returns>

        // GET api/<MessageController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _messageService.GetAll();
            return Ok(Dto.Adapt<List<GetMessageResponse>>());
        }

        /// <summary>
        /// Получение информации о сообщении по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<MessageController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _messageService.GetById(id);
            return Ok(Dto.Adapt<GetMessageResponse>());
        }

        /// <summary>
        /// Создание нового сообщения
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ChatId": 1,
        ///       "UserId": 1,
        ///       "MessageContent": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>

        // POST api/<MessageController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateMessageRequest message)
        {
            var Dto = message.Adapt<Message>();
            await _messageService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о сообщении
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "MessageId": 1,
        ///       "ChatId": 1,
        ///       "UserId": 1,
        ///       "MessageContent": "string",
        ///       "IsRead": false,
        ///       "IsDeleted": false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>

        // PUT api/<MessageController>
        [HttpPut]
        public async Task<IActionResult> Update(GetMessageResponse message)
        {
            var Dto = message.Adapt<Message>();
            await _messageService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<MessageController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.Delete(id);
            return Ok();
        }
    }
}