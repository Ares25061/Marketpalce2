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
            if (chatparticipant is null || chatparticipant.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            return chatparticipant.First();
        }

        public async Task Create(ChatParticipant model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.ChatParticipant.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(ChatParticipant model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
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
            _repositoryWrapper.ChatParticipant.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var chatparticipant = await _repositoryWrapper.ChatParticipant
                .FindByCondition(x => x.ChatParticipantId == id);
            if (chatparticipant is null || chatparticipant.Count == 0)
            {
                throw new ArgumentNullException("Not found");
            }
            _repositoryWrapper.ChatParticipant.Delete(chatparticipant.First());
            _repositoryWrapper.Save();
        }
    }
}