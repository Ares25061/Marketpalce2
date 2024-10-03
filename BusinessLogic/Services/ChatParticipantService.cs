using Domain.Interfaces;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class ChatParticipantService : IChatParticipantService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChatParticipantService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<ChatParticipant>> GetAll()
        {
            return await _repositoryWrapper.ChatParticipant.FindAll();
        }

        public async Task<ChatParticipant> GetById(int ChatId)
        {
            var chatparticipant = await _repositoryWrapper.ChatParticipant
                .FindByCondition(x => x.ChatParticipantId == ChatId);
            return chatparticipant.First();
        }

        public async Task Create(ChatParticipant model)
        {
            _repositoryWrapper.ChatParticipant.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(ChatParticipant model)
        {
            _repositoryWrapper.ChatParticipant.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var chatparticipant = await _repositoryWrapper.ChatParticipant
                .FindByCondition(x => x.ChatParticipantId == id);

            _repositoryWrapper.ChatParticipant.Delete(chatparticipant.First());
            _repositoryWrapper.Save();
        }
    }
}