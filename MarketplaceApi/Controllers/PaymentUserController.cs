using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.PaymentUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentUserController : BaseController
    {
        private IPaymentUserService _paymentUserService;
        public PaymentUserController(IPaymentUserService paymentUserService)
        {
            _paymentUserService = paymentUserService;
        }

        /// <summary>
        /// Получение информации о всех картах пользователя
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<PaymentUserController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _paymentUserService.GetAll();
            return Ok(Dto.Adapt<List<GetPaymentUserResponse>>());
        }

        /// <summary>
        /// Получение информации о картах пользователя по id
        /// </summary>
        /// <param name="paymentId">userID</param>
        /// <param name="userId">paymentID</param>
        /// <returns></returns>

        // GET api/<PaymentUserController>
        [Authorize(roles: 1)]
        [HttpGet("{paymentId}/{userId}")]
        public async Task<IActionResult> GetById(int paymentId, int userId)
        {
            var Dto = await _paymentUserService.GetById(paymentId, userId);
            return Ok(Dto.Adapt<GetPaymentUserResponse>());
        }

        /// <summary>
        /// Добавление новых карт пользователю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "PaymentId": 1,
        ///       "UserId": 1,
        ///       "IsActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="paymentuser">Карты пользователя</param>
        /// <returns></returns>

        // POST api/<PaymentUserController>
        [HttpPost]
        public async Task<IActionResult> Add(CreatePaymentUserRequest paymentuser)
        {
            var Dto = paymentuser.Adapt<PaymentUser>();
            if (Dto.UserId != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _paymentUserService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о картах пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "PaymentId": 1,
        ///       "UserId": 1,
        ///       "IsActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="paymentuser">Карты пользователя</param>
        /// <returns></returns>

        // PUT api/<PaymentUserController>
        [HttpPut]
        public async Task<IActionResult> Update(GetPaymentUserResponse paymentuser)
        {
            var Dto = paymentuser.Adapt<PaymentUser>();
            if (Dto.UserId != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _paymentUserService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление карт пользователя
        /// </summary>
        /// <param name="paymentId">userID</param>
        /// <param name="userId">paymentID</param>
        /// <returns></returns>

        // DELETE api/<PaymentUserController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int paymentId, int userId)
        {
            await _paymentUserService.Delete(paymentId, userId);
            return Ok();
        }
    }
}