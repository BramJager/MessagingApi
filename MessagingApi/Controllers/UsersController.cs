using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;

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
        public async Task<ActionResult> Register(SignUpInformation registration)
        {
            try
            {
                User user = await _service.RegisterUser(registration);
                return Ok(user);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("token")]
        public async Task<ActionResult> LogIn(SignInInformation info)
        {
            if(await _service.ValidateUser(info))
            {
                var user = await _service.GetUserByEmail(info.Mail);
                var roles = await _service.GetRolesByUser(user);
                return Ok(_service.GenerateJWT(user, roles));
            }
            return BadRequest();
        }
    }


}
