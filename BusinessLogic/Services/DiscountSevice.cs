using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class DiscountService : IDiscountService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DiscountService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Discount>> GetAll()
        {
            return await _repositoryWrapper.Discount.FindAll();
        }

        public async Task<Discount> GetById(int id)
        {
            var discount = await _repositoryWrapper.Discount
                .FindByCondition(x => x.DiscountId == id);
            return discount.First();
        }

        public async Task Create(Discount model)
        {
            _repositoryWrapper.Discount.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Discount model)
        {
            _repositoryWrapper.Discount.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var discount = await _repositoryWrapper.Discount
                .FindByCondition(x => x.DiscountId == id);

            _repositoryWrapper.Discount.Delete(discount.First());
            _repositoryWrapper.Save();
        }
    }
}