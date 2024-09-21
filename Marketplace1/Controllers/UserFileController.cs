using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userFileService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userFileService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserFile userfile)
        {
            await _userFileService.Create(userfile);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserFile userfile)
        {
            await _userFileService.Update(userfile);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userFileService.Delete(id);
            return Ok();
        }
    }
}
