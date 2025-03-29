using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using ChuckItApiV2.Core.Entities.Messages;
using ChuckIt.Core.Entities.Listings.Dtos;

namespace ChuckItApiV2.Core.Entities.Listings
{
    public class Listing : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; } = new Category.Category();
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = new User();
        public Guid UserId { get; set; }
        public ICollection<Images> Images { get; set; } = new List<Images>();

        public Listing() { }

        public Listing(CreateListingDto data)
        {
            Id = Guid.NewGuid();
            Title = data.Title;
            Description = data.Description;
            CategoryId = data.CategoryId;
            Price = data.Price;
            Images = new List<Images>();
        }
    }

    public class Images : BaseEntity<Guid>
    {
        public string FileName { get; set; } = string.Empty;

        [ForeignKey("ListingId")]
        public Guid ListingId { get; set; }
        public Listing Listing { get; set; } = new Listing();
    }


}
