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
            if (searchhistory is null || searchhistory.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return searchhistory.First();
        }

        public async Task Create(SearchHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.SearchTerm))
            {
                throw new ArgumentException(nameof(model.SearchTerm));
            }
            _repositoryWrapper.SearchHistory.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(SearchHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.SearchTerm))
            {
                throw new ArgumentException(nameof(model.SearchTerm));
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
            _repositoryWrapper.SearchHistory.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var searchhistory = await _repositoryWrapper.SearchHistory
                .FindByCondition(x => x.SearchHistoryId == id);
            if (searchhistory is null || searchhistory.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.SearchHistory.Delete(searchhistory.First());
            _repositoryWrapper.Save();
        }
    }
}