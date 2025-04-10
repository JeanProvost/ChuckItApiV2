﻿using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckItApiV2.Core.Entities.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Interfaces.IRepositories
{
    public interface IListingRepository : IBaseRepository<Listing>
    {
        Task<List<ListingDto>> GetAllListingsAsync();
        Task<Listing> GetListingDetailsAsync(Guid id);
    }
}
