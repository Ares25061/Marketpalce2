using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _filepermissionService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _filepermissionService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(FilePermission filepermission)
        {
            await _filepermissionService.Create(filepermission);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(FilePermission filepermission)
        {
            await _filepermissionService.Update(filepermission);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _filepermissionService.Delete(id);
            return Ok();
        }
    }
}
