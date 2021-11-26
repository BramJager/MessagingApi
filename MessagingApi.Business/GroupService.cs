using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _repository;

        public GroupService(IRepository<Group> repository)
        {
            _repository = repository;
        }

        public async Task CreateGroup(Group group)
        {
            await _repository.Add(group);
        }

        public async Task<Group> AddUserToGroup(Group group, User user)
        {
            group.Users.Add(user);
            await _repository.Update(group);
            return group;
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _repository.GetAll();
        }

        public List<User> GetUsersOfGroup(Group group)
        {
            return (List<User>)group.Users;
        }

        public Group UpdateGroup(Group group)
        {
            _repository.Update(group);
            return group;
        }
    }
}
