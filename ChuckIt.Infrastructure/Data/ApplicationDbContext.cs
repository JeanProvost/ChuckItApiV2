using ChuckItApiV2.Core.Entities;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Listing;
using ChuckItApiV2.Core.Entities.Message;
using ChuckItApiV2.Core.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ChuckItApiV2.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Listings> Listings { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
