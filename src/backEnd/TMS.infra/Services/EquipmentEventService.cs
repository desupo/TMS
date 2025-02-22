using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TMS.application.Interfaces;
using TMS.domain.Entities;
using TMS.infra.Data.Mapping;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Services;

/// <summary>
/// Service to process equipment events
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class EquipmentEventService(dbContext context, ILogger<EquipmentEventService> logger) : IEquipmentEventService
{
    private readonly dbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private readonly ILogger<EquipmentEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Parse the CSV file and convert the event times to UTC
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<List<Event>> ParseCsvAsync(IFormFile file)
    {
        var events = new List<Event>();
        try
        {
            using var stream = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);
            // Register the custom class map
            csv.Context.RegisterClassMap<EventMap>();

            var csvRecords = new List<Event>();
            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<Event>();
                    csvRecords.Add(record);
                }
                catch (CsvHelperException ex)
                {
                    // Log the error or handle it as needed
                    Console.WriteLine($"Error parsing row: {ex.Message}");
                }
            }

            foreach (var record in csvRecords)
            {
                var city = await _context.Cities.FindAsync(record.CityId);
                if (city != null)
                {
                    try
                    {
                        var eventTimeZone = TimeZoneInfo.FindSystemTimeZoneById(city.TimeZone);
                        var localDateTime = record.EventDate.DateTime;

                        // If the time is invalid (spring forward), adjust it forward
                        if (eventTimeZone.IsInvalidTime(localDateTime))
                        {
                            Console.WriteLine($"Invalid time detected: {localDateTime} in {city.TimeZone}. Adjusting forward...");
                            localDateTime = localDateTime.AddHours(1);
                        }

                        // If the time is ambiguous (fall back), choose the standard time offset
                        if (eventTimeZone.IsAmbiguousTime(localDateTime))
                        {
                            Console.WriteLine($"Ambiguous time detected: {localDateTime} in {city.TimeZone}. Choosing earlier standard occurrence...");

                            // Get the two possible UTC offsets
                            var offsets = eventTimeZone.GetAmbiguousTimeOffsets(localDateTime);
                            var chosenOffset = offsets.Min(); // Choose the earlier (standard) offset

                            // Manually calculate UTC time using the chosen offset
                            record.EventDate = new DateTimeOffset(localDateTime, chosenOffset).ToUniversalTime();
                        }
                        else
                        {
                            // Normal conversion
                            record.EventDate = TimeZoneInfo.ConvertTimeToUtc(localDateTime, eventTimeZone);
                        }
                    }
                    catch (TimeZoneNotFoundException ex)
                    {
                        Console.WriteLine($"Timezone '{city.TimeZone}' not found. Using original time for City ID {record.CityId}. Error: {ex.Message}");
                    }
                    catch (InvalidTimeZoneException ex)
                    {
                        Console.WriteLine($"Invalid timezone '{city.TimeZone}' for City ID {record.CityId}. Using original time. Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error converting timezone for City ID {record.CityId}. Using original time. Error: {ex.Message}");
                    }
                }
                events.Add(record);
            }



        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Error parsing CSV: {ex.Message}");
            _logger.LogError(ex, "An error occurred while parsing the CSV file.");
        }

        return events;
    }
    /// <summary>
    /// Process the events and save the trips to the database
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    public async Task ProcessEventsAsync(List<Event> events)
    {
        try
        {
            // Group events by EquipmentId
            var eventsByEquipment = events
                .OrderBy(e => e.EventDate) // Ensure events are ordered by date
                .GroupBy(e => e.EquipmentId);

            var trips = new List<Trip>();

            foreach (var equipmentGroup in eventsByEquipment)
            {
                Trip currentTrip = null;
                Event previousEvent = null;

                foreach (var currentEvent in equipmentGroup)
                {
                    switch (currentEvent.Code)
                    {
                        case "W":
                            if (currentTrip != null)
                            {
                                // End the current trip (no Z event found)
                                currentTrip.Destination_CityId = previousEvent.CityId;
                                currentTrip.End_Date = previousEvent.EventDate;
                                currentTrip.Completed = false; // Trip ended without Z
                                currentTrip.HasIssue = true; // Trip ended abnormally
                                trips.Add(currentTrip);
                            }

                            // Start a new trip
                            currentTrip = CreateNewTrip(currentEvent);
                            break;

                        case "A":
                        case "D":
                            if (currentTrip == null)
                            {
                                // Start a new trip with an issue (no W event found)
                                currentTrip = CreateNewTrip(currentEvent);
                                currentTrip.HasIssue = true;
                            }
                            else
                            {
                                // Add the event to the current trip
                                currentTrip.Events.Add(currentEvent);
                            }
                            break;

                        case "Z":
                            if (currentTrip == null)
                            {
                                // Start and end a trip with an issue (no W event found)
                                currentTrip = CreateNewTrip(currentEvent);
                                currentTrip.Destination_CityId = currentEvent.CityId;
                                currentTrip.End_Date = currentEvent.EventDate;
                                currentTrip.Completed = true;
                                currentTrip.HasIssue = true;
                                trips.Add(currentTrip);
                                currentTrip = null;
                            }
                            else
                            {
                                // End the current trip
                                currentTrip.Destination_CityId = currentEvent.CityId;
                                currentTrip.End_Date = currentEvent.EventDate;
                                currentTrip.Completed = true;
                                currentTrip.Events.Add(currentEvent);
                                trips.Add(currentTrip);
                                currentTrip = null;
                            }
                            break;
                    }

                    previousEvent = currentEvent;
                }

                // Add the last trip for this equipment if it exists
                if (currentTrip != null)
                {
                    // If the trip doesn't have a Z event, mark it as incomplete
                    if (!currentTrip.Completed)
                    {
                        currentTrip.Destination_CityId = previousEvent.CityId;
                        currentTrip.End_Date = previousEvent.EventDate;
                        currentTrip.HasIssue = true;
                    }
                    trips.Add(currentTrip);
                }
            }

            // Save trips to the database
            await SaveTripsAsync(trips);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while processing the data");
        }
       
    }
    /// <summary>
    /// Create a new trip from the given start event
    /// </summary>
    /// <param name="startEvent"></param>
    /// <returns></returns>
    private Trip CreateNewTrip(Event startEvent)
    {
        return new Trip
        {
            EquipmentId = startEvent.EquipmentId,
            Origin_CityId = startEvent.CityId,
            Start_Date = startEvent.EventDate,

            Events = new List<Event> { startEvent }
        };
    }
    /// <summary>
    /// Save the trips to the database
    /// </summary>
    /// <param name="trips"></param>
    /// <returns></returns>
    private async Task SaveTripsAsync(List<Trip> trips)
    {
        foreach (var trip in trips)
        {
            // Calculate duration in hours
            trip.TotalTripHours = (trip.End_Date - trip.Start_Date).TotalHours;

            // Add trip to the context
            _context.Trips.Add(trip);
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
    }
}
