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
    public class SearchHistoryService : ISearchHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SearchHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<SearchHistory>> GetAll()
        {
            return _repositoryWrapper.SearchHistory.FindAll().ToListAsync();
        }

        public Task<SearchHistory> GetById(int id)
        {
            var searchHistory = _repositoryWrapper.SearchHistory
                .FindByCondition(x => x.SearchHistoryId == id).First();
            return Task.FromResult(searchHistory);
        }

        public Task Create(SearchHistory model)
        {
            _repositoryWrapper.SearchHistory.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(SearchHistory model)
        {
            _repositoryWrapper.SearchHistory.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var searchHistory = _repositoryWrapper.SearchHistory
                .FindByCondition(x => x.SearchHistoryId == id).First();

            _repositoryWrapper.SearchHistory.Delete(searchHistory);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
