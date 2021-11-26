using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroup(Group group);

        Task<Group> AddUserToGroup(Group group, User user);

        Task<Group> GetGroupById(int id);

        Task<IEnumerable<Group>> GetGroups();

        List<User> GetUsersOfGroup(Group group);

        Group UpdateGroup(Group group);
    }
}
