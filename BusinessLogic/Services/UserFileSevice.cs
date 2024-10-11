using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class UserFileService : IUserFileService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserFileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserFile>> GetAll()
        {
            return await _repositoryWrapper.UserFile.FindAll();
        }

        public async Task<UserFile> GetById(int id)
        {
            var userfile = await _repositoryWrapper.UserFile
                .FindByCondition(x => x.UserFileId == id);
            if (userfile is null || userfile.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return userfile.First();
        }

        public async Task Create(UserFile model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.UserFile.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(UserFile model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.UserFile.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var userfile = await _repositoryWrapper.UserFile
                .FindByCondition(x => x.UserFileId == id);
            if (userfile is null || userfile.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.UserFile.Delete(userfile.First());
            _repositoryWrapper.Save();
        }
    }
}