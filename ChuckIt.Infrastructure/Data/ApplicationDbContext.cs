using ChuckIt.Infrastructure.Seeders;
using ChuckItApiV2.Core.Entities;
using ChuckItApiV2.Core.Entities.Category;
using ChuckItApiV2.Core.Entities.Listings;
using ChuckItApiV2.Core.Entities.Messages;
using ChuckItApiV2.Core.Entities.Users;
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

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Listings)
                .WithMany(l => l.Messages)
                .HasForeignKey(m => m.ListingId)
                .OnDelete(DeleteBehavior.Cascade);

            CategorySeeder.Seed(modelBuilder);
        }
    }
}
