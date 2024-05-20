using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokeTuberToolkit.Data.Models.Broadcast;
using System.Reflection.Emit;

namespace PokeTuberToolkit.Data.Configuration;
internal class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder
            .HasDiscriminator(u => u.Platform)
            .HasValue<YouTubeUser>(Platform.YouTube);

        _ = builder
            .Property(u => u.Platform)
            .HasConversion<string>();
    }
}