using ChuckIt.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChuckItApiV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllListings()
        {
            var listings = await _listingService.GetAllListingsAsync();

            return Ok(listings);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListingDetails(Guid id)
        {
            var listing = await _listingService.GetListingDetailsAsync(id);

            return Ok(listing);
        }
    }
}
