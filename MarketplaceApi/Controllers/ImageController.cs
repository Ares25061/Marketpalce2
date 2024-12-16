using BusinessLogic.Authorization;
using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        private IImageService _imageService;
        private IAccountService _accountService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Получение информации о всех изображениях
        /// </summary>
        /// <returns></returns>

        // GET api/<ImageController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _imageService.GetAll();
            return Ok(Dto.Adapt<List<GetImageResponse>>());
        }

        /// <summary>
        /// Получение информации о изображении по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // GET api/<ImageController>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _imageService.GetById(id);
            return Ok(Dto.Adapt<GetImageResponse>());
        }

        /// <summary>
        /// Создание нового изображения
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ProductId": 1,
        ///       "ImageUrl": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="image">Изображение</param>
        /// <returns></returns>

        // POST api/<ImageController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateImageRequest image)
        {
            var Dto = image.Adapt<Image>();
            Dto.ModifiedBy = Dto.CreatedBy;
            if (Dto.CreatedBy != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _imageService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о изображении
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "ImageId": 1,
        ///       "ProductId": 1,
        ///       "ImageUrl": "string",
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
        /// <param name="image">Изображение</param>
        /// <returns></returns>

        // PUT api/<ImageController>
        [HttpPut]
        public async Task<IActionResult> Update(GetImageResponse image)
        {
            var Dto = image.Adapt<Image>();
            if (Dto.CreatedBy != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _imageService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление изображения
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ImageController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageService.Delete(id);
            return Ok();
        }
    }
}