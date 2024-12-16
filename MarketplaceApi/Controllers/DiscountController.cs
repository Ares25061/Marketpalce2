using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Discount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// Получение информации о всех скидках
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<DiscountController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _discountService.GetAll();
            return Ok(Dto.Adapt<List<GetDiscountResponse>>());
        }

        /// <summary>
        /// Получение информации о скидке по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<DiscountController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _discountService.GetById(id);
            return Ok(Dto.Adapt<List<GetDiscountResponse>>());
        }

        /// <summary>
        /// Создание новой скидки
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "DiscountCode": "string",
        ///       "DiscountPercentage": 90.75,
        ///       "StartDate": "2024-09-19T14:34:41.968Z",
        ///       "EndDate": 2024-09-19T14:05:14.947Z",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="discount">Скидка</param>
        /// <returns></returns>

        // POST api/<DiscountController>
        [Authorize(roles: 1)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateDiscountRequest discount)
        {
            var Dto = discount.Adapt<Discount>();
            await _discountService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о скидке
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "DiscountId": 1,
        ///       "DiscountCode": "string",
        ///       "DiscountPercentage": 90.75,
        ///       "StartDate": "2024-09-19T14:34:41.968Z",
        ///       "EndDate": 2024-09-19T14:05:14.947Z",
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="discount">Скидка</param>
        /// <returns></returns>

        // PUT api/<DiscountController>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetDiscountResponse discount)
        {
            var Dto = discount.Adapt<Discount>();
            await _discountService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление скидки
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<DiscountController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _discountService.Delete(id);
            return Ok();
        }
    }
}