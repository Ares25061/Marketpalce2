using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PaymentUserService : IPaymentUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PaymentUserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<PaymentUser>> GetAll()
        {
            return _repositoryWrapper.PaymentUser.FindAll().ToListAsync();
        }

        public Task<PaymentUser> GetById(int id)
        {
            var paymentuser = _repositoryWrapper.PaymentUser
                .FindByCondition(x => x.PaymentId == id).First();
            return Task.FromResult(paymentuser);
        }

        public Task Create(PaymentUser model)
        {
            _repositoryWrapper.PaymentUser.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(PaymentUser model)
        {
            _repositoryWrapper.PaymentUser.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var paymentuser = _repositoryWrapper.PaymentUser
                .FindByCondition(x => x.PaymentId == id).First();

            _repositoryWrapper.PaymentUser.Delete(paymentuser);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
