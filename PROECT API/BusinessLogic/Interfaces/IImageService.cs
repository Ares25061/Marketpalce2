using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IImageService
    {
        Task<List<Image>> GetAll();
        Task<Image> GetById(int id);
        Task Create(Image model);
        Task Update(Image model);
        Task Delete(int id);
    }
}
