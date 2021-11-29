using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace MessagingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupsController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateGroup(GroupModel model)
        {
            var group = new Group();
            group.Name = model.Name;
            group.MaxUsers = model.MaxUsers;
            group.Visibility = model.Visibility;
            if (group.Visibility == Visibility.Private)
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    group.Salt = _groupService.GetSalt();
                    group.PasswordHash = _groupService.ComputeHash(model.Password + group.Salt);
                }

                else
                {
                    return BadRequest("A private chat needs a password, please provide.");
                }
            }

            else
            {
                group.PasswordHash = null;
            }

            await _groupService.CreateGroup(group);
            return Ok(group);
        }

        [HttpPost]
        [Route("join")]
        [Authorize]
        public async Task<ActionResult> InviteUserOrJoinGroup(int userId, int groupId, string message, string password)
        {
            var currentUser = await _userService.GetCurrentUserFromHttp(HttpContext);
            var roles = await _userService.GetRolesByUser(currentUser);
            var user = await _userService.GetUserById(userId);
            var group = await _groupService.GetGroupById(groupId);

            if(currentUser.Id == userId) 
            {
                if (group.Visibility == Visibility.Public)
                {
                    await _groupService.AddUserToGroup(group, currentUser);
                    return Ok();
                }
                else
                {
                    if (group.PasswordHash == _groupService.ComputeHash(password + group.Salt))
                    {
                        await _groupService.AddUserToGroup(group, currentUser);
                        return Ok();
                    }
                    
                    return BadRequest($"Enter the correct password to join {group.Name}.");
                }
            }

            if (roles.Contains("Administrator") || roles.Contains("Groupmoderator"))
            {
                Ok(message);
            }

            return BadRequest($"You are not authorized to add {user.UserName} to {group.Name}.");
        }

        [HttpDelete]
        [Route("remove")]
        [Authorize]
        public async Task<ActionResult> RemoveUserFromGroup(int userId, int groupId)
        {
            var currentUser = await _userService.GetCurrentUserFromHttp(HttpContext);
            var roles = await _userService.GetRolesByUser(currentUser);
            var group = await _groupService.GetGroupById(groupId);

            if (currentUser.Id == userId)
            {
                await _groupService.RemoveUserFromGroup(group, currentUser);
                return Ok();
            }

            if (roles.Contains("Administrator") || roles.Contains("Groupmoderator"))
            {
                var user = await _userService.GetUserById(userId);
                await _groupService.RemoveUserFromGroup(group, user);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{groupId}/remove")]
        [Authorize(Roles = "Groupmoderator, Administrator")]
        public async Task<ActionResult> RemoveGroup(int groupId)
        {;
            var group = await _groupService.GetGroupById(groupId);
            try
            {
                group.Removed = true;
                await _groupService.UpdateGroup(group);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
