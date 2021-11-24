using MessagingApi.Domain.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateUser(SignInInformation info);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> RegisterUser(SignUpInformation info);
        Task<User> GetUserByUsername(string username);
        string GenerateJWT(User user, List<string> roles);
        Task<List<string>> GetRolesByUser(User user);
        Task<User> UpdateUser(User user, string password);

    }
}
