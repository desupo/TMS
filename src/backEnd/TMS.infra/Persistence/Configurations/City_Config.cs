using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.domain.Entities;

namespace TMS.infra.Persistence.Configurations;

public class City_Config : BaseEntityConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.TimeZone)
            .IsRequired()
            .HasMaxLength(150);

        // Unique index to the Name property
        builder.HasIndex(p => p.Name).IsUnique();
    }
}

