using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение информации о всех пользователях
        /// </summary>
        /// <returns></returns>

        // GET api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _userService.GetAll();
            return Ok(Dto.Adapt<List<GetUserResponse>>());
        }

        /// <summary>
        /// Получение информации о пользователе по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<UserController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _userService.GetById(id);
            return Ok(Dto.Adapt<GetUserResponse>());
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "Username": "string",
        ///        "Email" : "email@gmail.com",
        ///        "Password" : "!Pa$$word123@",
        ///        "FirstName" : "Иван",
        ///        "LastName" : "Иванов"
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest user)
        {
            var Dto = user.Adapt<User>();
            await _userService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о пользователе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "UserId": 1,
        ///       "Username": "string",
        ///       "Email" : "email@gmail.com",
        ///       "Password" : "!Pa$$word123@",
        ///       "FirstName" : "Иван",
        ///       "LastName" : "Иванов"
        ///       "IsActive" : true,
        ///       "IsDeleted" : false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedBy": 1,
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>

        // PUT api/<UserController>
        [HttpPut]
        public async Task<IActionResult> Update(GetUserResponse user)
        {
            var Dto = user.Adapt<User>();
            await _userService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<UserController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}