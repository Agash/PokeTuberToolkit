using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokeTuberToolkit.Data.Models.Broadcast;
using PokeTuberToolkit.Data.Models.YTPlays;

namespace PokeTuberToolkit.Data.Configuration;
internal class ButtonMappinsEntityTypeConfiguration : IEntityTypeConfiguration<ButtonMapping>
{
    public void Configure(EntityTypeBuilder<ButtonMapping> builder)
    {
        _ = builder
             .HasIndex(b => b.Preset);
    }
}

