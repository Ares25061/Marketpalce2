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
            return pricehistory.First();
        }

        public async Task Create(PriceHistory model)
        {
            _repositoryWrapper.PriceHistory.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(PriceHistory model)
        {
            _repositoryWrapper.PriceHistory.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var pricehistory = await _repositoryWrapper.PriceHistory
                .FindByCondition(x => x.PriceHistoryId == id);

            _repositoryWrapper.PriceHistory.Delete(pricehistory.First());
            _repositoryWrapper.Save();
        }
    }
}