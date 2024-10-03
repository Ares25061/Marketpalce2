using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class AddressService : IAddressService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AddressService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Address>> GetAll()
        {
            return await _repositoryWrapper.Adress.FindAll();
        }

        public async Task<Address> GetById(int id)
        {
            var address = await _repositoryWrapper.Adress
                .FindByCondition(x => x.AddressId == id);
            return address.First();
        }

        public async Task Create(Address model)
        {
            _repositoryWrapper.Adress.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Address model)
        {
            _repositoryWrapper.Adress.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var address = await _repositoryWrapper.Adress
                .FindByCondition(x => x.AddressId == id);

            _repositoryWrapper.Adress.Delete(address.First());
            _repositoryWrapper.Save();
        }
    }
}