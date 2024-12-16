using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.SearchHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController : BaseController
    {
        private ISearchHistoryService _searchHistoryService;
        public SearchHistoryController(ISearchHistoryService searchHistoryService)
        {
            _searchHistoryService = searchHistoryService;
        }

        /// <summary>
        /// Получение информации о историях поиска
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<SearchHistoryController>
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _searchHistoryService.GetAll();
            return Ok(Dto.Adapt<List<GetSearchHistoryResponse>>());
        }

        /// <summary>
        /// Получение информации о истории поиска по id
        /// </summary>
        /// <param name="id">ID</param>

        /// <returns></returns>

        // GET api/<SearchHistoryController>
        [Authorize(roles: 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _searchHistoryService.GetById(id);
            return Ok(Dto.Adapt<GetSearchHistoryResponse>());
        }

        /// <summary>
        /// Создание нового история поиска
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "UserId": 1,
        ///       "SearchTerm": "string",
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="searchhistory">История поиска</param>
        /// <returns></returns>

        // POST api/<SearchHistoryController>
        [Authorize(roles: 1)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateSearchHistoryRequest searchhistory)
        {
            var Dto = searchhistory.Adapt<SearchHistory>();
            await _searchHistoryService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о истории поиска
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "SearchHistoryId": 1,
        ///       "UserId": 1,
        ///       "SearchTerm": "string",
        ///       "SearchDate": "2024-09-19T14:05:14.947Z",
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z",
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="searchhistory">История поиска</param>
        /// <returns></returns>

        // PUT api/<SearchHistoryController>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetSearchHistoryResponse searchhistory)
        {
            var Dto = searchhistory.Adapt<SearchHistory>();
            await _searchHistoryService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление истории поиска
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<SearchHistoryController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _searchHistoryService.Delete(id);
            return Ok();
        }
    }
}