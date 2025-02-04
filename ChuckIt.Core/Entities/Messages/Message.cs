using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listings;
using ChuckItApiV2.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApiV2.Core.Entities.Messages
{
    public class Message : BaseEntity<Guid>
    {
        [ForeignKey("ListingId")]
        public Listing Listings { get; set; } = new Listing();
        public Guid ListingId { get; set; }
        public string Content { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User Users { get; set; } = new User();
    }
}
