using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listing;
using ChuckItApiV2.Core.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApiV2.Core.Entities.Message
{
    public class Message : BaseEntity<Guid>
    {
        [ForeignKey("ListingId")]
        public Listing.Listing Listings { get; set; } = new Listing.Listing();
        public Guid ListingId { get; set; }
        public Listing.Listing Listing { get; set; } = new Listing.Listing();
        public string Content { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User.User User { get; set; } = new User.User();
    }
}
