using BusinessLogic.Authorization;
using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private IFileService _fileService;
        private IAccountService _accountService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Получение информации о всех файлах
        /// </summary>
        /// <returns></returns>

        // GET api/<FileController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _fileService.GetAll();
            return Ok(Dto.Adapt<List<GetFileResponse>>());
        }

        /// <summary>
        /// Получение информации о файле по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<FileController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _fileService.GetById(id);
            return Ok(Dto.Adapt<List<GetFileResponse>>());
        }

        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "FileName": "string",
        ///       "FilePath": "string",
        ///       "FileSize": 1,
        ///       "FileType": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="file">Файл</param>
        /// <returns></returns>

        // POST api/<FileController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateFileRequest file)
        {

            var Dto = file.Adapt<Domain.Models.File>();
            Dto.ModifiedBy = Dto.CreatedBy;
            if (Dto.CreatedBy != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _fileService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о файле
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "FileId": 1,
        ///       "FileName": "string",
        ///       "FilePath": "string",
        ///       "FileSize": 1,
        ///       "FileType": "string",
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
        /// <param name="file">Файл</param>
        /// <returns></returns>

        // PUT api/<FileController>
        [HttpPut]
        public async Task<IActionResult> Update(GetFileResponse file)
        {
            var Dto = file.Adapt<Domain.Models.File>();
            if (Dto.CreatedBy != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _fileService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<FileController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.Delete(id);
            return Ok();
        }
    }
}