using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Получение информации о всех отзывах
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<ReviewController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _reviewService.GetAll();
            return Ok(Dto.Adapt<List<GetReviewResponse>>());
        }

        /// <summary>
        /// Получение информации о отзыве по id
        /// </summary>
        /// <param name="id">ID</param>

        /// <returns></returns>

        // GET api/<ReviewController>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _reviewService.GetById(id);
            return Ok(Dto.Adapt<GetReviewResponse>());
        }

        /// <summary>
        /// Создание нового отзыва
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ProductId": 1,
        ///       "UserId": 1,
        ///       "Rating": 1,
        ///       "Comment": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="review">Отзыв</param>
        /// <returns></returns>

        // POST api/<ReviewController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateReviewRequest review)
        {
            var Dto = review.Adapt<Review>();
            if (Dto.UserId != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _reviewService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о отзыве
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "ReviewId": 1,
        ///       "ProductId": 1,
        ///       "UserId": 1,
        ///       "Rating": 1,
        ///       "Comment": "string",
        ///       "IsDeleted": false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z" 
        ///     }
        ///
        /// </remarks>
        /// <param name="review">Отзыв</param>
        /// <returns></returns>

        // PUT api/<ReviewController>
        [HttpPut]
        public async Task<IActionResult> Update(GetReviewResponse review)
        {
            var Dto = review.Adapt<Review>();
            if (Dto.UserId != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _reviewService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление отзыва
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ReviewController>
        [Authorize(roles: 1)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.Delete(id);
            return Ok();
        }
    }
}