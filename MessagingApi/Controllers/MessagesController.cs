using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            await _service.CreateMessage(message);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, User, Groupmoderator")]
        public async Task<ActionResult> GetPagedMessages(int Page, int Type, int? GroupId)
        {
            if (Type == 0)
            {
                var messages = await _service.GetMessagesById(1);
                messages = messages.OrderBy(on => on.DateTime)
                .Skip((Page - 1) * 10)
                .Take(10)
                .ToList();

                return Ok(messages);
            }

            else
            {
                if (GroupId == null)
                {
                    return BadRequest();
                }
                else
                {
                    var messages = await _service.GetMessagesById(GroupId.Value);
                    messages = messages.OrderBy(on => on.DateTime)
                    .Skip((Page - 1) * 10)
                    .Take(10)
                    .ToList();

                    return Ok(messages);
                }
            }
        }
    }
}
