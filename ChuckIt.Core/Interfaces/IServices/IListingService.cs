using ChuckIt.Core.Entities.Listings.Dtos;
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
        Task<ListingDto> GetListingDetailsAsync(Guid id);
    }
}
