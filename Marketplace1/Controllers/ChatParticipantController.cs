using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatParticipantController : ControllerBase
    {
        private IChatParticipantService _chatparticipantService;
        public ChatParticipantController(IChatParticipantService chatparticipantService)
        {
            _chatparticipantService = chatparticipantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _chatparticipantService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _chatparticipantService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChatParticipant chatparticipant)
        {
            await _chatparticipantService.Create(chatparticipant);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ChatParticipant chatparticipant)
        {
            await _chatparticipantService.Update(chatparticipant);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatparticipantService.Delete(id);
            return Ok();
        }
    }
}
