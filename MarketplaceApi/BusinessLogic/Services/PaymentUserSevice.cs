using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class PaymentUserService : IPaymentUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PaymentUserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<PaymentUser>> GetAll()
        {
            return await _repositoryWrapper.PaymentUser.FindAll();
        }

        public async Task<PaymentUser> GetById(int id)
        {
            var paymentuser = await _repositoryWrapper.PaymentUser
                .FindByCondition(x => x.PaymentId == id);
            return paymentuser.First();
        }

        public async Task Create(PaymentUser model)
        {
            _repositoryWrapper.PaymentUser.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(PaymentUser model)
        {
            _repositoryWrapper.PaymentUser.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var paymentuser = await _repositoryWrapper.PaymentUser
                .FindByCondition(x => x.PaymentId == id);

            _repositoryWrapper.PaymentUser.Delete(paymentuser.First());
            _repositoryWrapper.Save();
        }
    }
}