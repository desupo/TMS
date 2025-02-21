using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using TMS.domain.Entities;
using TMS.infra.Data.Mapping;
using TMS.infra.Persistence.Configurations;

namespace TMS.infra.Persistence.Context;

public class dbContext : DbContext
{
    //Entities
    public DbSet<City> Cities { get; set; }
    public DbSet<Event_Code> Event_Codes { get; set; }
    public DbSet<Trip> Trips { get; set; }

    public dbContext(DbContextOptions<dbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Apply the configurations
        builder.ApplyConfiguration(new City_Config());
        builder.ApplyConfiguration(new Event_Code_Config());
        builder.ApplyConfiguration(new Trip_Config());

        base.OnModelCreating(builder);

        //Seed Data
        SeedData(builder);
    }
    /// <summary>
    /// Seed that City and Event Code data. This will happen once
    /// </summary>
    /// <param name="builder"></param>
    private void SeedData(ModelBuilder builder) {
        // Get the path to the CSV files in the Infrastructure layer
        var seedDataFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Data", "SeedData");

        // Seed Event_Codes
        var eventCodes = ReadCsv<Event_Code, EventCodeMap>(Path.Combine(seedDataFolder, "event_code_definitions.csv"));
        int idCounter = -1;
        foreach (var eventCode in eventCodes)
        {
            if (eventCode.Id == 0) // If Id is missing or zero
            {
                eventCode.Id = idCounter--;
            }
        }
        builder.Entity<Event_Code>().HasData(eventCodes);

        // Seed Cities
        var cities = ReadCsv<City, CityMap>(Path.Combine(seedDataFolder, "canadian_cities.csv"));
        builder.Entity<City>().HasData(cities);
    }

    private List<T> ReadCsv<T, TMap>(string filePath) where TMap : ClassMap<T>
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TMap>();
        return csv.GetRecords<T>().ToList();
    }
}
//TODO: Setup DesignTime Factory if setting up in StartUp
