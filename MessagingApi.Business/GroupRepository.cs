using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
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

        public Task Add(Group entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Group entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Group> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Group entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
