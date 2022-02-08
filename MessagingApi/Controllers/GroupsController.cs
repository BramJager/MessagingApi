using AutoMapper;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using MessagingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MessagingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IUserService userService, IMapper mapper)
        {
            _groupService = groupService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _groupService.GetGroups();
            return Ok(groups);
        }


        [HttpPost]
        public async Task<ActionResult> CreateGroup(GroupModel model)
        {

            var group = _mapper.Map<Group>(model);

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
            return Ok();

        }

        [HttpPost]
        [Route("invite")]
        [Authorize(Roles = "Groupmoderator, Administrator")]
        public async Task<ActionResult> AddUserToGroup(JoinModel model)
        {
            var user = await _userService.GetUserById(model.UserId);
            var group = await _groupService.GetGroupById(model.GroupId);

            await _groupService.AddUserToGroup(group, user);
            return Ok();

        }


        [HttpPost]
        [Route("join")]
        public async Task<ActionResult> JoinGroup(JoinModel model)
        {

            var currentUser = await _userService.GetCurrentUserFromHttp(HttpContext);
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

            throw new UnauthorizedAccessException();
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<ActionResult> RemoveUserFromGroup(RemoveUserFromGroupModel model)
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

        [HttpDelete]
        [Route("{groupId}/remove")]
        [Authorize(Roles = "Groupmoderator, Administrator")]
        public async Task<ActionResult> RemoveGroup(int groupId)
        {

            var group = await _groupService.GetGroupById(groupId);

            group.Removed = true;

            await _groupService.UpdateGroup(group);
            return Ok();

        }
    }
}
