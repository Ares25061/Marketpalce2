using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PriceHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<PriceHistory>> GetAll()
        {
            return await _repositoryWrapper.PriceHistory.FindAll();
        }

        public async Task<PriceHistory> GetById(int id)
        {
            var pricehistory = await _repositoryWrapper.PriceHistory
                .FindByCondition(x => x.PriceHistoryId == id);
            if (pricehistory is null || pricehistory.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return pricehistory.First();
        }

        public async Task Create(PriceHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Price < 1)
            {
                throw new ArgumentException(nameof(model.Price));
            }
            _repositoryWrapper.PriceHistory.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(PriceHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Price < 1)
            {
                throw new ArgumentException(nameof(model.Price));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
            }
            if (model.ChangeDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.ChangeDate));
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
            _repositoryWrapper.PriceHistory.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var pricehistory = await _repositoryWrapper.PriceHistory
                .FindByCondition(x => x.PriceHistoryId == id);
            if (pricehistory is null || pricehistory.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.PriceHistory.Delete(pricehistory.First());
            _repositoryWrapper.Save();
        }
    }
}