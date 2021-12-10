using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CheckLogin(string username, string password);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task RegisterUser(User user, string Password);
        Task<User> GetUserByUsername(string username);
        Task<string> GenerateJWTForUsername(string username);
        string GenerateJWT(User user, List<string> roles);
        Task<List<string>> GetRolesByUser(User user);
        Task UpdateUser(User user, string password);
        Task BlockUserById(int id);
        Task<User> GetCurrentUserFromHttp(HttpContext http);
        Task<IEnumerable<User>> GetLoggedInUsers();
    }
}
