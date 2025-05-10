using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckItApiV2.Core.Entities.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Interfaces.IServices
{
    public interface IListingService
    {
        Task<List<ListingDto>> GetAllListingsAsync();
        Task<Listing> GetListingDetailsAsync(Guid id);
        Task<ListingDto> CreateListingAsync(CreateListingDto request);
        Task<ListingDto> UpdateListingAsync(UpdateListingDto request, Guid id);
        Task DeleteListingAsync(Guid id);
    }
}
