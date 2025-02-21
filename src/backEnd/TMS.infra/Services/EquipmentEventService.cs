using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TMS.application.Interfaces;
using TMS.domain.Entities;
using TMS.infra.Data.Mapping;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Services;

public class EquipmentEventService(dbContext context, ILogger<EquipmentEventService> logger) : IEquipmentEventService
{
    private readonly dbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private readonly ILogger<EquipmentEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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

    public async Task ProcessEventsAsync(List<Event> events)
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

    public async Task ProcessEventsAsync(IEnumerable<Event> events)
    {
        try
        {
            var groupedEvents = events.GroupBy(e => e.EquipmentId);
            List<Trip> trips = new List<Trip>();

            foreach (var group in groupedEvents)
            {
                var equipmentId = group.Key;
                var eventList = group.OrderBy(e => e.EventDate).ToList();
                Trip currentTrip = null;
                Event startEvent = null;

                for (int i = 0; i < eventList.Count; i++)
                {
                    var eventEntity = eventList[i];

                    if (eventEntity.Code == "W")
                    {
                        if (currentTrip == null)
                        {
                            // Capture the start event for the trip
                            startEvent = eventEntity;
                            currentTrip = new Trip
                            {
                                EquipmentId = equipmentId,
                                Origin_CityId = eventEntity.CityId,
                                Start_Date = eventEntity.EventDate,
                                HasIssue = false,
                                Completed = false,
                                Events = new List<Event> { eventEntity }
                            };

                            eventEntity.Trip = currentTrip;
                        }
                        else
                        {
                            // Misplaced code, flag the trip with an issue
                            currentTrip.HasIssue = true;
                            //then close the previous trip set the end date to the current event date
                            //Get the previous event 
                            var previousEvent = eventList[i - 1];
                            currentTrip.Destination_CityId = previousEvent.CityId;
                            currentTrip.End_Date = previousEvent.EventDate;
                            currentTrip.Completed = false; //because it was not properly completed
                            trips.Add(currentTrip);

                            //Start a new trip:
                            startEvent = eventEntity;
                            currentTrip = new Trip
                            {
                                EquipmentId = equipmentId,
                                Origin_CityId = eventEntity.CityId,
                                Start_Date = eventEntity.EventDate,
                                HasIssue = false,
                                Completed = false,
                                Events = new List<Event> { eventEntity }
                            };

                            eventEntity.Trip = currentTrip;
                        }
                    }
                    else if (eventEntity.Code == "Z")
                    {
                        if (currentTrip != null)
                        {
                            currentTrip.Destination_CityId = eventEntity.CityId;
                            currentTrip.End_Date = eventEntity.EventDate;
                            currentTrip.Completed = true;
                            currentTrip.Events.Add(eventEntity);
                            eventEntity.Trip = currentTrip;
                            trips.Add(currentTrip);
                            currentTrip = null;
                        }
                        else
                        {
                            // Misplaced code, handle the abnormal sequence
                            startEvent = eventList.FirstOrDefault(e => e.EventDate < eventEntity.EventDate);
                            if (startEvent != null)
                            {
                                currentTrip = new Trip
                                {
                                    EquipmentId = equipmentId,
                                    Origin_CityId = startEvent.CityId,
                                    Start_Date = startEvent.EventDate,
                                    Destination_CityId = eventEntity.CityId,
                                    End_Date = eventEntity.EventDate,
                                    HasIssue = true,
                                    Completed = true,
                                    Events = new List<Event> { startEvent, eventEntity }
                                };
                                startEvent.Trip = currentTrip;
                                eventEntity.Trip = currentTrip;
                                trips.Add(currentTrip);
                                currentTrip = null;
                            }
                        }
                    }
                    else
                    {
                        // Intermediate events; ensure there's a current trip
                        if (currentTrip != null)
                        {
                            currentTrip.Events.Add(eventEntity);
                            eventEntity.Trip = currentTrip;
                        }
                        else //This is an abnormal sequence
                        {
                            startEvent = eventEntity;
                            currentTrip = new Trip
                            {
                                EquipmentId = equipmentId,
                                Origin_CityId = eventEntity.CityId,
                                Start_Date = eventEntity.EventDate,
                                HasIssue = true, //it starts with a wrong sequence.
                                Completed = false,
                                Events = new List<Event> { eventEntity }
                            };

                            eventEntity.Trip = currentTrip;
                        }
                    }
                }

                if (currentTrip != null && !currentTrip.Completed)
                {
                    // Trip ended without a matching "Z" code
                    currentTrip.HasIssue = true;
                    //set the enddate and destination city
                    currentTrip.End_Date = eventList.Last().EventDate;
                    currentTrip.Destination_CityId = eventList.Last().CityId;

                    trips.Add(currentTrip);
                }
            }
            _context.Trips.AddRange(trips);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing events.");
            throw;
        }

    }
    public async Task ProcessEvents(List<Event> events)
    {
        try
        {
            // Convert event times to UTC and sort by event time
            var sortedEvents = events
                .Select(e => new Event
                {
                    EquipmentId = e.EquipmentId,
                    Code = e.Code,
                    EventDate = ConvertToUtc(e.EventDate, e.CityId), // Convert to UTC
                    CityId = e.CityId
                })
                .OrderBy(e => e.EventDate)
                .ToList();

            var trips = new List<Trip>();
            Trip currentTrip = null;

            foreach (var evt in sortedEvents)
            {
                if (evt.Code == "W") // Start of a new trip
                {
                    if (currentTrip != null)
                    {
                        // If a new trip starts without the previous trip ending, mark it as incomplete
                        currentTrip.HasIssue = true;
                        trips.Add(currentTrip);
                    }

                    currentTrip = new Trip
                    {
                        EquipmentId = evt.EquipmentId,
                        Origin_CityId = evt.CityId,
                        Start_Date = evt.EventDate,
                        Completed = false
                    };
                }
                else if (evt.Code == "Z" && currentTrip != null) // End of the current trip
                {
                    currentTrip.Destination_CityId = evt.CityId;
                    currentTrip.End_Date = evt.EventDate;
                    // currentTrip.Duration = currentTrip.End_Date - currentTrip.Start_Date;
                    currentTrip.Completed = true;
                    trips.Add(currentTrip);
                    currentTrip = null; // Reset for the next trip
                }
                else if (currentTrip != null)
                {
                    // Events between W and Z are part of the current trip
                    // No action needed here unless you want to track intermediate events
                }
            }

            // If there's an incomplete trip at the end, add it to the list
            if (currentTrip != null)
            {
                currentTrip.HasIssue = true;
                trips.Add(currentTrip);
            }

            // Save trips to the database
            _context.Trips.AddRange(trips);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing events.");
            throw;
        }

    }

    private DateTimeOffset ConvertToUtc(DateTimeOffset eventTime, int cityId)
    {
        var city = _context.Cities.Find(cityId);
        if (city == null)
        {
            throw new InvalidOperationException($"City with ID {cityId} not found.");
        }

        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(city.TimeZone);
        return TimeZoneInfo.ConvertTimeToUtc(eventTime.DateTime, timeZone);
    }
}
