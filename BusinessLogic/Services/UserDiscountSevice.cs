using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class UserDiscountService : IUserDiscountService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserDiscountService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserDiscount>> GetAll()
        {
            return await _repositoryWrapper.UserDiscount.FindAll();
        }

        public async Task<UserDiscount> GetById(int id)
        {
            var userdiscount = await _repositoryWrapper.UserDiscount
                .FindByCondition(x => x.UserDiscountId == id);
            if (userdiscount is null || userdiscount.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return userdiscount.First();
        }

        public async Task Create(UserDiscount model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.UserDiscount.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(UserDiscount model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.UserDiscount.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var userdiscount = await _repositoryWrapper.UserDiscount
                .FindByCondition(x => x.UserDiscountId == id);
            if (userdiscount is null || userdiscount.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.UserDiscount.Delete(userdiscount.First());
            _repositoryWrapper.Save();
        }
    }
}