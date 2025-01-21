using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApiV2.Core.Entities.Listing
{
    public class Listing : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; } = new Category.Category();
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = new User.User();
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
