using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public NotificationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _repositoryWrapper.Notification.FindAll();
        }

        public async Task<Notification> GetById(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);
            return notification.First();
        }

        public async Task Create(Notification model)
        {
            _repositoryWrapper.Notification.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Notification model)
        {
            _repositoryWrapper.Notification.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);

            _repositoryWrapper.Notification.Delete(notification.First());
            _repositoryWrapper.Save();
        }
    }
}