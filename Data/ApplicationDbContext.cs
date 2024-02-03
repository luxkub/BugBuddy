using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BugBuddy.Models;

namespace BugBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BugBuddy.Models.Bug> Bug { get; set; } = default!;
        public DbSet<BugBuddy.Models.Note> Note { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete for Bug -> Notes relationship
            modelBuilder.Entity<Bug>()
                .HasMany(b => b.Notes)
                .WithOne()
                .HasForeignKey("BugId")
                .OnDelete(DeleteBehavior.Cascade);

            // Additional configurations...

            // Example of how to configure cascade delete for other relationships
            // modelBuilder.Entity<YourEntity>()
            //     .HasMany(e => e.YourRelatedEntities)
            //     .WithOne()
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
