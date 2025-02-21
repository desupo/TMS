using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TMS.application.Interfaces;
using TMS.domain.Entities;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Services;

public class EquipmentEventService(dbContext context, ILogger<EquipmentEventService> logger) : IEquipmentEventService
{
    private readonly dbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private readonly ILogger<EquipmentEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<IEnumerable<Event>> ParseCsvAsync(IFormFile file)
    {
        var events = new List<Event>();
        try
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                var csvRecords = csv.GetRecords<Event>().ToList();
                foreach (var record in csvRecords)
                {
                    var city = await _context.Cities.FindAsync(record.CityId);
                    if (city != null)
                    {
                        var eventTimeZone = TimeZoneInfo.FindSystemTimeZoneById(city.TimeZone);
                        record.EventDate = TimeZoneInfo.ConvertTimeToUtc(record.EventDate.DateTime, eventTimeZone);
                    }
                    events.Add(record);
                }
            }
        }
        catch (Exception)
        {


        }

        return events;
    }

    public async Task ProcessEventsAsync(IEnumerable<Event> events)
    {
        try
        {
            var groupedEvents = events.GroupBy(e => e.EquipmentId);

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
                            _context.Trips.Add(currentTrip);
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
                                _context.Trips.Add(currentTrip);
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
                    }
                }

                if (currentTrip != null && !currentTrip.Completed)
                {
                    // Trip ended without a matching "Z" code
                    currentTrip.HasIssue = true;
                    _context.Trips.Add(currentTrip);
                }
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing events.");
            throw;
        }

    }
    public async Task ProcessEvents(IEnumerable<Event> events)
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
