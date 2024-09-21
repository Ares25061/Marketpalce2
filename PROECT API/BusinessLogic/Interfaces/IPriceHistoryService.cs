using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPriceHistoryService
    {
        Task<List<PriceHistory>> GetAll();
        Task<PriceHistory> GetById(int id);
        Task Create(PriceHistory model);
        Task Update(PriceHistory model);
        Task Delete(int id);
    }
}
