using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.EntityFrameworkCore;
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

        public async Task Add(Message entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Message entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            var messages = await _context.Messages
                .Include(x => x.Group)
                .AsNoTracking()
                .ToListAsync();

            return messages;
        }

        public async Task<Message> GetById(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Message entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

