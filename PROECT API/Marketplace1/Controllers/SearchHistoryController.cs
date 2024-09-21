using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {
        private ISearchHistoryService _searchHistoryService;
        public SearchHistoryController(ISearchHistoryService searchHistoryService)
        {
            _searchHistoryService = searchHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _searchHistoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _searchHistoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(SearchHistory searchhistory)
        {
            await _searchHistoryService.Create(searchhistory);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(SearchHistory searchhistory)
        {
            await _searchHistoryService.Update(searchhistory);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _searchHistoryService.Delete(id);
            return Ok();
        }
    }
}
