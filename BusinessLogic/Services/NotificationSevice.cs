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
            if (notification is null || notification.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return notification.First();
        }

        public async Task Create(Notification model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.NotificationType))
            {
                throw new ArgumentException(nameof(model.NotificationType));
            }
            if (string.IsNullOrEmpty(model.Message))
            {
                throw new ArgumentException(nameof(model.Message));
            }
            _repositoryWrapper.Notification.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Notification model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.NotificationType))
            {
                throw new ArgumentException(nameof(model.NotificationType));
            }
            if (string.IsNullOrEmpty(model.Message))
            {
                throw new ArgumentException(nameof(model.Message));
            }
            if (model.CreatedDate > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.CreatedDate));
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
            _repositoryWrapper.Notification.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);
            if (notification is null || notification.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.Notification.Delete(notification.First());
            _repositoryWrapper.Save();
        }
    }
}