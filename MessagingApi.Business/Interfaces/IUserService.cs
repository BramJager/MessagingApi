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
        Task<User> GetUserByEmail(string email);
        string GenerateJWT(User user, List<string> roles);
        Task<List<string>> GetRolesByUser(User user);

    }
}
