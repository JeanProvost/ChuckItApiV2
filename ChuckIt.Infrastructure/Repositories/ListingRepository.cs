using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckIt.Core.Interfaces.IRepositories;
using ChuckItApiV2.Core.Entities.Listings;
using ChuckItApiV2.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<ListingDto>> GetAllListingsAsync()
        {
            var listings = await _context.Listings
                .Include(l => l.Images)
                .Include(l => l.Category)
                .Select(l => new ListingDto
                {
                    Title = l.Title,
                    Description = l.Description,
                    Price = l.Price,
                    CategoryId = l.CategoryId,
                    UserId = l.UserId,
                    ImageFileName = l.Images.Select(img => (IFormFile)new FormFile(Stream.Null, 0, 0, null, img.FileName)).ToList(),
                }).ToListAsync();

            return listings;
        }

        public async Task<Listing> GetListingDetailsAsync(Guid id)
        {
            var listing = await _context.Listings
                .Include(l => l.Category)
                .Include(l => l.User)
                .Include(l => l.Images)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (listing == null)
            {
                throw new Exception($"Listing with ID {id} not found");
            }

            return listing;
        }
    }
}
