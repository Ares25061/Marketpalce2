using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IChatParticipantService
    {
        Task<List<ChatParticipant>> GetAll();
        Task<ChatParticipant> GetById(int id);
        Task Create(ChatParticipant model);
        Task Update(ChatParticipant model);
        Task Delete(int id);
    }
}
