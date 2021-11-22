using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MessagingApi.Business.Settings;
using Microsoft.Extensions.Options;

namespace MessagingApi.Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(IRepository<User> repository, UserManager<User> userManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _repository = repository;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetAll();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var users = await _repository.GetAll();
            return users.FirstOrDefault(x => x.UserName == username);
        }

        public async Task<User> RegisterUser(SignUpInformation info)
        {
            var users = await _repository.GetAll();

            if (users.Any(x => x.Email == info.Mail))
            {
                throw new Exception("Email already has an account");
            }

            User newUser = new();
            PasswordHasher<User> hasher = new();
            newUser.PasswordHash = hasher.HashPassword(newUser, info.Password);
            newUser.Email = info.Mail;
            newUser.UserName = info.Username;
            newUser.FirstName = info.FirstName;
            newUser.Surname = info.LastName;
            await _repository.Add(newUser);
            await _userManager.UpdateSecurityStampAsync(newUser);

            await _userManager.AddToRoleAsync(newUser, "User");
            return newUser;
        }

        public async Task<bool> ValidateUser(SignInInformation info)
        {
            var users = await _repository.GetAll();

            var user = users.FirstOrDefault(x => x.UserName == info.Username);

            return await _userManager.CheckPasswordAsync(user, info.Password) ?  true : false;
        }

        public string GenerateJWT(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
             };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.ExpirationInMinutes));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<string>> GetRolesByUser(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return (List<string>)roles;
        }
    }
}
