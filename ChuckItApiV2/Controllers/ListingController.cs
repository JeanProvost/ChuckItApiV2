// Ignore Spelling: auth

using Amazon.CognitoIdentityProvider.Model;
using ChuckIt.Core.Entities.Listings.Dtos;
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
        private readonly IAuthService _authService;

        public ListingController(IListingService listingService, IAuthService authService)
        {
            _listingService = listingService;
            _authService = authService;
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

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateListing([FromBody] CreateListingDto request)
        {
            var listing = await _listingService.CreateListingAsync(request);

            return Ok(listing);
        }
    }
}
