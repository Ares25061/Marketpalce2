using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Marketplace1.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _imageService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _imageService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(DataAccess.Models.Image image)
        {
            await _imageService.Create(image);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DataAccess.Models.Image image)
        {
            await _imageService.Update(image);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _imageService.Delete(id);
            return Ok();
        }
    }
}
