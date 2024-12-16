using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получение информации о всех заказах
        /// </summary>
        /// <returns></returns>

        // GET api/<OrderController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _orderService.GetAll();
            return Ok(Dto.Adapt<List<GetOrderResponse>>());
        }

        /// <summary>
        /// Получение информации о заказе по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<OrderController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _orderService.GetById(id);
            return Ok(Dto.Adapt<GetOrderResponse>());
        }

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "BuyerId": 1,
        ///       "OrderDate": "2024-09-19T14:05:14.947Z",
        ///       "Status": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="order">Заказ</param>
        /// <returns></returns>

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderRequest order)
        {
            var Dto = order.Adapt<Order>();
            Dto.ModifiedBy = Dto.CreatedBy;
            if (Dto.CreatedBy != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _orderService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о заказе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "OrderId": 1,
        ///       "BuyerId": 1,
        ///       "OrderDate": "2024-09-19T14:05:14.947Z",
        ///       "Status": "string",
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedBy": 1,
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="order">Заказ</param>
        /// <returns></returns>

        // PUT api/<OrderController>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetOrderResponse order)
        {
            var Dto = order.Adapt<Order>();
            await _orderService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<OrderController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Delete(id);
            return Ok();
        }
    }
}