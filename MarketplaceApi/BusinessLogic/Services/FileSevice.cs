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
            return file.First();
        }

        public async Task Create(Domain.Models.File model)
        {
            _repositoryWrapper.File.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Domain.Models.File model)
        {
            _repositoryWrapper.File.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var file = await _repositoryWrapper.File
                .FindByCondition(x => x.FileId == id);

            _repositoryWrapper.File.Delete(file.First());
            _repositoryWrapper.Save();
        }
    }
}