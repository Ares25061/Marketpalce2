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
    public class PriceHistoryService : IPriceHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PriceHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<PriceHistory>> GetAll()
        {
            return _repositoryWrapper.PriceHistory.FindAll().ToListAsync();
        }

        public Task<PriceHistory> GetById(int id)
        {
            var pricehistory = _repositoryWrapper.PriceHistory
                .FindByCondition(x => x.PriceHistoryId == id).First();
            return Task.FromResult(pricehistory);
        }

        public Task Create(PriceHistory model)
        {
            _repositoryWrapper.PriceHistory.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(PriceHistory model)
        {
            _repositoryWrapper.PriceHistory.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var pricehistory = _repositoryWrapper.PriceHistory
                .FindByCondition(x => x.PriceHistoryId == id).First();

            _repositoryWrapper.PriceHistory.Delete(pricehistory);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
