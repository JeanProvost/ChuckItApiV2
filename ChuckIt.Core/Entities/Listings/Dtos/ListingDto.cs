// Ignore Spelling: Dtos Dto
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Listings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
        public List<IFormFile> ImageFileName { get; set; } = new List<IFormFile>();
        public Guid UserId { get; set; }

        public ListingDto() { }

        public ListingDto(Listing data)
        {
            Title = data.Title;
            CategoryId = data.CategoryId;
            Description = data.Description;
            ImageFileName = data.Images.Select(image => (IFormFile)new FormFile(new MemoryStream(), 0, 0, null, image.FileName)).ToList();
            UserId = data.UserId;
            Price = data.Price;
        }
    }
}
