using Domain.Interfaces;
using Domain.Models;
using System.Net;

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
            if (address is null || address.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return address.First();
        }

        public async Task Create(Address model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.AddressLine1))
            {
                throw new ArgumentException(nameof(model.AddressLine1));
            }
            if (string.IsNullOrEmpty(model.City))
            {
                throw new ArgumentException(nameof(model.City));
            }
            if (string.IsNullOrEmpty(model.ZipCode))
            {
                throw new ArgumentException(nameof(model.ZipCode));
            }
            if (string.IsNullOrEmpty(model.Country))
            {
                throw new ArgumentException(nameof(model.Country));
            }
            if (string.IsNullOrEmpty(model.State))
            {
                throw new ArgumentException(nameof(model.State));
            }

            await _repositoryWrapper.Adress.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Address model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.AddressLine1))
            {
                throw new ArgumentException(nameof(model.AddressLine1));
            }
            if (string.IsNullOrEmpty(model.City))
            {
                throw new ArgumentException(nameof(model.City));
            }
            if (string.IsNullOrEmpty(model.State))
            {
                throw new ArgumentException(nameof(model.State));
            }
            if (string.IsNullOrEmpty(model.ZipCode))
            {
                throw new ArgumentException(nameof(model.ZipCode));
            }
            if (string.IsNullOrEmpty(model.Country))
            {
                throw new ArgumentException(nameof(model.Country));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
            }
            if (model.ModifiedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.ModifiedDate));
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

            await _repositoryWrapper.Adress.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var address = await _repositoryWrapper.Adress
                .FindByCondition(x => x.AddressId == id);
            if (address is null || address.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.Adress.Delete(address.First());
            await _repositoryWrapper.Save();
        }
        public async Task SoftDelete(int id)
        {
            var address = await GetById(id);
            if (address == null)
            {
                throw new ArgumentNullException("Not found");
            }
            address.IsDeleted = true;
            address.DeletedDate = DateTime.UtcNow;
            await Update(address);
        }
    }
}