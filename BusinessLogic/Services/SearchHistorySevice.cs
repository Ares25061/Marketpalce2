using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SearchHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<SearchHistory>> GetAll()
        {
            return await _repositoryWrapper.SearchHistory.FindAll();
        }

        public async Task<SearchHistory> GetById(int id)
        {
            var searchhistory = await _repositoryWrapper.SearchHistory
                .FindByCondition(x => x.SearchHistoryId == id);
            return searchhistory.First();
        }

        public async Task Create(SearchHistory model)
        {
            _repositoryWrapper.SearchHistory.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(SearchHistory model)
        {
            _repositoryWrapper.SearchHistory.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var searchhistory = await _repositoryWrapper.SearchHistory
                .FindByCondition(x => x.SearchHistoryId == id);

            _repositoryWrapper.SearchHistory.Delete(searchhistory.First());
            _repositoryWrapper.Save();
        }
    }
}