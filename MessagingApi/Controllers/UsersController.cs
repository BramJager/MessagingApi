﻿using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
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
                return Ok(user);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("token")]
        public async Task<ActionResult> LogIn(SignInModel info)
        {
            if (await _service.ValidateUser(info))
            {
                var user = await _service.GetUserByUsername(info.Username);
                var roles = await _service.GetRolesByUser(user);
                return Ok(_service.GenerateJWT(user, roles));
            }
            return BadRequest();
        }

        [HttpGet("list")]
        [Authorize(Roles = "User, Groupmoderator, Administrator")]
        public async Task<ActionResult> GetListOfLoggedInUser()
        {
            var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);
            if (currentUser == null || currentUser.Blocked == true) return BadRequest("You are currently blocked, contact admin for more information.");
            var users = await _service.GetUsers();
            return Ok(users);
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
            var currentUser = await _service.GetCurrentUserFromHttp(HttpContext);            
            var checkUser = await _service.GetUserById(userId);
            if (currentUser == null || currentUser.Blocked == true) return BadRequest("You are currently blocked, contact admin for more information.");
            if (currentUser == checkUser)
            {
                currentUser.Email = info.Mail;
                currentUser.FirstName = info.FirstName;
                currentUser.Surname = info.LastName;
                currentUser.UserName = info.Username;

                await _service.UpdateUser(currentUser, info.Password);
                return Ok(currentUser);
            }

            return BadRequest("You requested to update a different user, please do not do that.");
        }
    }
}
