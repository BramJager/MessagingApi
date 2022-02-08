using AutoMapper;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using MessagingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace MessagingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Register(SignUpModel registration)
        {
            User user = _mapper.Map<User>(registration);
            await _service.RegisterUser(user, registration.Password);
            return Ok();
        }

        [HttpPost("token")]
        public async Task<ActionResult> LogIn(SignInModel info)
        {
            if (await _service.CheckLogin(info.Username, info.Password))
            {
                return Ok(await _service.GenerateJWTForUsername(info.Username));
            }

            throw new InvalidCredentialException();
        }

        [HttpGet("list/loggedin")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListOfLoggedInUser()
        {

            var users = await _service.GetLoggedInUsers();
            return Ok(users);
        }

        [HttpGet("list")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListAllUsers()
        {

            var users = await _service.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}/block")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> BlockUserById(int userId)
        {

            await _service.BlockUserById(userId);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> UpdateUser(SignUpModel model)
        {

            var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);

            _mapper.Map(model, currentUser);

            await _service.UpdateUser(currentUser, model.Password);

            return Ok();
        }
    }
}
