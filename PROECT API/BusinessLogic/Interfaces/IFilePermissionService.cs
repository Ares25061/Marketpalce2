using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IFilePermissionService
    {
        Task<List<FilePermission>> GetAll();
        Task<FilePermission> GetById(int id);
        Task Create(FilePermission model);
        Task Update(FilePermission model);
        Task Delete(int id);
    }
}
