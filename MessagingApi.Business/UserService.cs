using MessagingApi.Business.Interfaces;
using MessagingApi.Business.Settings;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApi.Business
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(UserManager<User> userManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task ValidateUserForRegistration(User user)
        {
            if (user.Email.IsNullOrEmpty()) throw new ArgumentException(nameof(user.Email));
            if (user.UserName.IsNullOrEmpty()) throw new ArgumentException(nameof(user.UserName));
            if (user.FirstName.IsNullOrEmpty() || user.Surname.IsNullOrEmpty()) throw new ArgumentException(nameof(user.Name));

            if (await _userManager.FindByNameAsync(user.UserName) != null) throw new ArgumentException("This username already exists.");
            if (await _userManager.FindByEmailAsync(user.Email) != null) throw new ArgumentException("This email already has an account.");
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetLoggedInUsers()
        {
            return await _userManager.Users.Where(u => u.LastActive >= DateTime.Now.AddMinutes(-5)).ToListAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task RegisterUser(User user, string password)
        {
            await ValidateUserForRegistration(user);
            if (password.IsNullOrEmpty()) throw new ArgumentException("The password cannot be null or empty.");
            await _userManager.CreateAsync(user, password);
            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<bool> CheckLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Blocked == true) throw new AccessViolationException("You are currently blocked, contact the admin for more information.");
            
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public string GenerateJWT(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
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

        public async Task UpdateUser(User user, string password)
        {
            if (!password.IsNullOrEmpty())
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, password);
            }

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<User> GetCurrentUserFromHttp(HttpContext http)
        {
            return await _userManager.GetUserAsync(http.User);
        }

        public async Task BlockUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Blocked = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GenerateJWTForUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);

            return GenerateJWT(user, roles.ToList());
        }
    }
}