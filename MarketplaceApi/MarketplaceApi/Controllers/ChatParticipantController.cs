﻿using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.ChatParticipant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatParticipantController : ControllerBase
    {
        private IChatParticipantService _chatparticipantService;
        public ChatParticipantController(IChatParticipantService chatparticipantService)
        {
            _chatparticipantService = chatparticipantService;
        }

        /// <summary>
        /// Получение информации о всех пользователях чатов
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<ChatParticipantController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _chatparticipantService.GetAll();
            return Ok(Dto.Adapt<List<GetChatParticipantResponse>>());
        }

        /// <summary>
        /// Получение информации о пользователях в чате по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>

        // GET api/<ChatParticipantController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _chatparticipantService.GetById(id);
            return Ok(Dto.Adapt<List<GetChatParticipantResponse>>());
        }

        /// <summary>
        /// Добавление пользователя в чат
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ChatId": 1,
        ///       "UserId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="chatparticipant">Пользователь чата</param>
        /// <returns></returns>

        // POST api/<ChatParticipantController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateChatParticipantRequest chatparticipant)
        {
            var Dto = chatparticipant.Adapt<ChatParticipant>();
            await _chatparticipantService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о пользователе чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "ChatParticipantId": 1,
        ///       "ChatId": 1,
        ///       "UserId": 1,
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="chatparticipant">Пользователь чата</param>
        /// <returns></returns>

        // PUT api/<ChatParticipantController>
        [HttpPut]
        public async Task<IActionResult> Update(GetChatParticipantResponse chatparticipant)
        {
            var Dto = chatparticipant.Adapt<ChatParticipant>();
            await _chatparticipantService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя из чата
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ChatParticipantController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatparticipantService.Delete(id);
            return Ok();
        }
    }
}