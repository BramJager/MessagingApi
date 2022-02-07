﻿using AutoMapper;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using MessagingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                User user = _mapper.Map<User>(registration);
                await _service.RegisterUser(user, registration.Password);
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
                if (await _service.CheckLogin(info.Username, info.Password))
                {
                    return Ok(await _service.GenerateJWTForUsername(info.Username));
                }

                throw new InvalidCredentialException();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list/loggedin")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListOfLoggedInUser()
        {
            try
            {
                var users = await _service.GetLoggedInUsers();
                return Ok(users);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListAllUsers()
        {
            try
            {
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
                await _service.BlockUserById(userId);
                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> UpdateUser(SignUpModel model)
        {
            try
            {
                var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);

                _mapper.Map(model, currentUser);

                await _service.UpdateUser(currentUser, model.Password);

                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
