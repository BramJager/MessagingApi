using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using MessagingApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MessagingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Register(SignUpModel registration)
        {
            try
            {
                User user = await _service.RegisterUser(registration);
                return Ok();
            }

            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest(e.InnerException.Message);
                }

                return BadRequest(e.Message);
            }
        }

        [HttpPost("token")]
        public async Task<ActionResult> LogIn(SignInModel info)
        {
            try
            {
                if (await _service.ValidateUser(info))
                {
                    var user = await _service.GetUserByUsername(info.Username);
                    var roles = await _service.GetRolesByUser(user);
                    return Ok(_service.GenerateJWT(user, roles));
                }
                throw new InvalidCredentialException();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListOfLoggedInUser()
        {
            try
            {
                var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);
                if (currentUser.Blocked == true) throw new AccessViolationException("You are currently blocked, contact the admin for more information.");
                var users = await _service.GetUsers();
                return Ok(users);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId}/block")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> BlockUserById(int userId)
        {
            try
            {
                var user = await _service.GetUserById(userId);
                user.Blocked = true;
                await _service.UpdateUser(user);
                return Ok(user);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> UpdateUser(int userId, SignUpModel info)
        {
            try
            {
                var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);
                var checkUser = await _service.GetUserById(userId);

                if (currentUser == null || currentUser.Blocked == true) throw new AccessViolationException("You are currently blocked, contact the admin for more information.");
                if (currentUser == checkUser)
                {
                    currentUser.Email = info.Mail;
                    currentUser.FirstName = info.FirstName;
                    currentUser.Surname = info.LastName;
                    currentUser.UserName = info.Username;

                    await _service.UpdateUser(currentUser, info.Password);
                    return Ok(currentUser);
                }

                throw new UnauthorizedAccessException();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
