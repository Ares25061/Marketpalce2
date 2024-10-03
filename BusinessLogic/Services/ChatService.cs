using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChatService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Chat>> GetAll()
        {
            return await _repositoryWrapper.Chat.FindAll();
        }

        public async Task<Chat> GetById(int id)
        {
            var chat = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);
            return chat.First();
        }

        public async Task Create(Chat model)
        {
            _repositoryWrapper.Chat.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Chat model)
        {
            _repositoryWrapper.Chat.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var chat = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);

            _repositoryWrapper.Chat.Delete(chat.First());
            _repositoryWrapper.Save();
        }
    }
}