using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentUserController : ControllerBase
    {
        private IPaymentUserService _paymentUserService;
        public PaymentUserController(IPaymentUserService paymentUserService)
        {
            _paymentUserService = paymentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _paymentUserService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _paymentUserService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaymentUser paymentuser)
        {
            await _paymentUserService.Create(paymentuser);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUser paymentuser)
        {
            await _paymentUserService.Update(paymentuser);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentUserService.Delete(id);
            return Ok();
        }
    }
}
