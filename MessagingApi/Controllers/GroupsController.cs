using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using MessagingApi.Domain.Models;
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
            try
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
                        throw new ArgumentNullException(nameof(model.Password));
                    }
                }

                await _groupService.CreateGroup(group);
                return Ok(group);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("join")]
        [Authorize]
        public async Task<ActionResult> InviteUserOrJoinGroup(JoinModel model)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUserFromHttp(HttpContext);
                var roles = await _userService.GetRolesByUser(currentUser);
                var user = await _userService.GetUserById(model.UserId);
                var group = await _groupService.GetGroupById(model.GroupId);

                if (currentUser.Id == model.UserId)
                {
                    if (group.Visibility == Visibility.Public)
                    {
                        await _groupService.AddUserToGroup(group, currentUser);
                        return Ok();
                    }
                    else
                    {
                        if (group.PasswordHash == _groupService.ComputeHash(model.Password + group.Salt))
                        {
                            await _groupService.AddUserToGroup(group, currentUser);
                            return Ok();
                        }

                        throw new ArgumentException(nameof(model.Password));
                    }
                }

                if (roles.Contains("Administrator") || roles.Contains("Groupmoderator"))
                {
                    return Ok(model.Message);
                }

                throw new UnauthorizedAccessException();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("remove")]
        [Authorize]
        public async Task<ActionResult> RemoveUserFromGroup(RemoveUserFromGroupModel model)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUserFromHttp(HttpContext);
                var roles = await _userService.GetRolesByUser(currentUser);
                var group = await _groupService.GetGroupById(model.GroupId);

                if (currentUser.Id == model.UserId)
                {
                    await _groupService.RemoveUserFromGroup(group, currentUser);
                    return Ok();
                }

                if (roles.Contains("Administrator") || roles.Contains("Groupmoderator"))
                {
                    var user = await _userService.GetUserById(model.UserId);
                    await _groupService.RemoveUserFromGroup(group, user);
                    return Ok();
                }

                throw new UnauthorizedAccessException();
            }

            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{groupId}/remove")]
        [Authorize(Roles = "Groupmoderator, Administrator")]
        public async Task<ActionResult> RemoveGroup(int groupId)
        {
            try
            {
                var group = await _groupService.GetGroupById(groupId);

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
