using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Entities.Listings.Dtos
{
    public class UpdateListingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> ImageFileName { get; set; } = new List<IFormFile>();

        public UpdateListingDto() { }

        public UpdateListingDto(UpdateListingDto data)
        {
            Title = data.Title;
            CategoryId = data.CategoryId;
            Description = data.Description;
            Price = data.Price;
        }
    }
}
