using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reviewService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _reviewService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Review review)
        {
            await _reviewService.Create(review);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Review review)
        {
            await _reviewService.Update(review);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.Delete(id);
            return Ok();
        }
    }
}
