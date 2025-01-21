using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listing;

namespace ChuckItApiV2.Core.Entities.User
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Listing.Listing> Listings { get; set; } = new List<Listing.Listing>();
    }
}
