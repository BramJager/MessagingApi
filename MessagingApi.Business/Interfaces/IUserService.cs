using MessagingApi.Domain.Objects;
using MessagingApi.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateUser(SignInModel info);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> RegisterUser(SignUpModel info);
        Task<User> GetUserByUsername(string username);
        string GenerateJWT(User user, List<string> roles);
        Task<List<string>> GetRolesByUser(User user);
        Task<User> UpdateUser(User user, string password);
        Task<User> UpdateUser(User user);
        Task<User> GetCurrentUserFromHttp(HttpContext http);

    }
}
