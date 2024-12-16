using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.UserDiscount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDiscountController : BaseController
    {
        private IUserDiscountService _userDiscountService;
        public UserDiscountController(IUserDiscountService userDiscountService)
        {
            _userDiscountService = userDiscountService;
        }

        /// <summary>
        /// Получение информации о всех скидках пользователя
        /// </summary>
        /// <returns></returns>

        // GET api/<UserDiscountController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _userDiscountService.GetAll();
            return Ok(Dto.Adapt<List<GetUserDiscountResponse>>());
        }

        /// <summary>
        /// Получение информации о скидках пользователя по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<UserDiscountController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _userDiscountService.GetById(id);
            return Ok(Dto.Adapt<GetUserDiscountResponse>());
        }

        /// <summary>
        /// Добавление новых скидок пользователю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "UserId" : 1,
        ///        "DiscountId" : 1
        ///     }
        ///
        /// </remarks>
        /// <param name="userdiscount">Скидки пользователя</param>
        /// <returns></returns>

        // POST api/<UserDiscountController>
        [Authorize(roles: 1)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserDiscountRequest userdiscount)
        {
            var Dto = userdiscount.Adapt<UserDiscount>();
            await _userDiscountService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о скидках пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "UserDiscountId" : 1,
        ///       "UserId" : 1,
        ///       "DiscountId" : 1
        ///     }
        ///
        /// </remarks>
        /// <param name="userdiscount">Скидки пользователя</param>
        /// <returns></returns>

        // PUT api/<UserDiscountController>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetUserDiscountResponse userdiscount)
        {
            var Dto = userdiscount.Adapt<UserDiscount>();
            await _userDiscountService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление скидок пользователя
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<UserDiscountController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userDiscountService.Delete(id);
            return Ok();
        }
    }
}