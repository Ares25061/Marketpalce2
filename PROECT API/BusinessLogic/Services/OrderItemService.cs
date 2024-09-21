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
    public class OrderItemService : IOrderItemService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public OrderItemService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<OrderItem>> GetAll()
        {
            return _repositoryWrapper.OrderItem.FindAll().ToListAsync();
        }

        public Task<OrderItem> GetById(int id)
        {
            var orderitem = _repositoryWrapper.OrderItem
                .FindByCondition(x => x.OrderItemId == id).First();
            return Task.FromResult(orderitem);
        }

        public Task Create(OrderItem model)
        {
            _repositoryWrapper.OrderItem.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(OrderItem model)
        {
            _repositoryWrapper.OrderItem.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var orderitem = _repositoryWrapper.OrderItem
                .FindByCondition(x => x.OrderItemId == id).First();

            _repositoryWrapper.OrderItem.Delete(orderitem);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
