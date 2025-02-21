using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMS.domain.Contracts;

namespace TMS.infra.Persistence.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
       // builder.Property(e => e.Date_Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        ConfigureDateTimeOffsetProperties(builder);
    }
    protected void ConfigureDateTimeOffsetProperties(EntityTypeBuilder<TEntity> builder)
    {
        foreach (var property in builder.Metadata.GetProperties())
        {
            if (property.ClrType == typeof(DateTimeOffset) || property.ClrType == typeof(DateTimeOffset?))
            {
                var parameter = Expression.Parameter(typeof(TEntity), "e");
                var propertyExpression = Expression.Property(parameter, property.Name);
                var lambdaExpression = Expression.Lambda(propertyExpression, parameter);

                builder.Property(lambdaExpression as Expression<Func<TEntity, DateTimeOffset>>)
                    .HasConversion(
                        v => v != null ? v.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK") : null, // Convert to ISO 8601 string
                        v => (DateTimeOffset)(v != null ? DateTimeOffset.Parse(v) : (DateTimeOffset?)null) // Parse back from ISO 8601 string
                    );

                // Set default value for Date_Created property if it exists
                if (property.Name == nameof(BaseEntity.Date_Created))
                {
                    builder.Property(lambdaExpression as Expression<Func<TEntity, DateTimeOffset>>)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }
        }
    }
}

public abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Configure the Date_Created property - SQL Server Only
        //builder.Property(e => e.Date_Created)
        //    .IsRequired()
        //    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        ConfigureDateTimeOffsetProperties(builder);
    }

    protected void ConfigureDateTimeOffsetProperties(EntityTypeBuilder<TEntity> builder)
    {
        foreach (var property in builder.Metadata.GetProperties())
        {
            if (property.ClrType == typeof(DateTimeOffset) || property.ClrType == typeof(DateTimeOffset?))
            {
                var parameter = Expression.Parameter(typeof(TEntity), "e");
                var propertyExpression = Expression.Property(parameter, property.Name);
                var lambdaExpression = Expression.Lambda(propertyExpression, parameter);

                builder.Property(lambdaExpression as Expression<Func<TEntity, DateTimeOffset>>)
                    .HasConversion(
                        v => v != null ? v.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK") : null, // Convert to ISO 8601 string
                        v => (DateTimeOffset)(v != null ? DateTimeOffset.Parse(v) : (DateTimeOffset?)null) // Parse back from ISO 8601 string
                    );

                // Set default value for Date_Created property if it exists
                if (property.Name == nameof(BaseEntity.Date_Created))
                {
                    builder.Property(lambdaExpression as Expression<Func<TEntity, DateTimeOffset>>)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP"); //SYSDATETIMEOFFSET() for Sql Server
                }
            }
        }
    }
}