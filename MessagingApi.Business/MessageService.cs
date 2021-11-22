using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;

namespace MessagingApi.Business
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;

        public MessageService(IRepository<Message> repository)
        {
            _repository = repository;
        }
    }
}
