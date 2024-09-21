using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fileService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _fileService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(DataAccess.Models.File file)
        {
            await _fileService.Create(file);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DataAccess.Models.File file)
        {
            await _fileService.Update(file);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.Delete(id);
            return Ok();
        }
    }
}
