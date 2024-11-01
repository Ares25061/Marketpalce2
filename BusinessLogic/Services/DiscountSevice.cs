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
            if (discount is null || discount.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return discount.First();
        }

        public async Task Create(Discount model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.DiscountCode))
            {
                throw new ArgumentException(nameof(model.DiscountCode));
            }
            if (model.DiscountPercentage < 1 || model.DiscountPercentage > 100)
            {
                throw new ArgumentException(nameof(model.DiscountPercentage));
            }
            if (model.StartDate < model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            if (model.StartDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
           await _repositoryWrapper.Discount.Create(model);
           await _repositoryWrapper.Save();
        }

        public async Task Update(Discount model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.DiscountCode))
            {
                throw new ArgumentException(nameof(model.DiscountCode));
            }
            if (model.DiscountPercentage < 1 || model.DiscountPercentage > 100)
            {
                throw new ArgumentException(nameof(model.DiscountPercentage));
            }
            if (model.StartDate < model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            if (model.StartDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
            }
            if (model.IsDeleted is true && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.IsDeleted));
            }
            if (model.DeletedBy is not null && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.DeletedDate));
            }
            if (model.DeletedBy is null && model.DeletedDate is not null)
            {
                throw new ArgumentException(nameof(model.DeletedBy));
            }
           await _repositoryWrapper.Discount.Update(model);
           await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var discount = await _repositoryWrapper.Discount
                .FindByCondition(x => x.DiscountId == id);
            if (discount is null || discount.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
           await _repositoryWrapper.Discount.Delete(discount.First());
           await _repositoryWrapper.Save();
        }
    }
}