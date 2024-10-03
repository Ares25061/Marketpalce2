using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.FilePermission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilePermissionController : ControllerBase
    {
        private IFilePermissionService _filepermissionService;
        public FilePermissionController(IFilePermissionService filepermissionService)
        {
            _filepermissionService = filepermissionService;
        }

        /// <summary>
        /// Получение информации о всех правах доступа к файлам
        /// </summary>
        /// <returns></returns>

        // GET api/<FilePermissionController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _filepermissionService.GetAll();
            return Ok(Dto.Adapt<List<GetFilePermissionResponse>>());
        }

        /// <summary>
        /// Получение информации о праве доступа к файлу по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<FilePermissionController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _filepermissionService.GetById(id);
            return Ok(Dto.Adapt<List<GetFilePermissionResponse>>());
        }

        /// <summary>
        /// Добавление нового права доступа к файлу
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "FileId": 1,
        ///       "UserId": 1,
        ///       "PermissionLevel": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="filepermission">Право доступа</param>
        /// <returns></returns>

        // POST api/<FilePermissionController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateFilePermissionRequest filepermission)
        {
            var Dto = filepermission.Adapt<FilePermission>();
            await _filepermissionService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о праве доступа к файлу
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "FilePermissionId": 1,
        ///       "FileId": 1,
        ///       "UserId": 1,
        ///       "PermissionLevel": "string",
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
        /// <param name="filepermission">Право доступа</param>
        /// <returns></returns>

        // PUT api/<FilePermissionController>
        [HttpPut]
        public async Task<IActionResult> Update(GetFilePermissionResponse filepermission)
        {
            var Dto = filepermission.Adapt<FilePermission>();
            await _filepermissionService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление права доступа к файлу
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<FilePermissionController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _filepermissionService.Delete(id);
            return Ok();
        }
    }
}