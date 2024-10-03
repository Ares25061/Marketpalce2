using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISearchHistoryService
    {
        Task<List<SearchHistory>> GetAll();
        Task<SearchHistory> GetById(int id);
        Task Create(SearchHistory model);
        Task Update(SearchHistory model);
        Task Delete(int id);
    }
}