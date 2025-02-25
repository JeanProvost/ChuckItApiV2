﻿using ChuckItApiV2.Core.Entities.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Entities.Listings
{
    public class ListingDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageFileName { get; set; } = new List<string>();
    }
}
