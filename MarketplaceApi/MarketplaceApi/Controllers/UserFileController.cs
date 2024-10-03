using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.UserFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFileController : ControllerBase
    {
        private IUserFileService _userFileService;
        public UserFileController(IUserFileService userFileService)
        {
            _userFileService = userFileService;
        }

        /// <summary>
        /// Получение информации о всех файлах пользователя
        /// </summary>
        /// <returns></returns>

        // GET api/<UserFileController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _userFileService.GetAll();
            return Ok(Dto.Adapt<List<GetUserFileResponse>>());
        }

        /// <summary>
        /// Получение информации о файлах пользователя по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<UserFileController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _userFileService.GetById(id);
            return Ok(Dto.Adapt<GetUserFileResponse>());
        }

        /// <summary>
        /// Добавление новых файлов пользователю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "UserId": 1,
        ///       "FileId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="userfile">Файлы пользователя</param>
        /// <returns></returns>

        // POST api/<UserFileController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserFileRequest userfile)
        {
            var Dto = userfile.Adapt<UserFile>();
            await _userFileService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о файлах пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "UserFileId": 1,
        ///       "UserId": 1,
        ///       "FileId": 1,
        ///       "IsDeleted" : false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="userfile">Файлы пользователя</param>
        /// <returns></returns>

        // PUT api/<UserFileController>
        [HttpPut]
        public async Task<IActionResult> Update(GetUserFileResponse userfile)
        {
            var Dto = userfile.Adapt<UserFile>();
            await _userFileService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление файлов пользователя
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<UserFileController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userFileService.Delete(id);
            return Ok();
        }
    }
}