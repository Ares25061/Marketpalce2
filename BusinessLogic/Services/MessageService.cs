using Domain.Interfaces;
using Domain.Models;

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
            return message.First();
        }

        public async Task Create(Message model)
        {
            _repositoryWrapper.Message.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Message model)
        {
            _repositoryWrapper.Message.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var message = await _repositoryWrapper.Message
                .FindByCondition(x => x.MessageId == id);

            _repositoryWrapper.Message.Delete(message.First());
            _repositoryWrapper.Save();
        }
    }
}