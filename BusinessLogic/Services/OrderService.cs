using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public OrderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _repositoryWrapper.Order.FindAll();
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _repositoryWrapper.Order
                .FindByCondition(x => x.OrderId == id);
            if (order is null || order.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return order.First();
        }

        public async Task Create(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                throw new ArgumentException(nameof(model.Status));
            }
            if (model.OrderDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.OrderDate));
            }
            _repositoryWrapper.Order.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                throw new ArgumentException(nameof(model.Status));
            }
            if (model.OrderDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.OrderDate));
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
            _repositoryWrapper.Order.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var order = await _repositoryWrapper.Order
                .FindByCondition(x => x.OrderId == id);
            if (order is null || order.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.Order.Delete(order.First());
            _repositoryWrapper.Save();
        }
    }
}