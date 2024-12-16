using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Получение информации о всех уведомлениях
        /// </summary>
        /// <returns></returns>

        // GET api/<NotificationController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _notificationService.GetAll();
            return Ok(Dto.Adapt<List<GetNotificationResponse>>());
        }

        /// <summary>
        /// Получение информации о уведомлении по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<NotificationController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _notificationService.GetById(id);
            return Ok(Dto.Adapt<GetNotificationResponse>());
        }

        /// <summary>
        /// Создание нового уведомления
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "UserId": 1,
        ///       "NotificationType": "string",
        ///       "Message": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="notification">Уведомление</param>
        /// <returns></returns>

        // POST api/<NotificationController>
        [Authorize(roles: 1)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateNotificationRequest notification)
        {
            var Dto = notification.Adapt<Notification>();
            await _notificationService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации об уведомлении
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "NotificationId": 1,
        ///       "UserId": 1,
        ///       "NotificationType": "string",
        ///       "Message": "string",
        ///       "IsRead": "false",
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="notification">Уведомление</param>
        /// <returns></returns>

        // PUT api/<NotificationController>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetNotificationResponse notification)
        {
            var Dto = notification.Adapt<Notification>();
            await _notificationService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление уведомления
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<NotificationController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.Delete(id);
            return Ok();
        }
    }
}