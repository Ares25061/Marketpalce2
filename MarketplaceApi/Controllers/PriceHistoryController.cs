using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.PriceHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private IPriceHistoryService _priceHistoryService;
        public PriceHistoryController(IPriceHistoryService PricehistoryService)
        {
            _priceHistoryService = PricehistoryService;
        }

        /// <summary>
        /// Получение информации о всех изменениях истории цен
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<PriceHistoryController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _priceHistoryService.GetAll();
            return Ok(Dto.Adapt<List<GetPriceHistoryResponse>>());
        }

        /// <summary>
        /// Получение информации о изменении истории цен по id
        /// </summary>
        /// <param name="id">ID</param>

        /// <returns></returns>

        // GET api/<PriceHistoryController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _priceHistoryService.GetById(id);
            return Ok(Dto.Adapt<GetPriceHistoryResponse>());
        }

        /// <summary>
        /// Создание нового изменения историй цен
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ProductId": 0,
        ///       "Price": 93.75,
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="pricehistory">Изменение истории цен</param>
        /// <returns></returns>

        // POST api/<PriceHistoryController>
        [HttpPost]
        public async Task<IActionResult> Add(CreatePriceHistoryRequest pricehistory)
        {
            var Dto = pricehistory.Adapt<PriceHistory>();
            await _priceHistoryService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о изменении историй цен
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "Pricehstoryid": 1,
        ///       "ProductId": 0,
        ///       "Price": 93.75,
        ///       "ChangeDate": "2024-09-19T14:05:14.947Z",
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z",
        ///     }
        ///
        /// </remarks>
        /// <param name="pricehistory">Изменение истории цен</param>
        /// <returns></returns>

        // PUT api/<PriceHistoryController>
        [HttpPut]
        public async Task<IActionResult> Update(GetPriceHistoryResponse pricehistory)
        {
            var Dto = pricehistory.Adapt<PriceHistory>();
            await _priceHistoryService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление изменения историй цен
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<PriceHistoryController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _priceHistoryService.Delete(id);
            return Ok();
        }
    }
}