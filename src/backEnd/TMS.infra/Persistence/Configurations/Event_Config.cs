using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.domain.Entities;

namespace TMS.infra.Persistence.Configurations;

public class Event_Config : BaseEntityConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.TripId)
            .IsRequired();

        builder.HasOne(e => e.Trip)
            .WithMany(t => t.Events)
            .HasForeignKey(e => e.TripId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
