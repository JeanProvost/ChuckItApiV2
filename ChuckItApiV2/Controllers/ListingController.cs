// Ignore Spelling: auth

using Amazon.CognitoIdentityProvider.Model;
using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckIt.Core.Interfaces.IServices;
using ChuckItApiV2.Validators.Listings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateListing([FromForm] CreateListingDto request)
        {
           // var validator = new CreateListingValidator
            var listing = await _listingService.CreateListingAsync(request);

            return Ok(listing);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> UpdateListing(Guid id, [FromBody] UpdateListingDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var listing = await _listingService.UpdateListingAsync(request);
                return Ok(listing);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteListing(Guid id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                await _listingService.DeleteListingAsync(id);
                return Ok("Listing successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
