using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class OrderItemService : IOrderItemService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public OrderItemService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<OrderItem>> GetAll()
        {
            return await _repositoryWrapper.OrderItem.FindAll();
        }

        public async Task<OrderItem> GetById(int id)
        {
            var orderitem = await _repositoryWrapper.OrderItem
                .FindByCondition(x => x.OrderItemId == id);
            if (orderitem is null || orderitem.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return orderitem.First();
        }

        public async Task Create(OrderItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Quantity < 1)
            {
                throw new ArgumentException(nameof(model.Quantity));
            }
            if (model.Price < 1)
            {
                throw new ArgumentException(nameof(model.Price));
            }
            await _repositoryWrapper.OrderItem.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(OrderItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Quantity < 1)
            {
                throw new ArgumentException(nameof(model.Quantity));
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
            await _repositoryWrapper.OrderItem.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var orderitem = await _repositoryWrapper.OrderItem
                .FindByCondition(x => x.OrderItemId == id);
            if (orderitem is null || orderitem.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.OrderItem.Delete(orderitem.First());
            await _repositoryWrapper.Save();
        }
    }
}