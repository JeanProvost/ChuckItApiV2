using ChuckIt.Core.Interfaces.IRepositories;
using ChuckIt.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ChuckItApiV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
    }
}
