using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesById(int id);
        Task CreateMessage(Message message);
        Task<IEnumerable<Message>> SearchMessagesByQuery(string query);
    }
}
