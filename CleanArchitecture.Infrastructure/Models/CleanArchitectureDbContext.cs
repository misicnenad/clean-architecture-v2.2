using CleanArchitecture.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Models
{
    public class CleanArchitectureDbContext : DbContext
    {
        public CleanArchitectureDbContext()
        {
        }

        public CleanArchitectureDbContext(DbContextOptions<CleanArchitectureDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IdNumber).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
