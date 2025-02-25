using ChuckIt.Core.Interfaces.IRepositories;
using ChuckItApiV2.Core.Entities.Listings;
using ChuckItApiV2.Infrastructure.Data;
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
    }
}
