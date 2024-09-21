using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserDiscountService
    {
        Task<List<UserDiscount>> GetAll();
        Task<UserDiscount> GetById(int id);
        Task Create(UserDiscount model);
        Task Update(UserDiscount model);
        Task Delete(int id);
    }
}
