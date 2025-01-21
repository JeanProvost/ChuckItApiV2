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

        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Listings)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Listing>()
                .HasMany(l => l.Images)
                .WithOne(i => i.Listing)
                .HasForeignKey(i => i.ListingId);

        }
    }
}
