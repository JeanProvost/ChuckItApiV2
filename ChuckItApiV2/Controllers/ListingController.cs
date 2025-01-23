using Microsoft.AspNetCore.Mvc;

namespace ChuckItApiV2.Controllers
{
    public class ListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
