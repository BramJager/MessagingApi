using MessagingApi.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
