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
    public class UserDiscountService : IUserDiscountService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserDiscountService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<UserDiscount>> GetAll()
        {
            return _repositoryWrapper.UserDiscount.FindAll().ToListAsync();
        }

        public Task<UserDiscount> GetById(int id)
        {
            var userDiscount = _repositoryWrapper.UserDiscount
                .FindByCondition(x => x.UserDiscountId == id).First();
            return Task.FromResult(userDiscount);
        }

        public Task Create(UserDiscount model)
        {
            _repositoryWrapper.UserDiscount.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(UserDiscount model)
        {
            _repositoryWrapper.UserDiscount.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var userDiscount = _repositoryWrapper.UserDiscount
                .FindByCondition(x => x.UserDiscountId == id).First();

            _repositoryWrapper.UserDiscount.Delete(userDiscount);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
