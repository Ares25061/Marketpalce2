using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderItemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _orderItemService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderItem orderitem)
        {
            await _orderItemService.Create(orderitem);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderItem orderitem)
        {
            await _orderItemService.Update(orderitem);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderItemService.Delete(id);
            return Ok();
        }
    }
}
