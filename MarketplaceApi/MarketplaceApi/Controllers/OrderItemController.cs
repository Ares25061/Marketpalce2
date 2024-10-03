﻿using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.OrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Получение информации о всех товарах в заказе
        /// </summary>
        /// <returns></returns>

        // GET api/<OrderItemController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _orderItemService.GetAll();
            return Ok(Dto.Adapt<List<GetOrderItemResponse>>());
        }

        /// <summary>
        /// Получение информации о товарах в заказе по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<OrderItemController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _orderItemService.GetById(id);
            return Ok(Dto.Adapt<GetOrderItemResponse>());
        }

        /// <summary>
        /// Добавление новых товаров в заказ
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "OrderId": 1,
        ///       "ProductId": 1,
        ///       "Quantity": 1
        ///       "Price": 93.75
        ///     }
        ///
        /// </remarks>
        /// <param name="orderitem">Товары в заказе</param>
        /// <returns></returns>

        // POST api/<OrderItemController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderItemRequest orderitem)
        {
            var Dto = orderitem.Adapt<OrderItem>();
            await _orderItemService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о товарах в заказе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "OrderItemId": 1,
        ///       "OrderId": 1,
        ///       "ProductId": 1,
        ///       "Quantity": 1
        ///       "Price": 93.75,
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
        /// <param name="orderitem">Товары в заказе</param>
        /// <returns></returns>

        // PUT api/<OrderItemController>
        [HttpPut]
        public async Task<IActionResult> Update(GetOrderItemResponse orderitem)
        {
            var Dto = orderitem.Adapt<OrderItem>();
            await _orderItemService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление товаров из заказа
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<OrderItemController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderItemService.Delete(id);
            return Ok();
        }
    }
}