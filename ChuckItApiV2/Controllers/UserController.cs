using Microsoft.AspNetCore.Mvc;

namespace ChuckItApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public UserController()
        {

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
