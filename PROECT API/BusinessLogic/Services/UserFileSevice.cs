using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserFileService : IUserFileService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserFileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<UserFile>> GetAll()
        {
            return _repositoryWrapper.UserFile.FindAll().ToListAsync();
        }

        public Task<UserFile> GetById(int id)
        {
            var userFile = _repositoryWrapper.UserFile
                .FindByCondition(x => x.UserFileId == id).First();
            return Task.FromResult(userFile);
        }

        public Task Create(UserFile model)
        {
            _repositoryWrapper.UserFile.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(UserFile model)
        {
            _repositoryWrapper.UserFile.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var userFile = _repositoryWrapper.UserFile
                .FindByCondition(x => x.UserFileId == id).First();

            _repositoryWrapper.UserFile.Delete(userFile);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
