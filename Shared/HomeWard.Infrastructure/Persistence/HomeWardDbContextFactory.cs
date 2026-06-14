using HomeWard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class HomeWardDbContextFactory : IDesignTimeDbContextFactory<HomeWardDbContext>
{
    public HomeWardDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HomeWardDbContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=homeward;Username=homeward;Password=homeward");

        return new HomeWardDbContext(optionsBuilder.Options);
    }
}