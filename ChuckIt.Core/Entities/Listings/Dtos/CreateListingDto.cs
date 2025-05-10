// Ignore Spelling: Dtos Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Entities.Listings.Dtos
{
    public class CreateListingDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> ImageFileName { get; set; } = new List<IFormFile>();

        public CreateListingDto() { }

        public CreateListingDto(ListingDto data)
        {
            Title = data.Title;
            CategoryId = data.CategoryId;
            Description = data.Description;
            //ImageFileName = data.ImageFileName;
            Price = data.Price;
        }
    }

}
