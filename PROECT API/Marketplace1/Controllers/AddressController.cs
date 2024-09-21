using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _addressService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Address address)
        {
            await _addressService.Create(address);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Address address)
        {
            await _addressService.Update(address);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressService.Delete(id);
            return Ok();
        }
    }
}
