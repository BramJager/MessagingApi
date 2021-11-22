using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public Task Add(Message entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Message entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Message> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Message entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

