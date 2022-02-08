using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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

        public void ValidateGroupForCreating(Group group)
        {
            if (group == null) throw new ArgumentNullException("Group");
            if (group.Name.IsNullOrEmpty()) throw new ArgumentNullException(group.Name, nameof(group));
            if (group.Visibility == Visibility.Private && string.IsNullOrEmpty(group.PasswordHash)) throw new ArgumentNullException("Password is required for private group.");
        }

        public async Task CreateGroup(Group group)
        {
            ValidateGroupForCreating(group);
            await _repository.Add(group);
        }

        public async Task<Group> AddUserToGroup(Group group, User user)
        {
            var group1 = await _repository.GetById(group.GroupId);
            group1.Users.Add(user);
            await _repository.Update(group1);
            return group1;
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<User>> GetUsersOfGroupById(int groupId)
        {
            var group = await GetGroupById(groupId);
            return group.Users;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            await _repository.Update(group);
            return group;
        }        
        
        public async Task<Group> RemoveUserFromGroup(Group group, User user)
        {
            group.Users.Remove(user);
            await _repository.Update(group);
            return group;
        }

        public string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public string ComputeHash(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
