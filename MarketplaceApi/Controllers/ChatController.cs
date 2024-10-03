﻿using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Получение информации о всех чатах
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<ChatController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _chatService.GetAll();
            return Ok(Dto.Adapt<List<GetChatResponse>>());
        }

        /// <summary>
        /// Получение информации о чате по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<ChatController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _chatService.GetById(id);
            return Ok(Dto.Adapt<List<GetChatResponse>>());
        }

        /// <summary>
        /// Создание нового чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "Title": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="chat">Автор</param>
        /// <returns></returns>

        // POST api/<ChatController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateChatRequest chat)
        {
            var Dto = chat.Adapt<Chat>();
            await _chatService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о чате
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "ChatId": 1,
        ///       "Title": "string",
        ///       "IsDeleted": false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedBy": 1,
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="chat">Автор</param>
        /// <returns></returns>

        // PUT api/<ChatController>
        [HttpPut]
        public async Task<IActionResult> Update(GetChatResponse chat)
        {
            var Dto = chat.Adapt<Chat>();
            await _chatService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление чата
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ChatController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatService.Delete(id);
            return Ok();
        }
    }
}