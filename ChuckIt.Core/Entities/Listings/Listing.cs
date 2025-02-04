using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using ChuckItApiV2.Core.Entities.Messages;

namespace ChuckItApiV2.Core.Entities.Listings
{
    public class Listing : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; } = new Category.Category();
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = new User();
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
        public Listing Listing { get; set; } = new Listing();
    }
}
