using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private IPriceHistoryService _priceHistoryService;
        public PriceHistoryController(IPriceHistoryService pricehistoryService)
        {
            _priceHistoryService = pricehistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _priceHistoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _priceHistoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(PriceHistory pricehistory)
        {
            await _priceHistoryService.Create(pricehistory);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PriceHistory pricehistory)
        {
            await _priceHistoryService.Update(pricehistory);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _priceHistoryService.Delete(id);
            return Ok();
        }
    }
}
