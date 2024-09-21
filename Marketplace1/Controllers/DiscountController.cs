using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _discountService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Discount discount)
        {
            await _discountService.Create(discount);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Discount discount)
        {
            await _discountService.Update(discount);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _discountService.Delete(id);
            return Ok();
        }
    }
}
