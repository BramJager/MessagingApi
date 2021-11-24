using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            var user = await _context.Users.FindAsync(entity);
            _context.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users
                .Include(x => x.Groups)
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersByGroupId(int groupId)
        {
            var users = await GetAll();
            return users.Where(x => x.Groups.Any(x => x.GroupId == groupId) == true);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity, string changedPassword)
        {
            var user = await _context.Users.FindAsync(entity.Id);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            var user = await _context.Users.FindAsync(entity.Id);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
