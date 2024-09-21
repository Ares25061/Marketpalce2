using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<List<DataAccess.Models.File>> GetAll();
        Task<DataAccess.Models.File> GetById(int id);
        Task Create(DataAccess.Models.File model);
        Task Update(DataAccess.Models.File model);
        Task Delete(int id);
    }
}
