// Ignore Spelling: Dtos Dto
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Listings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Entities.Listings.Dtos
{
    public class ListingDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageFileName { get; set; } = new List<string>();
        public Guid UserId { get; set; }

        public ListingDto() { }

        public ListingDto(Listing data)
        {
            Title = data.Title;
            CategoryId = data.CategoryId;
            Description = data.Description;
            ImageFileName = data.Images.Select(img => img.FileName).ToList();
            UserId = data.UserId;
            Price = data.Price;
        }
    }
}
