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
    public class FilePermissionService : IFilePermissionService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FilePermissionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<FilePermission>> GetAll()
        {
            return _repositoryWrapper.FilePermission.FindAll().ToListAsync();
        }

        public Task<FilePermission> GetById(int id)
        {
            var filepermission = _repositoryWrapper.FilePermission
                .FindByCondition(x => x.FilePermissionId == id).First();
            return Task.FromResult(filepermission);
        }

        public Task Create(FilePermission model)
        {
            _repositoryWrapper.FilePermission.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(FilePermission model)
        {
            _repositoryWrapper.FilePermission.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var filepermission = _repositoryWrapper.FilePermission
                .FindByCondition(x => x.FilePermissionId == id).First();

            _repositoryWrapper.FilePermission.Delete(filepermission);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
