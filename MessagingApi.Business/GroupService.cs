using MessagingApi.Business.Interfaces;
using System.Text.RegularExpressions;

namespace MessagingApi.Business
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _repository;

        public GroupService(IRepository<Group> repository)
        {
            _repository = repository;
        }
    }
}
