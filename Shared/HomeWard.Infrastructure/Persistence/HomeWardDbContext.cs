using HomeWard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeWard.Infrastructure.Persistence;

public sealed class HomeWardDbContext : DbContext
{
    public HomeWardDbContext(DbContextOptions<HomeWardDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }

    public DbSet<User> Users => Set<User>();
}
