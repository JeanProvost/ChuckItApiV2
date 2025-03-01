using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckIt.Core.Interfaces.IRepositories;
using ChuckItApiV2.Core.Entities.Listings;
using ChuckItApiV2.Infrastructure.Data;
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
                    ImageFileName = l.Images.Select(i => i.FileName).ToList()
                }).ToListAsync();

            return listings;
        }
    }
}
