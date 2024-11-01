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
            if (filepermission is null || filepermission.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return filepermission.First();
        }

        public async Task Create(FilePermission model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.PermissionLevel))
            {
                throw new ArgumentException(nameof(model.PermissionLevel));
            }
            await _repositoryWrapper.FilePermission.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(FilePermission model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.PermissionLevel))
            {
                throw new ArgumentException(nameof(model.PermissionLevel));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
            }
            if (model.ModifiedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.ModifiedDate));
            }
            if (model.IsDeleted is true && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.IsDeleted));
            }
            if (model.DeletedBy is not null && model.DeletedDate is null || model.DeletedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.DeletedDate));
            }
            if (model.DeletedBy is null && model.DeletedDate is not null)
            {
                throw new ArgumentException(nameof(model.DeletedBy));
            }
            await _repositoryWrapper.FilePermission.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var filepermission = await _repositoryWrapper.FilePermission
                .FindByCondition(x => x.FilePermissionId == id);
            if (filepermission is null || filepermission.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.FilePermission.Delete(filepermission.First());
            await _repositoryWrapper.Save();
        }
    }
}