using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApiV2.Core.Entities.Listing
{
    public class Listings : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Category { get; set; } = new Categories();
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; } = new Users();
        public Guid UserId { get; set; }
        public ICollection<Images> Images { get; set; } = new List<Images>();
    }

    public class Locations : BaseEntity<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Images : BaseEntity<Guid>
    {
        public string FileName { get; set; } = string.Empty;

        [ForeignKey("ListingId")]
        public Guid ListingId { get; set; }
        public Listings Listing { get; set; } = new Listings();
    }
}
