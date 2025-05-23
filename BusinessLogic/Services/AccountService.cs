﻿using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Models.Accounts;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(
        IRepositoryWrapper repositoryWrapper,
        IJwtUtils jwtUtils, IMapper mapper,
        IOptions<AppSettings> appSettings,
        IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }
        private void removeOldRefreshTokens(User account)
        {
            account.RefreshTokens.RemoveAll(x => !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repositoryWrapper.User.GetByEmailWithToken(model.Email);
            if (account == null || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
            {
                throw new AppException("Email or password is incorrect");
            }
            var jstToket = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);
            removeOldRefreshTokens(account);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jstToket;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            if ((await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Count > 0)
                throw new AppException($"Email '{model.Email}' is already registred");

            var account = _mapper.Map<User>(model);
            account.Created = DateTime.Now;
            account.Verified = DateTime.UtcNow;

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            await _repositoryWrapper.User.Delete(account);
            await _repositoryWrapper.Save();
        }

        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnique = (await _repositoryWrapper.User.FindByCondition(x => x.ResetToken == token)).Count == 0;
            if (!tokenIsUnique)
                return await generateResetToken();
            return token;
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).FirstOrDefault();

            if (account == null) return;

            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
            var resetUrl = $"{origin}/reset-password?token={account.ResetToken}";
            var message = $"<h3>Сброс пароля</h3><p>Для сброса вашего пароля перейдите по <a href='{resetUrl}'>этой ссылке</a>.</p>";

            _emailService.Send(account.Email, "Сброс пароля", message);
        }
        private async Task<User> getAccountByRefreshToken(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token))).SingleOrDefault();
            if (account == null) throw new AppException("Invalid token");
            account = await _repositoryWrapper.User.GetByIdWithToken(account.UserId);
            return account;
        }
        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }
        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.ReplacedByToken = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }


        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User account, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);

                else
                    revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);

            }
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = await _repositoryWrapper.User.FindAll();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                await _repositoryWrapper.User.Update(account);
                await _repositoryWrapper.Save();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");


            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            var responce = _mapper.Map<AuthenticateResponse>(account);
            responce.JwtToken = jwtToken;
            responce.RefreshToken = newRefreshToken.Token;

            return responce;


        }
        private async Task<string> generateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnique = (await _repositoryWrapper.User.FindByCondition(x => x.VerificationToken == token)).Count == 0;
            if (!tokenIsUnique)
                return await generateVerificationToken();

            return token;
        }
        public async Task Register(RegisterRequest model, string origin)
        {
            if ((await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Count > 0)
            {
                return;
            }

            var account = _mapper.Map<User>(model);

            var isFirstAccount = (await _repositoryWrapper.User.FindAll()).Count == 0;
            account.RoleId = isFirstAccount ? 1 : 2;
            account.CreatedDate = DateTime.UtcNow;
            account.Created = DateTime.UtcNow;
            account.VerificationToken = await generateVerificationToken();

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            await _repositoryWrapper.User.Create(account);
            await _repositoryWrapper.Save();
            var verifyUrl = $"{origin}/verify-email?token={account.VerificationToken}";
            var message = $"<h3>Подтверждение почты</h3><p>Для подтверждения вашей почты перейдите по <a href='{verifyUrl}'>этой ссылке</a>.</p>";

            _emailService.Send(account.Email, "Подтверждение почты", message);
        }

        private async Task<User> getAccountByResetToken(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow)).SingleOrDefault();
            if (account == null)
            {
                throw new AppException("Неверный или просроченный токен.");
            }
            return account;
        }
        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByResetToken(model.Token);
            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");


            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }


        private async Task<User> getAccount(int id)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.UserId == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }
        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (account.Email != model.Email && (await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Count > 0)
                throw new AppException($"Email '{model.Email}' is already registred");

            if (!string.IsNullOrEmpty(model.Password))
                account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);


            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            var account = await getAccountByResetToken(model.Token);
            if (account == null)
            {
                throw new AppException("Неверный или просроченный токен.");
            }
        }

        public async Task VerifyEmail(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.VerificationToken == token)).FirstOrDefault();
            if (account == null)
                throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }
        public async Task ResendVerificationCode(string email, string origin)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.Email == email)).FirstOrDefault();

            if (account == null)
            {
                throw new AppException("Аккаунт с таким email не найден.");
            }
            account.VerificationToken = await generateVerificationToken();
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
            var verifyUrl = $"{origin}/verify-email?token={account.VerificationToken}";
            var message = $"<h3>Подтверждение почты</h3><p>Для подтверждения вашей почты перейдите по <a href='{verifyUrl}'>этой ссылке</a>.</p>";

            _emailService.Send(account.Email, "Подтверждение почты", message);
        }
        public async Task<bool> CheckUsernameExists(string username)
        {
            return (await _repositoryWrapper.User.FindByCondition(x => x.UserName == username)).Any();
        }
        public async Task<bool> CheckEmailExists(string email)
        {
            return (await _repositoryWrapper.User.FindByCondition(x => x.Email == email)).Any();
        }

    }
}