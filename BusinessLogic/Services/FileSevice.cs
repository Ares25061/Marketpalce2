using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Domain.Models.File>> GetAll()
        {
            return await _repositoryWrapper.File.FindAll();
        }

        public async Task<Domain.Models.File> GetById(int id)
        {
            var file = await _repositoryWrapper.File
                .FindByCondition(x => x.FileId == id);
            if (file is null || file.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return file.First();
        }

        public async Task Create(Domain.Models.File model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.FileName))
            {
                throw new ArgumentException(nameof(model.FileName));
            }
            if (string.IsNullOrEmpty(model.FilePath))
            {
                throw new ArgumentException(nameof(model.FilePath));
            }
            if (model.FileSize < 1)
            {
                throw new ArgumentException(nameof(model.FileSize));
            }
            if (string.IsNullOrEmpty(model.FileType))
            {
                throw new ArgumentException(nameof(model.FileType));
            }
            await _repositoryWrapper.File.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Domain.Models.File model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.FileName))
            {
                throw new ArgumentException(nameof(model.FileName));
            }
            if (string.IsNullOrEmpty(model.FilePath))
            {
                throw new ArgumentException(nameof(model.FilePath));
            }
            if (model.FileSize < 1)
            {
                throw new ArgumentException(nameof(model.FileSize));
            }
            if (string.IsNullOrEmpty(model.FileType))
            {
                throw new ArgumentException(nameof(model.FileType));
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
            await _repositoryWrapper.File.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var file = await _repositoryWrapper.File
                .FindByCondition(x => x.FileId == id);
            if (file is null || file.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            await _repositoryWrapper.File.Delete(file.First());
            await _repositoryWrapper.Save();
        }
    }
}