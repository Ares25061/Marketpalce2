using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Получение информации о всех изображениях
        /// </summary>
        /// <returns></returns>

        // GET api/<ImageController>
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
            await _imageService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление изображения
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ImageController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageService.Delete(id);
            return Ok();
        }
    }
}