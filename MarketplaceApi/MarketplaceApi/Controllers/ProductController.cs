using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получение информации о всех товарах
        /// </summary>
        /// <returns></returns>
        /// 
        // GET api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _productService.GetAll();
            return Ok(Dto.Adapt<List<GetProductResponse>>());
        }

        /// <summary>
        /// Получение информации о товаре по id
        /// </summary>
        /// <param name="id">ID</param>

        /// <returns></returns>

        // GET api/<ProductController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _productService.GetById(id);
            return Ok(Dto.Adapt<GetProductResponse>());
        }

        /// <summary>
        /// Создание нового товара
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "ProductName": "string",
        ///       "Description": "string",
        ///       "Price": 93.75,
        ///       "CategoryId": 1,
        ///       "SellerId": 1,
        ///       "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="product">Товар</param>
        /// <returns></returns>

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductRequest product)
        {
            var Dto = product.Adapt<Product>();
            await _productService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о товаре
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "ProductId": 1,
        ///       "ProductName": "string",
        ///       "Description": "string",
        ///       "Price": 93.75,
        ///       "CategoryId": 1,
        ///       "SellerId": 1,
        ///       "IsDeleted": false,
        ///       "CreatedBy": 1,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedBy": 1,
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="product">Товар</param>
        /// <returns></returns>

        // PUT api/<ProductController>
        [HttpPut]
        public async Task<IActionResult> Update(GetProductResponse product)
        {
            var Dto = product.Adapt<Product>();
            await _productService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>

        // DELETE api/<ProductController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}