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
    public class NotificationService : INotificationService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public NotificationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Notification>> GetAll()
        {
            return _repositoryWrapper.Notification.FindAll().ToListAsync();
        }

        public Task<Notification> GetById(int id)
        {
            var notification = _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id).First();
            return Task.FromResult(notification);
        }

        public Task Create(Notification model)
        {
            _repositoryWrapper.Notification.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Notification model)
        {
            _repositoryWrapper.Notification.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var notification = _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id).First();

            _repositoryWrapper.Notification.Delete(notification);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
