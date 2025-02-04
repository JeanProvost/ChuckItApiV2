using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listings;

namespace ChuckItApiV2.Core.Entities.Users
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
