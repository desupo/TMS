using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.domain.Entities;

namespace TMS.infra.Persistence.Configurations;
public class Trip_Config : BaseEntityConfiguration<Trip, long>
{
    public override void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.Property(t => t.EquipmentId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Origin_CityId)
            .IsRequired();

        builder.Property(t => t.Destination_CityId)
            .IsRequired();

        builder.Property(t => t.Start_Date)
            .IsRequired();

        builder.Property(t => t.End_Date)
            .IsRequired();

        builder.HasOne(t => t.Origin_City)
            .WithMany(c => c.Start_Cities)
            .HasForeignKey(t => t.Origin_CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Destination_City)
            .WithMany(c => c.Destination_Cities)
            .HasForeignKey(t => t.Destination_CityId)
            .OnDelete(DeleteBehavior.Restrict);

        /*For SQL Server*/
        //builder.Property(t => t.Duration)
        //    .HasComputedColumnSql("DATEDIFF_BIG(MINUTE, Start_Date AT TIME ZONE 'UTC', End_Date AT TIME ZONE 'UTC')", stored: true);



    }
}
