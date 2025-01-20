using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listing;
using ChuckItApiV2.Core.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApiV2.Core.Entities.Message
{
    public class Messages : BaseEntity<Guid>
    {
        [ForeignKey("ListingId")]
        public Listings Listings { get; set; } = new Listings();
        public Guid ListingId { get; set; }
        public Listings Listing { get; set; } = new Listings();
        public string Message { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public Users User { get; set; } = new Users();
    }
}
