using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetAll();
        Task<OrderItem> GetById(int id);
        Task Create(OrderItem model);
        Task Update(OrderItem model);
        Task Delete(int id);
    }
}
