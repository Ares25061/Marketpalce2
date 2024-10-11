using Domain.Interfaces;
using Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public MessageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Message>> GetAll()
        {
            return await _repositoryWrapper.Message.FindAll();
        }

        public async Task<Message> GetById(int id)
        {
            var message = await _repositoryWrapper.Message
                .FindByCondition(x => x.MessageId == id);
            if (message is null || message.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return message.First();
        }

        public async Task Create(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.MessageContent))
            {
                throw new ArgumentException(nameof(model.MessageContent));
            }
            _repositoryWrapper.Message.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.MessageContent))
            {
                throw new ArgumentException(nameof(model.MessageContent));
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
            _repositoryWrapper.Message.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var message = await _repositoryWrapper.Message
                .FindByCondition(x => x.MessageId == id);
            if (message is null || message.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.Message.Delete(message.First());
            _repositoryWrapper.Save();
        }
    }
}