using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindAll();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);
            if (user is null || user.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return user.First();
        }

        public async Task Create(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Username))
            {
                throw new ArgumentException(nameof(model.Username));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException(nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException(nameof(model.Password));
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
            }
            _repositoryWrapper.User.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Username))
            {
                throw new ArgumentException(nameof(model.Username));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException(nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException(nameof(model.Password));
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
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
            _repositoryWrapper.User.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);
            if (user is null || user.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.User.Delete(user.First());
            _repositoryWrapper.Save();
        }
    }
}