using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDiscountController : ControllerBase
    {
        private IUserDiscountService _userDiscountService;
        public UserDiscountController(IUserDiscountService userDiscountService)
        {
            _userDiscountService = userDiscountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userDiscountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userDiscountService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDiscount userdiscount)
        {
            await _userDiscountService.Create(userdiscount);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDiscount userdiscount)
        {
            await _userDiscountService.Update(userdiscount);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userDiscountService.Delete(id);
            return Ok();
        }
    }
}
