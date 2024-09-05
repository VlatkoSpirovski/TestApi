using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<Location> Locations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>()
            .HasOne(l => l.WeatherForecast) // Use correct lambda for navigation property
            .WithMany(wf => wf.Locations)
            .HasForeignKey(l => l.WeatherForecastId); // Ensure this matches the FK property

        base.OnModelCreating(modelBuilder);
    }
}