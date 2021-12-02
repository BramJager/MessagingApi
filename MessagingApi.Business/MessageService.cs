using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;

        public MessageService(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public async Task CreateMessage(Message message)
        {
            await _repository.Add(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesById(int id)
        {
            return await _repository.Search(x => x.GroupId == id);
        }

        public async Task<IEnumerable<Message>> SearchMessagesByQuery(string query)
        {
            return await _repository.Search(x => x.Content.Contains(query));
        }
    }
}
