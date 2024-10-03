using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPaymentUserService
    {
        Task<List<PaymentUser>> GetAll();
        Task<PaymentUser> GetById(int id);
        Task Create(PaymentUser model);
        Task Update(PaymentUser model);
        Task Delete(int id);
    }
}