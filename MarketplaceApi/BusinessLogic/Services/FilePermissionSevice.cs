using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class FilePermissionService : IFilePermissionService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FilePermissionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<FilePermission>> GetAll()
        {
            return await _repositoryWrapper.FilePermission.FindAll();
        }

        public async Task<FilePermission> GetById(int id)
        {
            var filepermission = await _repositoryWrapper.FilePermission
                .FindByCondition(x => x.FilePermissionId == id);
            return filepermission.First();
        }

        public async Task Create(FilePermission model)
        {
            _repositoryWrapper.FilePermission.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(FilePermission model)
        {
            _repositoryWrapper.FilePermission.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var filepermission = await _repositoryWrapper.FilePermission
                .FindByCondition(x => x.FilePermissionId == id);

            _repositoryWrapper.FilePermission.Delete(filepermission.First());
            _repositoryWrapper.Save();
        }
    }
}