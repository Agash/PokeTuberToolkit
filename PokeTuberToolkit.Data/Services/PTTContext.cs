using Microsoft.EntityFrameworkCore;
using PokeTuberToolkit.Data.Configuration;
using PokeTuberToolkit.Data.Models.Broadcast;
using PokeTuberToolkit.Data.Models.YTPlays;

namespace PokeTuberToolkit.Data.Services;
public class PTTContext(DbContextOptions<PTTContext> options) : DbContext(options)
{
    public DbSet<Broadcaster> Broadcasters
    {
        get; set;
    }
    public DbSet<BroadcastingSession> BroadcastingSessions
    {
        get; set;
    }
    public DbSet<YouTubeBroadcastingSession> YouTubeBroadcastingSessions
    {
        get; set;
    }
    public DbSet<Donation> Donations
    {
        get; set;
    }
    public DbSet<Message> Messages
    {
        get; set;
    }
    public DbSet<User> Users
    {
        get; set;
    }
    public DbSet<YouTubeUser> YouTubeUsers
    {
        get; set;
    }

    public DbSet<ButtonMapping> ButtonMappings
    {
        get; set;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        new BroadcastingSessionEntityTypeConfiguration().Configure(modelBuilder.Entity<BroadcastingSession>());
    }
}
