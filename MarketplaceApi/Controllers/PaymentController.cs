﻿using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Получение информации о всех картах
        /// </summary>
        /// <returns></returns>

        // GET api/<PaymentController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _paymentService.GetAll();
            return Ok(Dto.Adapt<List<GetPaymentResponse>>());
        }

        /// <summary>
        /// Получение информации о карте по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<PaymentController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _paymentService.GetById(id);
            return Ok(Dto.Adapt<GetPaymentResponse>());
        }

        /// <summary>
        /// Создание новой карты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "CardNumber": "string",
        ///       "Cvv": "string",
        ///       "ExpressionDate": "2024-09-20T17:59:37.667Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="payment">Карта</param>
        /// <returns></returns>

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<IActionResult> Add(CreatePaymentRequest payment)
        {
            var Dto = payment.Adapt<Payment>();
            await _paymentService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о карте
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "PaymentId": 1,
        ///       "CardNumber": "string",
        ///       "Cvv": "string",
        ///       "ExpressionDate": "2024-09-20T18:00:15.835Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="payment">Карта</param>
        /// <returns></returns>

        // PUT api/<PaymentController>
        [HttpPut]
        public async Task<IActionResult> Update(GetPaymentResponse payment)
        {
            var Dto = payment.Adapt<Payment>();
            await _paymentService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление карты
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<PaymentController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.Delete(id);
            return Ok();
        }
    }
}