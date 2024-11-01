using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProductService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _repositoryWrapper.Product.FindAll();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _repositoryWrapper.Product
                .FindByCondition(x => x.ProductId == id);
            if (product is null || product.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return product.First();
        }

        public async Task Create(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ProductName))
            {
                throw new ArgumentException(nameof(model.ProductName));
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ArgumentException(nameof(model.Description));
            }
            if (model.Price < 1)
            {
                throw new ArgumentException(nameof(model.Price));
            }
            await _repositoryWrapper.Product.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ProductName))
            {
                throw new ArgumentException(nameof(model.ProductName));
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ArgumentException(nameof(model.Description));
            }
            if (model.Price < 1)
            {
                throw new ArgumentException(nameof(model.Price));
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
            await _repositoryWrapper.Product.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var product = await _repositoryWrapper.Product
                .FindByCondition(x => x.ProductId == id);
            if (product is null || product.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.Product.Delete(product.First());
            await _repositoryWrapper.Save();
        }
    }
}