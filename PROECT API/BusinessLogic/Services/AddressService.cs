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
    public class AddressService : IAddressService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AddressService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Address>> GetAll()
        {
            return _repositoryWrapper.Adress.FindAll().ToListAsync();
        }

        public Task<Address> GetById(int id)
        {
            var adress = _repositoryWrapper.Adress
                .FindByCondition(x => x.AddressId == id).First();
            return Task.FromResult(adress);
        }

        public Task Create(Address model)
        {
            _repositoryWrapper.Adress.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Address model)
        {
            _repositoryWrapper.Adress.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var adress = _repositoryWrapper.Adress
                .FindByCondition(x => x.AddressId == id).First();

            _repositoryWrapper.Adress.Delete(adress);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
