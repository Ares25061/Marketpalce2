using MarketplaceApi.Authorization;
using BusinessLogic.Authorization;
using BusinessLogic.Models.Accounts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _accountService.Authenticate(model,ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            if (!User.OwnsToken(token) && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await _accountService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }
        [AllowAnonymous]
        [HttpPost("regitster")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await _accountService.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registration sucsesfull, please check your email for verification insrtructions" });
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await _accountService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset insrtructions" });
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetToken(ValidateResetTokenRequest model)
        {
            await _accountService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }


        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await _accountService.ResetPassword(model);
            return Ok(new { message = "Password reset successfullm you can now login" });
        }
        [Authorize(roles: 1)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
        {
            var accounts = await _accountService.GetAll();
            return Ok(accounts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AccountResponse>> GetById(int id)
        {
            if (id != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            var account = await _accountService.GetById(id);
            return Ok();
        }
        [Authorize(roles:2)]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> Create(CreateRequest model)
        {
            var account = await _accountService.Create(model);
            return Ok(account);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AccountResponse>> Update(int id, UpdateRequest model)
        {
            if (id != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            if (User.RoleId != 1)
            {
                model.Role = null;
            }

            var account = await _accountService.Update(id, model);
            return Ok(account);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != User.UserId && User.RoleId != 1)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _accountService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }
    }
}
