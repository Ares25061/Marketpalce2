using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Services
{
    public class ImageService : IImageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ImageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<DataAccess.Models.Image>> GetAll()
        {
            return _repositoryWrapper.Image.FindAll().ToListAsync();
        }

        public Task<DataAccess.Models.Image> GetById(int id)
        {
            var image = _repositoryWrapper.Image
                .FindByCondition(x => x.ImageId == id).First();
            return Task.FromResult(image);
        }

        public Task Create(DataAccess.Models.Image model)
        {
            _repositoryWrapper.Image.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(DataAccess.Models.Image model)
        {
            _repositoryWrapper.Image.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var image = _repositoryWrapper.Image
                .FindByCondition(x => x.ImageId == id).First();

            _repositoryWrapper.Image.Delete(image);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
