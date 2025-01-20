using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listing;

namespace ChuckItApiV2.Core.Entities.User
{
    public class Users : BaseEntity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Listings> Listings { get; set; } = new List<Listings>();
    }
}
