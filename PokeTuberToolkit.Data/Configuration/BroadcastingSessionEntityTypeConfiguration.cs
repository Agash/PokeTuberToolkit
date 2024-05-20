using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeTuberToolkit.Data.Models.Broadcast;

namespace PokeTuberToolkit.Data.Configuration;
internal class BroadcastingSessionEntityTypeConfiguration : IEntityTypeConfiguration<BroadcastingSession>
{
    public void Configure(EntityTypeBuilder<BroadcastingSession> builder)
    {
        _ = builder
             .HasDiscriminator(b => b.Platform)
             .HasValue<YouTubeBroadcastingSession>(Platform.YouTube);

        _ = builder
            .Property(b => b.Platform)
            .HasConversion<string>();
    }
}

