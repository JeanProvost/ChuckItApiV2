using ChuckItApiV2.Core.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Infrastructure.Seeders
{
    public static class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Furniture", Icon = "floor-lamp", BackgroundColor = "#fc5c65", Color = "white" },
                new Category { Id = 2, Name = "Cars", Icon = "car", BackgroundColor = "#fd9644", Color = "white" },
                new Category { Id = 3, Name = "Cameras", Icon = "camera", BackgroundColor = "#fed330", Color = "white" },
                new Category { Id = 4, Name = "Games", Icon = "cards", BackgroundColor = "#26de81", Color = "white" },
                new Category { Id = 5, Name = "Clothing", Icon = "shoe-heel", BackgroundColor = "#2bcbba", Color = "white" },
                new Category { Id = 6, Name = "Sports", Icon = "basketball", BackgroundColor = "#45aaf2", Color = "white" },
                new Category { Id = 7, Name = "Movies & Music", Icon = "headphones", BackgroundColor = "#4b7bec", Color = "white" },
                new Category { Id = 8, Name = "Books", Icon = "book-open-variant", BackgroundColor = "#a55eea", Color = "white" },
                new Category { Id = 9, Name = "Other", Icon = "application", BackgroundColor = "#778ca3", Color = "white" }
            );
        }
    }
}
