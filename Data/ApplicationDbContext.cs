using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BugBuddy.Models;

namespace BugBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bug> Bug { get; set; } // Reverted back to original name
        public DbSet<Note> Note { get; set; } // Reverted back to original name

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