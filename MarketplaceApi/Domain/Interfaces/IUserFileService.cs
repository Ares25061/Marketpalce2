using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserFileService
    {
        Task<List<UserFile>> GetAll();
        Task<UserFile> GetById(int id);
        Task Create(UserFile model);
        Task Update(UserFile model);
        Task Delete(int id);
    }
}