using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.domain.Entities;

namespace TMS.infra.Persistence.Configurations;

public class Event_Code_Config : BaseEntityConfiguration<Event_Code>
{
    public override void Configure(EntityTypeBuilder<Event_Code> builder)
    {
        base.Configure(builder);
        
        //For seeding alone. 
        builder.Property(e => e.Id).ValueGeneratedNever(); 

        // Configure the Code property
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(1)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(), // Converts the value to uppercase before saving
                v => v);

        // Configure the Name property
        builder.Property(e => e.Name)
            .HasMaxLength(35);

        // Configure the Description property
        builder.Property(e => e.Description)
            .HasMaxLength(450);
    }
}
