using BusinessLogic.Authorization;
using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MarketplaceApi.Contracts.Address;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;
        private readonly IAccountService _accountService;

        public AddressController(IAddressService addressService, IAccountService accountService)
        {
            _addressService = addressService;
            _accountService = accountService;
        }

        /// <summary>
        /// Получение информации о всех адресах
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _addressService.GetAll();
            return Ok(Dto.Adapt<List<GetAddressResponse>>());
        }

        /// <summary>
        /// Получение информации о адресе по id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _addressService.GetById(id);
            return Ok(Dto.Adapt<GetAddressResponse>());
        }

        /// <summary>
        /// Создание нового адреса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///       "UserId": 1,
        ///       "AddressLine1": "string",
        ///       "AddressLine2": "string",
        ///       "City": "string",
        ///       "State": "string",
        ///       "ZipCode": "string",
        ///       "Country": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="address">Адрес</param>
        /// <returns></returns>
        [Authorize(roles: 1)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateAddressRequest address)
        {
            var Dto = address.Adapt<Address>();
            await _addressService.Create(Dto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации о адресе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Todo
        ///     {
        ///       "AddressId": 1,
        ///       "UserId": 1,
        ///       "AddressLine1": "string",
        ///       "AddressLine2": "string",
        ///       "City": "string",
        ///       "State": "string",
        ///       "ZipCode": "string",
        ///       "Country": "string",
        ///       "IsDeleted": false,
        ///       "CreatedDate": "2024-09-19T14:05:14.947Z",
        ///       "ModifiedDate": "2024-09-19T14:05:14.947Z",
        ///       "DeletedBy": 1,
        ///       "DeletedDate": "2024-09-19T14:05:14.947Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="address">Адресс</param>
        /// <returns></returns>
        [Authorize(roles: 1)]
        [HttpPut]
        public async Task<IActionResult> Update(GetAddressResponse address)
        {
            var Dto = address.Adapt<Address>();
            await _addressService.Update(Dto);
            return Ok();
        }

        /// <summary>
        /// Удаление адреса
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(roles: 1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressService.Delete(id);
            return Ok();
        }
    }
}