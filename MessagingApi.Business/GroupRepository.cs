using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Group entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Group entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            var groups = await _context.Groups
                .Include(x => x.Users)
                .AsNoTracking()
                .ToListAsync();

            return groups;
        }

        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Group entity)
        {
            var group = await _context.Messages.FindAsync(entity.GroupId);
            _context.Entry(group).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
