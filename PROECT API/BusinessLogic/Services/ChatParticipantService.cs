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
    public class ChatParticipantService : IChatParticipantService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChatParticipantService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<ChatParticipant>> GetAll()
        {
            return _repositoryWrapper.ChatParticipant.FindAll().ToListAsync();
        }

        public Task<ChatParticipant> GetById(int id)
        {
            var chatparticipant = _repositoryWrapper.ChatParticipant
                .FindByCondition(x => x.ChatParticipantId == id).First();
            return Task.FromResult(chatparticipant);
        }

        public Task Create(ChatParticipant model)
        {
            _repositoryWrapper.ChatParticipant.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(ChatParticipant model)
        {
            _repositoryWrapper.ChatParticipant.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var chatparticipant = _repositoryWrapper.ChatParticipant
                .FindByCondition(x => x.ChatParticipantId == id).First();

            _repositoryWrapper.ChatParticipant.Delete(chatparticipant);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
